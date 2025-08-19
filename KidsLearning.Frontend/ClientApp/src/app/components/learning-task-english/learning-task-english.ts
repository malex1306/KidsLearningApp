import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TasksService } from '../../services/tasks.service';
import { LearningService } from '../../services/learning.service';
import { LearningTask } from '../../models/learning-task';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-learning-task-english',
  imports: [CommonModule, RouterLink],
  templateUrl: './learning-task-english.html',
  styleUrls: ['./learning-task-english.css']
})
export class LearningTaskEnglish implements OnInit {
  task: LearningTask | null = null;
  currentQuestionIndex = 0;
  answerStatus: 'correct' | 'wrong' | null = null;
  statusMessage = '';
  isWaitingForNext = false;
  childId: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService
  ) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId');

    if (taskId) this.tasksService.getTaskById(+taskId).subscribe(task => this.task = task);
    if (childIdParam) this.childId = +childIdParam;
  }

  selectAnswer(answer: string): void {
    if (!this.task || this.isWaitingForNext) return;

    const currentQuestion = this.task.questions[this.currentQuestionIndex];
    if (!currentQuestion) return;

    if (answer === currentQuestion.correctAnswer) {
      this.answerStatus = 'correct';
      this.statusMessage = 'Correct! 🎉';
    } else {
      this.answerStatus = 'wrong';
      this.statusMessage = 'Wrong, try again. 🤔';
    }

    this.isWaitingForNext = true;
    setTimeout(() => {
      this.nextQuestion();
    }, 1500);
  }

  nextQuestion(): void {
    this.answerStatus = null;
    this.statusMessage = '';
    this.isWaitingForNext = false;

    if (this.task && this.currentQuestionIndex < this.task.questions.length - 1) {
      this.currentQuestionIndex++;
    }
  }

  get isCompleted(): boolean {
    return !!this.task && this.currentQuestionIndex >= this.task.questions.length;
  }
}
