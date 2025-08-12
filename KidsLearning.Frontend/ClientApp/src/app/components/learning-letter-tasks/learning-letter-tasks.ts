import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

import { TasksService } from '../../services/tasks.service';
import { LearningService } from '../../services/learning.service';
import { LearningTask, } from '../../models/learning-task';
import { Question } from '../../models/question';

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
  isWaitingForNext = false; // Hinzugefügt für die selectAnswer-Methode

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService
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
        if (letter === currentQuestion.correctAnswer[nextLetterIndex]) {
          this.spelledWord[nextLetterIndex] = letter;
          this.answerStatus = null;
          this.statusMessage = '';

          if (this.spelledWord.join('') === currentQuestion.correctAnswer) {
            this.answerStatus = 'correct';
            this.statusMessage = 'Richtig! 🎉';
            this.checkCompletion();
          }
        } else {
          this.answerStatus = 'wrong';
          this.statusMessage = 'Falsch, versuche es noch einmal. 🤔';
        }
      }
    }
  }

  // Neu hinzugefügt: Methode, um die Mathe-Fragen zu behandeln
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

      setTimeout(() => {
        this.nextQuestion();
      }, 1500);
    } else {
      this.answerStatus = 'wrong';
      this.statusMessage = 'Falsch, versuche es noch einmal. 🤔';
    }
  }

  checkCompletion(): void {
    if (this.currentQuestionIndex === (this.task?.questions.length ?? 0) - 1) {
      this.isCompleted = true;
      if (this.childId && this.task) { // Sicherstellen, dass die Werte nicht null sind
        this.learningService.completeTask(this.childId, this.task.id).subscribe({
          next: () => console.log('Aufgabe erfolgreich als abgeschlossen markiert!'),
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
    this.currentQuestionIndex++;
    if (this.isSpellingTask) {
      this.initializeSpellingTask();
    }
  }
}