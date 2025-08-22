import { Component, OnInit, OnDestroy } from '@angular/core';
import { LearningTask } from '../../models/learning-task';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TasksService } from '../../services/tasks.service';
import { CommonModule } from '@angular/common';
import { LearningService } from '../../services/learning.service';
import { QuestionNavigationService } from '../../services/question-navigation.service';
import { Subscription } from 'rxjs';
import { ActiveChildService } from '../../services/active-child.service';

@Component({
  selector: 'app-logic-task',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './logic-task.html',
  styleUrl: './logic-task.css'
})
export class LogicTask implements OnInit, OnDestroy {
  task: LearningTask | null = null;
  childId: number | null = null;
  isCompleted = false;
  private subscriptions = new Subscription();


  gameSequence: number[] = [];
  playerSequence: number[] = [];
  isMemorizing = false;
  isRecalling = false;
  highlightedIndex: number | null = null;
  currentLevel = 0;
  statusMessage = '';
  answerStatus: 'correct' | 'wrong' | null = null;


  lastClickedNumber: number | null = null;
  lastClickedStatus: 'correct' | 'wrong' | null = null;

  currentQuestionIndex = 0;
  questionImage: string | null = null;
  optionsImages: string[] = [];
  selectedOption: number | null = null;
  correctOptionIndex: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService,
    private navigationService: QuestionNavigationService,
    private activeChildService: ActiveChildService
  ) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId');

    if (childIdParam) this.childId = +childIdParam;

    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe(task => {
        const activeChild = this.activeChildService.activeChild();
        const childDifficulty = activeChild?.difficulty ?? 'Vorschule';
        if (childDifficulty) {
          task.questions = task.questions.filter(q => q.difficulty === childDifficulty);
        }
        this.task = task;
        if (this.task.title === 'Fülle die Form') {
          this.loadCurrentFillFormQuestion();
        }
        if (this.task.title === 'Zahlenkombinationen') {
          this.startGame();
        }
        this.navigationService.setTask(task);
      });
    }
  }
  loadCurrentFillFormQuestion(): void {
    if (!this.task || !this.task.questions || this.currentQuestionIndex >= this.task.questions.length) {
      this.isCompleted = true;
      return;
    }

    const question = this.task.questions[this.currentQuestionIndex];

    this.questionImage = question.imageUrl ?? null;
    this.optionsImages = question.options ?? [];
    this.correctOptionIndex = question.options?.findIndex(opt => opt === question.correctAnswer) ?? null;
    this.selectedOption = null;
    this.answerStatus = null;
    this.statusMessage = 'Wähle das richtige Bild aus!';
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
  selectOption(index: number): void {
    if (this.answerStatus === 'correct') return;

    this.selectedOption = index;

    if (index === this.correctOptionIndex) {
      this.answerStatus = 'correct';
      this.statusMessage = 'Richtig! 🎉';
      setTimeout(() => this.nextQuestion(), 1000);
    } else {
      this.answerStatus = 'wrong';
      this.statusMessage = 'Leider falsch. Versuche es noch einmal.';
    }
  }


  nextQuestion(): void {
    if (!this.task) return;

    if (this.currentQuestionIndex < this.task.questions.length - 1) {
      this.currentQuestionIndex++;
      this.loadCurrentFillFormQuestion();
    } else {
      this.isCompleted = true;
      this.completeLearningTask();
    }
  }


  startGame(): void {
    this.gameSequence = [];
    this.currentLevel = 0;
    this.startRound();
  }

  startRound(): void {
    this.playerSequence = [];
    this.isMemorizing = true;
    this.isRecalling = false;
    this.resetFeedbackState();
    this.statusMessage = 'Merke dir die Zahlenfolge...';


    const newNumber = Math.floor(Math.random() * 9) + 1;
    this.gameSequence.push(newNumber);

    this.showSequence();
  }

  async showSequence(): Promise<void> {
    for (let i = 0; i < this.gameSequence.length; i++) {
      this.highlightedIndex = i;
      await this.delay(700);
      this.highlightedIndex = null;
      await this.delay(300);
    }

    this.isMemorizing = false;
    this.isRecalling = true;
    this.statusMessage = 'Wiederhole die Folge!';
  }

  selectNumber(number: number): void {
    if (!this.isRecalling) {
      return;
    }

    this.lastClickedNumber = number;

    const correct = (number === this.gameSequence[this.playerSequence.length]);
    this.playerSequence.push(number);

    if (correct) {
      this.lastClickedStatus = 'correct';
      if (this.playerSequence.length === this.gameSequence.length) {
        this.answerStatus = 'correct';
        this.statusMessage = 'Richtig! 🎉 Bereit für die nächste Runde...';
        this.currentLevel++;
        setTimeout(() => {
          this.startRound();
        }, 1500);
      }
    } else {
      this.lastClickedStatus = 'wrong';
      this.answerStatus = 'wrong';
      this.statusMessage = `Leider Falsch! Du hast Level ${this.currentLevel} erreicht. Versuche es noch einmal.`;
      this.isRecalling = false;
      setTimeout(() => {
        this.resetFeedbackState();
      }, 1500);
    }


    setTimeout(() => {
      this.lastClickedNumber = null;
      this.lastClickedStatus = null;
    }, 500);
  }

  delay(ms: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  onFinishTask(): void {
    this.isCompleted = true;
    this.statusMessage = `Spiel beendet. Du hast Level ${this.currentLevel} erreicht.`;
    this.completeLearningTask();
  }

  private completeLearningTask(): void {
    if (this.childId && this.task) {
      this.learningService.completeTask(this.childId, this.task.id).subscribe({
        next: () => console.log('Logic-Aufgabe erfolgreich abgeschlossen!'),
        error: (err) => console.error('Fehler beim Abschluss der Logic-Aufgabe', err),
      });
    }
  }

  resetFeedbackState(): void {
    this.answerStatus = null;
    this.statusMessage = '';
    this.lastClickedNumber = null;
    this.lastClickedStatus = null;
  }
}
