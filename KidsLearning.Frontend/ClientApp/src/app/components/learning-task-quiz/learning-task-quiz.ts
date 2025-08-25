import { Component, OnInit } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { Question } from '../../models/question';
import {CommonModule} from '@angular/common';
import {RouterLink, ActivatedRoute, RouterModule} from '@angular/router';
import {LearningTaskEnglish} from '../learning-task-english/learning-task-english';
import {LearningLetterTasks} from '../learning-letter-tasks/learning-letter-tasks';
import {LearningTaskDetail} from '../learning-task-detail/learning-task-detail';
import {LogicTask} from '../logic-task/logic-task';


@Component({
  selector: 'app-learning-task-quiz',
  imports: [CommonModule, RouterModule, LearningTaskEnglish, LearningLetterTasks, LearningTaskDetail, LogicTask, RouterLink],
  templateUrl: './learning-task-quiz.html',
  styleUrls: ['./learning-task-quiz.css']
})
export class LearningTaskQuiz implements OnInit {
  questions: Question[] = [];
  currentQuestionIndex = 0;
  selectedAnswer: string | null = null;
  isCompleted = false;
  feedbackMessage = '';
  learningTaskId: number | null = null;
  currentTaskTitle: string | null = null;
  answerStatus: 'correct' | 'wrong' | null = null;
  isWaitingForNext = false;
  spelledWord: string[] = [];
  lastClickedLetter: string | null = null;
  lastClickedStatus: 'correct' | 'wrong' | null = null;
  selectedLetter: string | null = null;

  constructor(private route: ActivatedRoute, private tasksService: TasksService) {}

  ngOnInit(): void {
    this.tasksService.getAllQuestions().subscribe({
      next: (questions: Question[]) => {
        console.log('API returned questions:', questions);
        this.questions = this.shuffleArray(questions);
        console.log('Questions after shuffle:', this.questions);
      },
      error: err => console.error('API error:', err)


    });
    const taskId = 13;
    this.tasksService.getTaskById(taskId).subscribe(task => {
      this.currentTaskTitle = task.title; // nur für ngSwitch im Template
    });
  }


  get currentQuestion(): Question {
    return this.questions[this.currentQuestionIndex];
  }

  selectAnswer(answer: string) {
    this.selectedAnswer = answer;
    this.feedbackMessage =
      answer === this.currentQuestion.correctAnswer
        ? 'Richtig! 🎉'
        : 'Falsch, versuche es noch einmal. 🤔';
  }

  nextQuestion() {
    this.selectedAnswer = null;
    this.feedbackMessage = '';
    if (this.currentQuestionIndex < this.questions.length - 1) {
      this.currentQuestionIndex++;
    } else {
      this.isCompleted = true;
    }
  }

  private shuffleArray<T>(array: T[]): T[] {
    const arr = [...array];
    for (let i = arr.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [arr[i], arr[j]] = [arr[j], arr[i]];
    }
    return arr;
  }
}
