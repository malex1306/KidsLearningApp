import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {CommonModule} from '@angular/common';
import {TasksService} from '../../services/tasks.service';
import {LearningService} from '../../services/learning.service';
import {LearningTask} from '../../models/learning-task';
import {RewardService} from '../../services/reward.service';
import {QuestionNavigationService} from '../../services/question-navigation.service';
import {Subscription} from 'rxjs';
import {FormsModule} from '@angular/forms';
import {ActiveChildService} from '../../services/active-child.service';
import {QuizLogic} from '../../services/quiz-logic';

@Component({
  selector: 'app-learning-letter-tasks',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './learning-letter-tasks.html',
  styleUrl: './learning-letter-tasks.css'
})
export class LearningLetterTasks implements OnInit, OnDestroy {
  isSpellingTask: boolean = false;
  isConnectingTask: boolean = false;
  task: LearningTask | null = null;
  childId: number | null = null;
  selectedLetter: string | null = null;
  selectedAnswer: string | null = null;
  currentQuestionIndex = 0;
  isCompleted = false;
  hasSkippedQuestions = false;

  spelledWord: string[] = [];
  statusMessage: string = '';
  answerStatus: 'correct' | 'wrong' | null = null;
  isWaitingForNext = false;
  typedAnswer: string = '';
  isGapFillTask: boolean = false;

  lastClickedLetter: string | null = null;
  lastClickedStatus: 'correct' | 'wrong' | null = null;
  answeredQuestions: boolean[] = [];

  exam: boolean = false;
  timerValue: number = 0;
  timerInterval: any = null;
  examfailed: boolean = false;
  private subscriptions = new Subscription();

