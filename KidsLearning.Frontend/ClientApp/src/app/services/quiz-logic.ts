import {Injectable} from '@angular/core';
import {LearningTask} from '../models/learning-task';
import {Question} from '../models/question';
import {S} from '@angular/cdk/keycodes';

export type AnswerStatus = 'correct' | 'wrong' | null;

export interface PuzzleTile {
  id: number;
  backgroundPosition: string;
  correctPosition: number;
  currentPosition: number;
  selected: boolean;
}


@Injectable({
  providedIn: 'root'
})
export class QuizLogic {

  // Randomize Array
  shuffleArray<T>(array: T[]): T[] {
    const copy = [...array];
    for (let i = copy.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [copy[i], copy[j]] = [copy[j], copy[i]];
    }
    return copy;
  }

  // ----------------------------------------------------------------

  // Mathe Logik
  prepareMathTask(task: LearningTask, difficulty: string): LearningTask {
    let filtered = task.questions.filter(q => q.difficulty === difficulty);
    filtered = this.shuffleArray(filtered);
    filtered.forEach(q => {
      q.options = this.shuffleArray(q.options);
    });

    return {
      ...task,
      questions: filtered
    };
  }

  checkMathAnswer(
    question: Question,
    selectedAnswer: string
  ): { status: AnswerStatus; message: string } {
    if (selectedAnswer === question.correctAnswer) {
      return {status: 'correct', message: 'Richtig! 🎉'};
    } else {
      return {status: 'wrong', message: 'Falsch, versuche es noch einmal.'};
    }
  }

  selectMathAnswer(task: LearningTask, currentIndex: number, answer: string, answeredQuestions: boolean[]) {
    const currentQuestion = task.questions[currentIndex];
    answeredQuestions[currentIndex] = true;

    const result = this.checkMathAnswer(currentQuestion, answer);

    return {
      answerStatus: result.status,
      statusMessage: result.message,
      answeredQuestions,
      completed: result.status === 'correct'
    };
  }

  // ---------------------------------------------------------------------------------------------------

  // Buchstaben land Logik

  prepareLetterTask(task: LearningTask, childDifficulty: string): LearningTask {
    task.questions = task.questions.filter(q => q.difficulty === childDifficulty);

    task.questions = this.shuffleArray(task.questions);
    task.questions.forEach(q => {
      q.options = this.shuffleArray(q.options);
    });

    return task;
  }

  checkSpelling(letter: string, spelledWord: string[], currentQuestion: Question) {
    const nextIndex = spelledWord.findIndex(l => l === '');
    if (nextIndex === -1) return {spelledWord, correct: false, completed: false};

    if (letter === currentQuestion.correctAnswer.charAt(nextIndex)) {
      spelledWord[nextIndex] = letter;
      const completed = spelledWord.join('') === currentQuestion.correctAnswer;
      return {spelledWord, correct: true, completed};
    } else {
      return {spelledWord, correct: false, completed: false};
    }
  }

  checkAnswer(answer: string, currentQuestion: Question) {
    return answer === currentQuestion.correctAnswer;
  }

  checkGapFill(input: string, currentQuestion: Question) {
    return input.trim().toLowerCase() === currentQuestion.correctAnswer.toLowerCase();
  }

  selectAnswer(answer: string, currentQuestion: any) {
    const isCorrect = answer === currentQuestion.correctAnswer;

    return {
      correct: isCorrect,
      message: isCorrect ? 'Richtig! 🎉' : 'Falsch, versuche es noch einmal. 🤔',
      completed: isCorrect
    };
  }

  selectLetterSpelling(letter: string, spelledWord: string, currentQuestion: any) {
    const newSpelledWord = spelledWord + letter;
    const isCorrect = currentQuestion.correctAnswer.startsWith(newSpelledWord);

    return {
      spelledWord: newSpelledWord,
      correct: isCorrect,
      completed: isCorrect && newSpelledWord === currentQuestion.correctAnswer
    };
  }

  selectTypedAnswer(typedAnswer: string, currentQuestion: any) {
    const isCorrect = typedAnswer.trim().toLowerCase() === currentQuestion.correctAnswer.trim().toLowerCase();

    return {
      correct: isCorrect,
      message: isCorrect ? 'Richtig! 🎉' : 'Falsch, versuche es noch einmal. 🤔',
      completed: isCorrect
    };
  }

