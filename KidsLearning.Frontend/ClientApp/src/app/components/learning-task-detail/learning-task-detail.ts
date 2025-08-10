import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TasksService } from '../../services/tasks.service';
import { LearningTask } from '../../models/learning-task';
import { CommonModule } from '@angular/common';
import { LearningService } from '../../services/learning.service';

@Component({
  selector: 'app-learning-task-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './learning-task-detail.html',
  styleUrl: './learning-task-detail.css'
})
export class LearningTaskDetail implements OnInit {
  task: LearningTask | null = null;
  currentQuestionIndex = 0;
  isCompleted = false;

  answerStatus: 'correct' | 'wrong' | null = null;
  statusMessage: string = '';
  isWaitingForNext = false;
  childId: number | null = null; // Hinzugefügte Eigenschaft

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService
  ) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId'); // childId aus der Route lesen

    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe(task => {
        this.task = task;
      });
    }
    if (childIdParam) {
      this.childId = +childIdParam; // childId in eine Zahl konvertieren
    }
  }

  selectAnswer(answer: string): void {
    // Dieser Guard Clause ist der Grund, warum nichts passiert.
    // Wenn `childId` null ist, wird die Methode sofort beendet.
    if (this.isWaitingForNext || !this.task || this.childId === null) {
      return;
    }

    const currentQuestion = this.task.questions[this.currentQuestionIndex];
    if (currentQuestion && answer === currentQuestion.correctAnswer) {
      this.answerStatus = 'correct';
      this.statusMessage = 'Richtig! 🎉';
      this.isWaitingForNext = true;

      if (this.currentQuestionIndex === this.task.questions.length - 1) {
        // Hier wird der Service aufgerufen, um die Aufgabe als abgeschlossen zu markieren
        this.learningService.completeTask(this.childId, this.task.id).subscribe({
          next: () => {
            console.log('Aufgabe erfolgreich als abgeschlossen markiert!');
            // Nach erfolgreichem Abschluss könntest du hier weitere Aktionen ausführen, z.B. zum Dashboard navigieren
          },
          error: (err) => {
            console.error('Fehler beim Markieren der Aufgabe als abgeschlossen', err);
          }
        });
      }

      setTimeout(() => {
        this.nextQuestion();
      }, 1500);
    } else {
      this.answerStatus = 'wrong';
      this.statusMessage = 'Falsch, versuche es noch einmal. 🤔';
    }
  }

  nextQuestion(): void {
    this.answerStatus = null;
    this.isWaitingForNext = false;
    this.statusMessage = '';
    
    if (this.task && this.currentQuestionIndex < this.task.questions.length - 1) {
      this.currentQuestionIndex++;
    } else {
      this.isCompleted = true;
    }
  }
}