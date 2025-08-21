import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

import { TasksService } from '../../services/tasks.service';
import { LearningService } from '../../services/learning.service';
import { LearningTask, } from '../../models/learning-task';
import { Question } from '../../models/question';
import { RewardService } from '../../services/reward.service';
import { QuestionNavigationService } from '../../services/question-navigation.service'; 
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-learning-letter-tasks',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './learning-letter-tasks.html',
  styleUrl: './learning-letter-tasks.css'
})
export class LearningLetterTasks implements OnInit {
  isSpellingTask: boolean = false;
  isConnectingTask: boolean = false;
  task: LearningTask | null = null;
  childId: number | null = null;
  selectedLetter: string | null = null;
  selectedAnswer: string | null = null;
  currentQuestionIndex = 0;
  isCompleted = false;

  spelledWord: string[] = [];
  statusMessage: string = '';
  answerStatus: 'correct' | 'wrong' | null = null;
  isWaitingForNext = false;

  lastClickedLetter: string | null = null;
  lastClickedStatus: 'correct' | 'wrong' | null = null;
  answeredQuestions: boolean[] = [];
  private subscriptions = new Subscription();

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService,
    private rewardService: RewardService,
    public navigationService: QuestionNavigationService 
  ) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId');

    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe(task => {
        this.task = task;
        this.navigationService.setTask(task);
        this.answeredQuestions = new Array(task.questions.length).fill(false);
        if (this.task.title === 'Wörter buchstabieren') {
          this.isSpellingTask = true;
          this.initializeSpellingTask();
        } else if (this.task.title === 'Buchstaben verbinden') {
          this.isConnectingTask = true;
        }
      });
    }

    if (childIdParam) {
      this.childId = +childIdParam;
    }

    this.subscriptions.add(
    this.navigationService.currentIndex$.subscribe((index) =>{
      this.currentQuestionIndex = index;
      this.resetAnswerState();
    })
  )
  }

  

  initializeSpellingTask(): void {
    if (this.task && this.task.questions.length > 0) {
      this.spelledWord = new Array(this.task.questions[this.currentQuestionIndex].correctAnswer.length).fill('');
    }
  }

  selectLetter(letter: string): void {
    if (!this.task || this.childId === null) {
      return;
    }
    this.selectedLetter = letter;
    this.answeredQuestions[this.currentQuestionIndex] = true;

    const currentQuestion = this.task.questions[this.currentQuestionIndex];
    if (currentQuestion) {
      const nextLetterIndex = this.spelledWord.findIndex(l => l === '');
      if (nextLetterIndex !== -1) {
        this.lastClickedLetter = letter;
        
        if (letter === currentQuestion.correctAnswer.charAt(nextLetterIndex)) {
          this.spelledWord[nextLetterIndex] = letter;
          this.lastClickedStatus = 'correct';
          this.answerStatus = 'correct';
          this.statusMessage = '';

          if (this.spelledWord.join('') === currentQuestion.correctAnswer) {
            this.statusMessage = 'Richtig! ';
            this.checkCompletion();
          }
        } else {
          this.lastClickedStatus = 'wrong';
          this.answerStatus = 'wrong';
          this.statusMessage = 'Falsch, versuche es noch einmal. 🤔';
        }

        setTimeout(() => {
          this.lastClickedStatus = null;
          this.lastClickedLetter = null;
        }, 500);
      }
    }
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

      this.checkCompletion();
    } else {
      this.answerStatus = 'wrong';
      this.statusMessage = 'Falsch, versuche es noch einmal. 🤔';
    }
  }

  checkCompletion(): void {
    if (this.currentQuestionIndex === (this.task?.questions.length ?? 0) - 1) {
      this.isCompleted = true;
      if (this.childId && this.task) {
        this.learningService.completeTask(this.childId, this.task.id).subscribe({
          next: () => {
            console.log('Aufgabe erfolgreich als abgeschlossen markiert!');
            this.rewardService.rewardChild(this.childId!, this.task!.id)
              .subscribe({
                next: () => console.log('Belohnung erfolgreich vergeben'),
                error: (err) => console.error('Fehler beim Vergeben der Belohnung', err)
              });
          },
          error: (err) => console.error('Fehler beim Markieren der Aufgabe als abgeschlossen', err)
        });
      }
    } else {
      setTimeout(() => this.navigationService.nextQuestion(), 1000);
    }
  }

  resetAnswerState(): void {
    this.answerStatus = null;
    this.statusMessage = '';
    this.isWaitingForNext = false;
    if (this.isSpellingTask) {
      this.initializeSpellingTask();
    }
  }

  goToPreviousQuestion(): void {
  if (this.currentQuestionIndex > 0) {
    this.navigationService.previousQuestion(); 
  }
}

goToNextQuestion(): void {
  if (this.currentQuestionIndex < (this.task?.questions.length ?? 0) - 1) {
    this.navigationService.nextQuestion();
  }
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

}