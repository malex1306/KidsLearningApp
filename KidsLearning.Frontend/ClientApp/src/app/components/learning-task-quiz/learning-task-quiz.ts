import { Component, OnInit, computed } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { Question } from '../../models/question';
import {CommonModule} from '@angular/common';
import {RouterLink, ActivatedRoute, RouterModule} from '@angular/router';
import {LearningTaskEnglish} from '../learning-task-english/learning-task-english';
import {LearningLetterTasks} from '../learning-letter-tasks/learning-letter-tasks';
import {LearningTaskDetail} from '../learning-task-detail/learning-task-detail';
import {LogicTask} from '../logic-task/logic-task';
import { ActiveChildService } from '../../services/active-child.service';
import { QuizLogic, AnswerStatus } from '../../services/quiz-logic';
import { LearningTask } from '../../models/learning-task';


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

  activeChild = computed(() => this.activeChildService.activeChild());

  constructor(private route: ActivatedRoute, private tasksService: TasksService, private activeChildService: ActiveChildService, private quizLogic: QuizLogic) {}

  ngOnInit(): void {
    const activeChild = this.activeChildService.activeChild();

    // Alle Fragen laden
    this.tasksService.getAllQuestions().subscribe({
      next: (questions: Question[]) => {
        this.questions = this.shuffleArray(questions);
      },
      error: err => console.error('API error:', err)
    });

    // Task-Titel holen für ngSwitch / Typen
    const taskId = 13;
    this.tasksService.getTaskById(taskId).subscribe(task => {
      this.currentTaskTitle = task.title;
    });
  }


  get currentQuestion(): Question {
    return this.questions[this.currentQuestionIndex];
  }

  selectAnswer(option: string, selectedIndex?: number) {
    if (!this.currentQuestion) return;

    const task: LearningTask = {
      id: this.currentQuestion.learningTaskId,
      title: this.currentTaskTitle || 'Unbekannter Task',
      description: '',
      subject: '',
      questions: this.questions
    };

    switch (this.currentQuestion.learningTaskId) {
      // Mathe Aufgaben
      case 1:
      case 2: {
        const res = this.quizLogic.selectMathAnswer(task, this.currentQuestionIndex, option, []);
        this.answerStatus = res.answerStatus;
        this.feedbackMessage = res.statusMessage;
        break;
      }

      // Formen Aufgaben
      case 3:
      case 12: {
        if (selectedIndex === undefined) return;
        const res = this.quizLogic.selectFillFormOption(this.currentQuestion, selectedIndex);
        this.answerStatus = res.status;
        this.feedbackMessage = res.message;
        break;
      }

      // Buchstaben-land
      case 4:
      case 5:
      case 6:
      case 10: {
        const res = this.quizLogic.selectTypedAnswer(option, this.currentQuestion);
        this.answerStatus = res.correct ? 'correct' : 'wrong';
        this.feedbackMessage = res.message;
        break;
      }

      // Englisch
      case 7:
      case 9: {
        const res = this.quizLogic.selectEnglishAnswer(task, this.currentQuestionIndex, option, []);
        this.answerStatus = res.answerStatus;
        this.feedbackMessage = res.statusMessage;
        break;
      }

      case 8: {
        // Deutsch/Englisch Matching
        const batch = task.questions.slice(); // optional: batch logic
        const correct = this.quizLogic.checkEnglishMatching(
          this.currentQuestion.text,
          option,
          batch
        );
        this.answerStatus = correct ? 'correct' : 'wrong';
        this.feedbackMessage = correct
          ? 'Richtig! 🎉'
          : 'Falsch, versuche es noch einmal.';
        break;
      }

      // Zahlenkombinationen
      case 11:
        // Logik bleibt in Component, nicht auslagern
        break;

      default:
        this.answerStatus = option === this.currentQuestion.correctAnswer ? 'correct' : 'wrong';
        this.feedbackMessage = this.answerStatus === 'correct' ? 'Richtig! 🎉' : 'Falsch, versuche es noch einmal.';
        break;
    }
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
  prevQuestion() {
    this.selectedAnswer = null;
    this.feedbackMessage = '';
    if (this.currentQuestionIndex > 0) {
      this.currentQuestionIndex--;
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
