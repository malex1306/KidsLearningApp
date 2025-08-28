import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {TasksService} from '../../services/tasks.service';
import {LearningTask} from '../../models/learning-task';
import {CommonModule} from '@angular/common';
import {LearningService} from '../../services/learning.service';
import {QuestionNavigationService} from '../../services/question-navigation.service';
import {Subscription} from 'rxjs';
import {ActiveChildService} from '../../services/active-child.service';
import {QuizLogic} from '../../services/quiz-logic';

@Component({
  selector: 'app-learning-task-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './learning-task-detail.html',
  styleUrl: './learning-task-detail.css',
})
export class LearningTaskDetail implements OnInit, OnDestroy {
  task: LearningTask | null = null;
  currentQuestionIndex = 0;
  isCompleted = false;
  answerStatus: 'correct' | 'wrong' | null = null;
  statusMessage: string = '';
  isWaitingForNext = false;
  childId: number | null = null;
  selectedAnswer: string | null = null;
  answeredQuestions: boolean[] = [];
  exam: boolean = false;
  timerValue: number = 0;
  timerInterval: any = null;

  examfailed: boolean = false;

  private subscriptions = new Subscription();

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService,
    public navigationService: QuestionNavigationService,
    private activeChildService: ActiveChildService,
    private quizLogic: QuizLogic
  ) {
  }

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId');
    const examParam = this.route.snapshot.paramMap.get('exam');

    if (childIdParam) {
      this.childId = +childIdParam;
    }

    this.exam = examParam === 'true';
    this.examfailed = false;

    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe((task) => {
        const activeChild = this.activeChildService.activeChild();
        const childDifficulty = activeChild?.difficulty ?? 'Vorschule';

        this.task = this.quizLogic.prepareMathTask(task, childDifficulty);

        this.navigationService.setTask(this.task);
        this.answeredQuestions = new Array(this.task.questions.length).fill(false);
        if (this.exam) {
          this.startTimer(300); // Test Zeit in Sekunden können hier geändert werden! ✅✅✅
        }
      });
    }

    this.subscriptions.add(
      this.navigationService.currentIndex$.subscribe((index) => {
        this.currentQuestionIndex = index;
        this.resetAnswerState();
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
    this.stopTimer();
  }

  selectAnswer(answer: string): void {
    if (!this.task || this.childId === null || this.isWaitingForNext) return;

    const result = this.quizLogic.selectMathAnswer(
      this.task,
      this.currentQuestionIndex,
      answer,
      this.answeredQuestions
    );

    this.answerStatus = result.answerStatus;
    this.statusMessage = result.statusMessage;
    this.answeredQuestions = result.answeredQuestions;

    if (result.completed) {
      this.isWaitingForNext = true;
      setTimeout(() => {
        this.navigationService.nextQuestion();
      }, 1500);
    }
  }

  onFinishTask(): void {
    const allQuestionsAnswered = this.answeredQuestions.every(answered => answered);

    this.isCompleted = true;
    this.stopTimer();

    if (allQuestionsAnswered) {
      this.statusMessage = 'Gut gemacht! Du hast alle Fragen beantwortet. Das Ergebnis wurde gespeichert.';
      this.completeLearningTask();
    } else {
      this.statusMessage = 'Du hast nicht alle Fragen beantwortet. Das Ergebnis wird nicht gespeichert.';
    }
  }

  resetAnswerState(): void {
    this.answerStatus = null;
    this.isWaitingForNext = false;
    this.statusMessage = '';
    this.selectedAnswer = null;
  }

  private completeLearningTask(): void {
    if (this.childId && this.task) {
      this.learningService.completeTask(this.childId, this.task.id).subscribe({
        next: () => {
          console.log('Aufgabe erfolgreich abgeschlossen markiert!');
        },
        error: (err) => {
          console.error(
            'Fehler beim Markieren der Aufgabe als abgeschlossen',
            err
          );
        },
      });
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

  stopTimer(): void {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
      this.timerInterval = null;
    }
  }
}
