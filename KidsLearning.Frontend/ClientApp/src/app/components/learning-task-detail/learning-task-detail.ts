import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TasksService } from '../../services/tasks.service';
import { LearningTask } from '../../models/learning-task';
import { CommonModule } from '@angular/common';

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

  constructor(private route: ActivatedRoute, private tasksService: TasksService) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe(task => {
        this.task = task;
      });
    }
  }

  selectAnswer(answer: string): void {
    if (this.isWaitingForNext) {
      return;
    }

    const currentQuestion = this.task?.questions[this.currentQuestionIndex];
    if (currentQuestion && answer === currentQuestion.correctAnswer) {
      this.answerStatus = 'correct';
      this.statusMessage = 'Richtig! 🎉';
      this.isWaitingForNext = true;
      setTimeout(() => {
        this.nextQuestion();
      }, 1500); // 1.5 Sekunden warten, bevor es weitergeht
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