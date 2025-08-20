import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

import { TasksService } from '../../services/tasks.service';
import { LearningService } from '../../services/learning.service';
import { LearningTask, } from '../../models/learning-task';
import { Question } from '../../models/question';
import { RewardService } from '../../services/reward.service'; // NEU: RewardService importieren

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
  currentQuestionIndex = 0;
  isCompleted = false;

  spelledWord: string[] = [];
  statusMessage: string = '';
  answerStatus: 'correct' | 'wrong' | null = null;
  isWaitingForNext = false;

  lastClickedLetter: string | null = null;
  lastClickedStatus: 'correct' | 'wrong' | null = null;

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService,
    private rewardService: RewardService 
  ) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId');

    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe(task => {
        this.task = task;
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
    if (!this.task || this.childId === null) {
      return;
    }

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
            // Belohnungssystem-Endpunkt aufrufen
            this.rewardService.rewardChild(this.childId!, this.task!.id)
              .subscribe({
                next: () => console.log('Belohnung erfolgreich vergeben!'),
                error: (err) => console.error('Fehler beim Vergeben der Belohnung', err)
              });
          },
          error: (err) => console.error('Fehler beim Markieren der Aufgabe als abgeschlossen', err)
        });
      }
    } else {
      setTimeout(() => this.nextQuestion(), 1500);
    }
  }

  nextQuestion(): void {
    this.answerStatus = null;
    this.statusMessage = '';
    this.isWaitingForNext = false;
    this.currentQuestionIndex++;
    if (this.isSpellingTask) {
      this.initializeSpellingTask();
    }
  }
}