  private speechSynth: SpeechSynthesis | null = null;
  private germanVoice: SpeechSynthesisVoice | null = null;

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService,
    private rewardService: RewardService,
    public navigationService: QuestionNavigationService,
    private activeChildService: ActiveChildService,
    private quizLogic: QuizLogic
  ) {
    if ('speechSynthesis' in window) {
      this.speechSynth = window.speechSynthesis;
      this.setGermanVoice();
    }
  }

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId');
    const examParam = this.route.snapshot.paramMap.get('exam');
    this.exam = examParam === 'true';

    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe(task => {
        const activeChild = this.activeChildService.activeChild();
        const childDifficulty = activeChild?.difficulty ?? 'Vorschule';
        if (childDifficulty) {
          task.questions = task.questions.filter(q => q.difficulty === childDifficulty);
        }
        task.questions = this.quizLogic.shuffleArray(task.questions);
        this.task = task;
        this.navigationService.setTask(task);
        this.answeredQuestions = new Array(task.questions.length).fill(false);
        if (this.task.title === 'Wörter buchstabieren') {
          this.isSpellingTask = true;
          this.initializeSpellingTask();
        } else if (this.task.title === 'Buchstaben verbinden') {
          this.isConnectingTask = true;
        } else if (this.task.title === 'Fülle die Lücken') {
          this.isGapFillTask = true;
        }
        if (this.exam) {
          this.startTimer(300); // Test Zeit in Sekunden können hier geändert werden! 💥💥💥💥💥💥💥💥💥💥
        }
      });
    }

    if (childIdParam) {
      this.childId = +childIdParam;
    }

    this.subscriptions.add(
      this.navigationService.currentIndex$.subscribe((index) => {
        this.currentQuestionIndex = index;
        this.resetAnswerState();
        this.readQuestion();
      })
    );
  }

  ngOnDestroy(): void {
    if (this.speechSynth) {
      this.speechSynth.cancel();
    }
    this.subscriptions.unsubscribe();
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
  }

  private setGermanVoice(): void {
    if (!this.speechSynth) return;

    this.speechSynth.onvoiceschanged = () => {
      const voices = this.speechSynth?.getVoices();
      if (voices) {
        this.selectVoice(voices);
      }
    };

    const voices = this.speechSynth.getVoices();
    if (voices.length > 0) {
      this.selectVoice(voices);
    }
  }

  private selectVoice(voices: SpeechSynthesisVoice[]): void {
    this.germanVoice = voices.find(voice =>
      voice.lang === 'de-DE' && voice.name.includes('Female')
    ) || voices.find(voice =>
      voice.lang === 'de-DE'
    ) || null;
  }


  initializeSpellingTask(): void {
    if (this.task && this.task.questions.length > 0) {
      this.spelledWord = new Array(this.task.questions[this.currentQuestionIndex].correctAnswer.length).fill('');
    }
  }

  selectLetter(letter: string): void {
    if (!this.task || this.childId === null) return;
    const currentQuestion = this.task.questions[this.currentQuestionIndex];
    if (!currentQuestion) return;

    const result = this.quizLogic.selectLetter(letter, currentQuestion.correctAnswer, [...this.spelledWord]);
    this.spelledWord = result.spelledWord;
    this.lastClickedLetter = letter;
    this.lastClickedStatus = result.correct ? 'correct' : 'wrong';
    this.answerStatus = result.correct ? 'correct' : 'wrong';
    this.statusMessage = result.message;

    if (result.completed) {
      this.checkCompletion();
    }

    setTimeout(() => {
      this.lastClickedStatus = null;
      this.lastClickedLetter = null;
    }, 500);
  }

  selectAnswer(answer: string): void {
    if (this.isWaitingForNext || !this.task) return;

    const currentQuestion = this.task.questions[this.currentQuestionIndex];
    if (!currentQuestion) return;

    const result = this.quizLogic.selectAnswer(answer, currentQuestion);

    this.selectedAnswer = answer;
    this.answeredQuestions[this.currentQuestionIndex] = true;
    this.answerStatus = result.correct ? 'correct' : 'wrong';
    this.statusMessage = result.message;

    if (result.completed) {
      this.isWaitingForNext = true;
      this.checkCompletion();
    }
  }

  checkCompletion(): void {
    if (this.currentQuestionIndex === (this.task?.questions.length ?? 0) - 1) {
      this.isCompleted = true;
      this.stopTimer();

      if (this.childId && this.task) {
        this.learningService.completeTask(this.childId, this.task.id).subscribe({
          next: () => {
            console.log('Aufgabe erfolgreich als abgeschlossen markiert!');

            this.rewardService.rewardChild(this.childId!, this.task!.id).subscribe({
              next: (updatedChildData) => {
                this.activeChildService.updateChildInfo({
                  starCount: updatedChildData.starCount,
                  totalStarsEarned: updatedChildData.totalStarsEarned,
                  avatarUrl: updatedChildData.avatarUrl,
                  unlockedAvatars: updatedChildData.unlockedAvatars,
                  progress: updatedChildData.progress
                });

                console.log('Belohnung erfolgreich vergeben und Daten aktualisiert.');
              },
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
    if (this.isGapFillTask) {
      this.typedAnswer = '';
    }
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
          console.error('Fehler beim Markieren der Aufgabe als abgeschlossen', err);
        },
      });
    }
  }

  onFinishTask(): void {
  const allQuestionsAnswered = this.answeredQuestions.every(answered => answered);
  this.isCompleted = true; 
  this.stopTimer();

  if (allQuestionsAnswered) {
    this.hasSkippedQuestions = false; 
    this.completeLearningTask();
  } else {
    this.hasSkippedQuestions = true; 
  }
}

  checkTypedAnswer(): void {
    if (!this.task) return;

    const currentQuestion = this.task.questions[this.currentQuestionIndex];
    if (!currentQuestion) return;

    const result = this.quizLogic.selectTypedAnswer(this.typedAnswer, currentQuestion);

    this.answerStatus = result.correct ? 'correct' : 'wrong';
    this.statusMessage = result.message;
    if (result.correct) {
      this.answeredQuestions[this.currentQuestionIndex] = true;
      this.checkCompletion();
    }
  }

  readQuestion(): void {
    if (!this.speechSynth || !this.task || this.isCompleted) {
      return;
    }

    this.speechSynth.cancel();

    let textToRead = '';
    const currentQuestion = this.task.questions[this.currentQuestionIndex];

    if (this.isSpellingTask) {
      textToRead = `Wie buchstabiert man ${currentQuestion.correctAnswer}?`;
    } else if (this.isGapFillTask) {
      textToRead = `Fülle die Lücke: ${currentQuestion.text}`;
    } else {
      textToRead = currentQuestion.text;
    }

    const utterance = new SpeechSynthesisUtterance(textToRead);
    utterance.lang = 'de-DE';

    if (this.germanVoice) {
      utterance.voice = this.germanVoice;
    }

    utterance.pitch = 4;
    utterance.rate = 1.1;

    this.speechSynth.speak(utterance);
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
