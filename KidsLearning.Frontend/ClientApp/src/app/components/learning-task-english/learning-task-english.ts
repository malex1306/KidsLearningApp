import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TasksService } from '../../services/tasks.service';
import { LearningService } from '../../services/learning.service';
import { LearningTask } from '../../models/learning-task';
import { CommonModule } from '@angular/common';
import { ActiveChildService } from '../../services/active-child.service';
import {Subscription} from 'rxjs';
import { QuizLogic, AnswerStatus} from '../../services/quiz-logic';

@Component({
  selector: 'app-learning-task-english',
  imports: [CommonModule, RouterLink],
  templateUrl: './learning-task-english.html',
  styleUrls: ['./learning-task-english.css']
})
export class LearningTaskEnglish implements OnInit, OnDestroy {
  task: LearningTask | null = null;
  childId: number | null = null;

  // Multiple Choice / Bilder
  currentQuestionIndex = 0;
  answerStatus: AnswerStatus = null;
  statusMessage = '';
  isWaitingForNext = false;

  // Deutsch/Englisch Matching
  currentBatchIndex = 0;
  currentBatch: any[] = [];
  batchSize = 8;
  shuffledGerman: string[] = [];
  shuffledEnglish: string[] = [];
  selectedGerman: string | null = null;
  selectedEnglish: string | null = null;
  connectedPairs = new Set<{ de: string; en: string }>();

  isCompleted = false;

  exam: boolean = false; // from route or service
  timerValue: number = 0; // in seconds
  timerInterval: any = null;
  examfailed: boolean = false;

  private subscriptions = new Subscription();

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService,
    private activeChildService: ActiveChildService,
    private quizLogic: QuizLogic
  ) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId');
    const examParam = this.route.snapshot.paramMap.get('exam');

    if (childIdParam) this.childId = +childIdParam;

    this.exam = examParam === 'true';

    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe(task => {
        // Filter nach Schwierigkeit
        const difficulty = this.activeChildService.activeChild()?.difficulty ?? 'Vorschule';
        if (difficulty) task.questions = task.questions.filter(q => q.difficulty === difficulty);

        this.task = task;

        if (this.task.title === 'Deutsch/Englisch verbinden') {
          this.loadBatch();
        }
        if (this.exam) {
          this.startTimer(300); // e.g., 5 minutes
        }
      });
    }
  }

  // ----------------- Multiple Choice / Bilder -----------------
  selectAnswer(answer: string) {
    if (!this.task || this.isWaitingForNext || this.childId === null) return;

    const result = this.quizLogic.selectEnglishAnswer(this.task, this.currentQuestionIndex, answer, []);
    this.answerStatus = result.answerStatus;
    this.statusMessage = result.statusMessage;
    this.isWaitingForNext = result.answerStatus === 'correct';

    if (this.isWaitingForNext) {
      setTimeout(() => this.nextQuestion(), 1500);
      if (this.currentQuestionIndex === this.task.questions.length - 1) this.completeTask();
    }
  }

  nextQuestion() {
    this.answerStatus = null;
    this.statusMessage = '';
    this.isWaitingForNext = false;

    if (this.task && this.currentQuestionIndex < this.task.questions.length - 1) {
      this.currentQuestionIndex++;
    } else {
      this.isCompleted = true;
      this.stopTimer();
    }
  }

  prevQuestion() {
    if (this.currentQuestionIndex > 0) this.currentQuestionIndex--;
    this.answerStatus = null;
    this.statusMessage = '';
    this.isWaitingForNext = false;
  }

  // ----------------- Deutsch/Englisch Matching -----------------
  loadBatch() {
    if (!this.task) return;
    const batches = this.quizLogic.createBatches(this.task, this.batchSize);
    this.currentBatch = batches[this.currentBatchIndex] ?? [];

    const shuffled = this.quizLogic.shuffleBatch(this.currentBatch);
    this.shuffledGerman = shuffled.german;
    this.shuffledEnglish = shuffled.english;

    this.connectedPairs.clear();
    this.selectedGerman = null;
    this.selectedEnglish = null;
  }

  nextBatch() {
    if (!this.task) return;
    const totalBatches = Math.ceil(this.task.questions.length / this.batchSize);
    if (this.currentBatchIndex < totalBatches - 1) {
      this.currentBatchIndex++;
      this.loadBatch();
    } else {
      this.isCompleted = true;
      this.stopTimer();
      this.completeTask();
    }
  }

  prevBatch() {
    if (this.currentBatchIndex > 0) {
      this.currentBatchIndex--;
      this.loadBatch();
    }
  }

  selectGerman(word: string) {
    this.selectedGerman = word;
    this.checkMatch();
  }

  selectEnglish(word: string) {
    this.selectedEnglish = word;
    this.checkMatch();
  }

  checkMatch() {
    if (!this.selectedGerman || !this.selectedEnglish) return;
    if (!this.task) return;

    if (this.quizLogic.checkEnglishMatching(this.selectedGerman, this.selectedEnglish, this.currentBatch)) {
      this.connectedPairs.add({ de: this.selectedGerman, en: this.selectedEnglish });
    }

    this.selectedGerman = null;
    this.selectedEnglish = null;

    if (this.connectedPairs.size === this.currentBatch.length) this.nextBatch();
  }

  isGermanConnected(word: string) {
    return [...this.connectedPairs].some(pair => pair.de === word);
  }

  isEnglishConnected(word: string) {
    return [...this.connectedPairs].some(pair => pair.en === word);
  }

  completeTask() {
    if (this.childId && this.task) {
      this.learningService.completeTask(this.childId, this.task.id).subscribe({
        next: () => console.log('Aufgabe erfolgreich abgeschlossen!'),
        error: err => console.error(err)
      });
    }
  }

  protected readonly Math = Math;

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
    this.stopTimer();
  }
  startTimer(seconds: number): void {
    this.timerValue = seconds;

    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }

    this.timerInterval = setInterval(() => {
      if (this.timerValue > 0) {
        this.timerValue--;
      } else {
        clearInterval(this.timerInterval);
        this.onTimeUp();
      }
    }, 1000);
  }

  onTimeUp(): void {
    this.examfailed = true;
  }

  get timerDisplay(): string {
    const minutes = Math.floor(this.timerValue / 60);
    const seconds = this.timerValue % 60;
    return `${minutes.toString().padStart(2, '0')}:${seconds
      .toString()
      .padStart(2, '0')}`;
  }
  onFinishTask(): void {
    if (this.task && this.childId) {
      this.isCompleted = true;
      this.stopTimer();
      this.completeTask();
      alert('Aufgabe automatisch beendet, da die Zeit abgelaufen ist.');
    }
  }
  stopTimer(): void {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
      this.timerInterval = null;
    }
  }
}