  selectLetter(letter: string, currentWord: string, spelledWord: string[]): {
    spelledWord: string[],
    correct: boolean,
    completed: boolean,
    message: string
  } {
    const nextLetterIndex = spelledWord.findIndex(l => l === '');
    if (nextLetterIndex === -1) {
      return {spelledWord, correct: false, completed: false, message: ''};
    }

    if (letter === currentWord.charAt(nextLetterIndex)) {
      spelledWord[nextLetterIndex] = letter;
      const completed = spelledWord.join('') === currentWord;
      return {
        spelledWord,
        correct: true,
        completed,
        message: completed ? 'Richtig!' : ''
      };
    } else {
      return {spelledWord, correct: false, completed: false, message: 'Falsch, versuche es nochmal. 🤔'};
    }
  }


  // ------------------------------------------------------------------------

  // Englisch Logik

  checkEnglishAnswer(question: Question, selectedAnswer: string) {
    const correct = selectedAnswer.trim().toLowerCase() === question.correctAnswer.trim().toLowerCase();
    return {correct, message: correct ? 'Richtig! 🎉' : 'Falsch, versuche es noch einmal. 🤔'};
  }

  selectEnglishAnswer(task: LearningTask, currentIndex: number, selectedAnswer: string, answeredQuestions: boolean[]) {
    const question = task.questions[currentIndex];
    answeredQuestions[currentIndex] = true;
    const result = this.checkEnglishAnswer(question, selectedAnswer);
    return {
      answerStatus: result.correct ? 'correct' as AnswerStatus : 'wrong' as AnswerStatus,
      statusMessage: result.message,
      answeredQuestions,
      completed: result.correct
    };
  }

  createBatches(task: LearningTask, batchSize: number = 8) {
    const totalBatches = Math.ceil(task.questions.length / batchSize);
    const batches: Question[][] = [];
    for (let i = 0; i < totalBatches; i++) {
      const start = i * batchSize;
      const end = start + batchSize;
      batches.push(task.questions.slice(start, end));
    }
    return batches;
  }

  shuffleBatch(batch: Question[]) {
    const german = this.shuffleArray(batch.map(q => q.text));
    const english = this.shuffleArray(batch.map(q => q.correctAnswer));
    return {german, english};
  }

  checkEnglishMatching(selectedDe: string, selectedEn: string, batch: Question[]) {
    return batch.some(q =>
      q.text.trim().toLowerCase() === selectedDe.trim().toLowerCase() &&
      q.correctAnswer.trim().toLowerCase() === selectedEn.trim().toLowerCase()
    );
  }

  // --------------------------------------------------------------------------

  // Logik Logik

  selectFillFormOption(
    question: Question,
    selectedIndex: number
  ): { status: AnswerStatus; message: string } {
    if (!question.options || question.options.length === 0) {
      return {status: null, message: 'Keine Optionen vorhanden'};
    }

    const correctIndex = question.options.findIndex(opt => opt === question.correctAnswer);
    const status: AnswerStatus = selectedIndex === correctIndex ? 'correct' : 'wrong';
    const message = status === 'correct' ? 'Richtig! 🎉' : 'Leider falsch. Versuche es noch einmal.';

    return {status, message};
  }

  // -------------------------------------------------------------------

  // Spaß-Land Logik

  createPuzzle(rows: number, cols: number, tileSize: number): PuzzleTile[] {
    const totalTiles = rows * cols;
    const tiles: PuzzleTile[] = [];

    for (let i = 0; i < totalTiles; i++) {
      const row = Math.floor(i / cols);
      const col = i % cols;

      tiles.push({
        id: i,
        backgroundPosition: `-${col * tileSize}px -${row * tileSize}px`,
        correctPosition: i,
        currentPosition: i,
        selected: false
      });
    }
    return tiles;
  }

  shuffleTiles(tiles: PuzzleTile[]): PuzzleTile[] {
    const shuffledTiles = [...tiles];
    for (let i = shuffledTiles.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [shuffledTiles[i], shuffledTiles[j]] = [shuffledTiles[j], shuffledTiles[i]];
    }

    shuffledTiles.forEach((tile, index) => tile.currentPosition = index);
    return shuffledTiles;
  }

  swapTiles(tiles: PuzzleTile[], tile1: PuzzleTile, tile2: PuzzleTile): PuzzleTile[] {
    const index1 = tiles.indexOf(tile1);
    const index2 = tiles.indexOf(tile2);

    if (index1 !== -1 && index2 !== -1) {
      [tiles[index1], tiles[index2]] = [tiles[index2], tiles[index1]];
    }
    tiles.forEach((tile, index) => tile.currentPosition = index);
    return tiles;
  }

  checkPuzzleCompletion(tiles: PuzzleTile[]): boolean {
    return tiles.every(tile => tile.correctPosition === tile.currentPosition);
  }
}
