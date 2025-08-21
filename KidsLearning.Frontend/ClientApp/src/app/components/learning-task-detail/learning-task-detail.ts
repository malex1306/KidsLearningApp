import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TasksService } from '../../services/tasks.service';
import { LearningTask } from '../../models/learning-task';
import { CommonModule } from '@angular/common';
import { LearningService } from '../../services/learning.service';
import { QuestionNavigationService } from '../../services/question-navigation.service';
import { Subscription } from 'rxjs';

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
  private subscriptions = new Subscription();

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService,
    public navigationService: QuestionNavigationService
  ) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId');

    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe((task) => {
        task.questions = this.shuffleArray(task.questions);
        task.questions.forEach(q => {
          q.options = this.shuffleArray(q.options);
        });
        this.task = task;
        this.navigationService.setTask(task);

        this.answeredQuestions = new Array(task.questions.length).fill(false);
      });
    }
    if (childIdParam) {
      this.childId = +childIdParam;
    }

    this.subscriptions.add(
      this.navigationService.currentIndex$.subscribe((index) => {
        this.currentQuestionIndex = index;
        this.resetAnswerState();
      })
    );
  }

  private shuffleArray<T>(array: T[]): T[] {
    const copy = [...array];
    for (let i = copy.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [copy[i], copy[j]] = [copy[j], copy[i]];
    }
    return copy;
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  selectAnswer(answer: string): void {
    if (this.isWaitingForNext || !this.task || this.childId === null) {
      return;
    }
    this.selectedAnswer = answer;
    this.answeredQuestions[this.currentQuestionIndex] = true;

    const currentQuestion = this.task.questions[this.currentQuestionIndex];
    if (currentQuestion && answer === currentQuestion.correctAnswer) {
      this.answerStatus = 'correct';
      this.statusMessage = 'Richtig! 🎉';
      this.isWaitingForNext = true;

      setTimeout(() => {
        this.navigationService.nextQuestion();
      }, 1500);
    } else {
      this.answerStatus = 'wrong';
      this.statusMessage = 'Falsch, versuche es noch einmal.';
    }
  }

  onFinishTask(): void {
    const allQuestionsAnswered = this.answeredQuestions.every(answered => answered);

    this.isCompleted = true;

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
}
