import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TasksService } from '../../services/tasks.service';
import { LearningService } from '../../services/learning.service';
import { LearningTask } from '../../models/learning-task';
import { CommonModule } from '@angular/common';
import { ActiveChildService } from '../../services/active-child.service';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-learning-task-english',
  imports: [CommonModule, RouterLink],
  templateUrl: './learning-task-english.html',
  styleUrls: ['./learning-task-english.css']
})
export class LearningTaskEnglish implements OnInit, OnDestroy {
  task: LearningTask | null = null;
  currentQuestionIndex = 0;
  answerStatus: 'correct' | 'wrong' | null = null;
  statusMessage = '';
  isWaitingForNext = false;
  childId: number | null = null;
  isCompleted = false;

  pairs: { de: string; en: string }[] = [];
  shuffledEnglish: string[] = [];
  shuffledGerman: string[] = [];
  selectedGerman: string | null = null;
  selectedEnglish: string | null = null;

  connectedPairs: Set<{ de: string; en: string }> = new Set();

  batchSize = 8;
  currentBatchIndex = 0;
  currentBatch: any[] = [];
  exam: boolean = false; // from route or service
  timerValue: number = 0; // in seconds
  timerInterval: any = null;
  examfailed: boolean = false;

  private subscriptions = new Subscription();

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService,
    private activeChildService: ActiveChildService
  ) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId');
    const examParam = this.route.snapshot.paramMap.get('exam');

    if (childIdParam) this.childId = +childIdParam;

    this.exam = examParam === 'true';

    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe(task => {
        const activeChild = this.activeChildService.activeChild();
        const childDifficulty = activeChild?.difficulty ?? 'Vorschule';

        if (childDifficulty) {
          task.questions = task.questions.filter(q => q.difficulty === childDifficulty);
        }
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

  loadBatch(): void {
    if (!this.task) return;

    const start = this.currentBatchIndex * this.batchSize;
    const end = start + this.batchSize;
    this.currentBatch = this.task.questions.slice(start, end);

    if (this.task.title === 'Deutsch/Englisch verbinden') {
      this.shuffledGerman = this.shuffleArray(this.currentBatch.map(q => q.text));
      this.shuffledEnglish = this.shuffleArray(this.currentBatch.map(q => q.correctAnswer));
    }
  }
  nextBatch(): void {
    const totalBatches = Math.ceil(this.task?.questions.length ?? 0 / this.batchSize);

    if (this.currentBatchIndex < totalBatches - 1) {
      this.currentBatchIndex++;
      this.connectedPairs.clear();
      this.selectedGerman = null;
      this.selectedEnglish = null;
      this.loadBatch();
    }
  }
  prevBatch(): void {
    if (this.currentBatchIndex > 0) {
      this.currentBatchIndex--;
      this.connectedPairs.clear();
      this.selectedGerman = null;
      this.selectedEnglish = null;
      this.loadBatch();
    }
  }
  prevQuestion(): void {
    if (this.currentQuestionIndex > 0) {
      this.currentQuestionIndex--;
      this.answerStatus = null;
      this.statusMessage = '';
      this.isWaitingForNext = false;
    }
  }

  shuffleArray<T>(array: T[]): T[] {
    const arr = [...array];
    for (let i = arr.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [arr[i], arr[j]] = [arr[j], arr[i]];
    }
    return arr;
  }

  selectGerman(word: string) {
    this.selectedGerman = word;
    this.checkMatch();
  }

  selectEnglish(word: string) {
    this.selectedEnglish = word;
    this.checkMatch();
  }

  completeLearningTask(): void {
    if (this.childId && this.task) {
      this.learningService.completeTask(this.childId, this.task.id).subscribe({
        next: () => console.log('Englisch-Aufgabe erfolgreich abgeschlossen!'),
        error: (err) => console.error('Fehler beim Abschluss der Englisch-Aufgabe', err),
      });
    }
  }

  checkMatch() {
    if (this.selectedGerman && this.selectedEnglish && this.task) {
      const correct = this.currentBatch.find(
        q => q.text === this.selectedGerman && q.correctAnswer === this.selectedEnglish
      );

      if (correct) {
        this.connectedPairs.add({ de: correct.text, en: correct.correctAnswer });
      }

      this.selectedGerman = null;
      this.selectedEnglish = null;

      if (this.connectedPairs.size === this.currentBatch.length) {
        // Batch completed
        const totalBatches = Math.ceil(this.task.questions.length / this.batchSize);
        if (this.currentBatchIndex < totalBatches - 1) {
          this.nextBatch();
        } else {
          this.isCompleted = true;
          this.completeLearningTask();
        }
      }
    }
  }


  isGermanConnected(word: string): boolean {
    return [...this.connectedPairs].some(pair => pair.de === word);
  }

  isEnglishConnected(word: string): boolean {
    return [...this.connectedPairs].some(pair => pair.en === word);
  }

  selectAnswer(answer: string): void {
    if (this.isWaitingForNext || !this.task || this.childId === null) return;

    const currentQuestion = this.task.questions[this.currentQuestionIndex];
    if (currentQuestion && answer === currentQuestion.correctAnswer) {
      this.answerStatus = 'correct';
      this.statusMessage = 'Richtig! 🎉';
      this.isWaitingForNext = true;

      if (this.currentQuestionIndex === this.task.questions.length - 1) {
        this.learningService.completeTask(this.childId, this.task.id).subscribe({
          next: () => console.log('Aufgabe erfolgreich als abgeschlossen markiert!'),
          error: err => console.error('Fehler beim Markieren der Aufgabe als abgeschlossen', err)
        });
      }

      setTimeout(() => this.nextQuestion(), 1500);
    } else {
      this.answerStatus = 'wrong';
      this.statusMessage = 'Falsch, versuche es noch einmal. 🤔';
    }
  }

  nextQuestion(): void {
    this.answerStatus = null;
    this.statusMessage = '';
    this.isWaitingForNext = false;

    if (this.task && this.currentQuestionIndex < this.task.questions.length - 1) {
      this.currentQuestionIndex++;
    } else {
      this.isCompleted = true;
    }
  }

  protected readonly Math = Math;

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
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
      this.completeLearningTask();
      alert('Aufgabe automatisch beendet, da die Zeit abgelaufen ist.');
    }
  }
}
