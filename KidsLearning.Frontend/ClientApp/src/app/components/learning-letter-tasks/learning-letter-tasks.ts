// src/app/components/learning-letter-tasks/learning-letter-tasks.ts

import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

import { TasksService } from '../../services/tasks.service';
import { LearningService } from '../../services/learning.service';
import { LearningTask } from '../../models/learning-task';
import { RewardService } from '../../services/reward.service';
import { QuestionNavigationService } from '../../services/question-navigation.service';
import { Subscription } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { ActiveChildService } from '../../services/active-child.service';

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

  spelledWord: string[] = [];
  statusMessage: string = '';
  answerStatus: 'correct' | 'wrong' | null = null;
  isWaitingForNext = false;
  typedAnswer: string = '';
  isGapFillTask: boolean = false;

  lastClickedLetter: string | null = null;
  lastClickedStatus: 'correct' | 'wrong' | null = null;
  answeredQuestions: boolean[] = [];
  private subscriptions = new Subscription();

  private speechSynth: SpeechSynthesis | null = null;
  private germanVoice: SpeechSynthesisVoice | null = null;

  constructor(
    private route: ActivatedRoute,
    private tasksService: TasksService,
    private learningService: LearningService,
    private rewardService: RewardService,
    public navigationService: QuestionNavigationService,
    private activeChildService: ActiveChildService
  ) {
    if ('speechSynthesis' in window) {
      this.speechSynth = window.speechSynthesis;
      this.setGermanVoice();
    }
  }

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    const childIdParam = this.route.snapshot.paramMap.get('childId');

    if (taskId) {
      this.tasksService.getTaskById(+taskId).subscribe(task => {
        const activeChild = this.activeChildService.activeChild();
        const childDifficulty = activeChild?.difficulty ?? 'Vorschule';
        if (childDifficulty) {
          task.questions = task.questions.filter(q => q.difficulty === childDifficulty);
        }
        task.questions = this.shuffleArray(task.questions);
        this.task = task;
        this.navigationService.setTask(task);
        this.answeredQuestions = new Array(task.questions.length).fill(false);
        if (this.task.title === 'Wörter buchstabieren') {
          this.isSpellingTask = true;
          this.initializeSpellingTask();
        } else if (this.task.title === 'Buchstaben verbinden') {
          this.isConnectingTask = true;
        } else if (this.task.title === 'Fülle die Lücken'){
          this.isGapFillTask = true;
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

  private shuffleArray(array: any[]): any[] {
    let currentIndex = array.length, randomIndex;
    while (currentIndex !== 0) {
      randomIndex = Math.floor(Math.random() * currentIndex);
      currentIndex--;
      [array[currentIndex], array[randomIndex]] = [
        array[randomIndex], array[currentIndex]];
    }
    return array;
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
    if(this.isGapFillTask){
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
    if (allQuestionsAnswered) {
      this.statusMessage = 'Gut gemacht! Du hast alle Fragen beantwortet. Das Ergebnis wurde gespeichert.';
      this.completeLearningTask();
    } else {
      this.statusMessage = 'Du hast nicht alle Fragen beantwortet. Das Ergebnis wird nicht gespeichert.';
    }
  }

  checkTypedAnswer(): void {
    if (!this.task) return;
    const currentQuestion = this.task.questions[this.currentQuestionIndex];
    if (this.typedAnswer.trim().toLowerCase() === currentQuestion.correctAnswer.toLowerCase()) {
      this.answerStatus = 'correct';
      this.statusMessage = 'Richtig! 🎉';
      this.answeredQuestions[this.currentQuestionIndex] = true;
      this.checkCompletion();
    } else {
      this.answerStatus = 'wrong';
      this.statusMessage = 'Falsch, versuche es nochmal.';
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

    // Verwenden Sie die ausgewählte Stimme, falls vorhanden
    if (this.germanVoice) {
      utterance.voice = this.germanVoice;
    }

    // Passen Sie Tonhöhe und Geschwindigkeit für einen kindlicheren Klang an
    utterance.pitch = 4;
    utterance.rate = 1.1;

    this.speechSynth.speak(utterance);
  }
}
