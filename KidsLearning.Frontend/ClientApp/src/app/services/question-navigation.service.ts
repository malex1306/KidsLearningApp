import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {LearningTask} from '../models/learning-task';

@Injectable({
  providedIn: 'root'
})
export class QuestionNavigationService {
  private currentTaskSource = new BehaviorSubject<LearningTask | null>(null);
  currentTask$ = this.currentTaskSource.asObservable();

  private currentIndexSource = new BehaviorSubject<number>(0);
  currentIndex$ = this.currentIndexSource.asObservable();

  constructor() {
  }

  setTask(task: LearningTask): void {
    this.currentTaskSource.next(task);
    this.currentIndexSource.next(0);
  }

  nextQuestion(): void {
    const currentTask = this.currentTaskSource.getValue();
    const currentIndex = this.currentIndexSource.getValue();

    if (currentTask && currentIndex < currentTask.questions.length - 1) {
      this.currentIndexSource.next(currentIndex + 1);
    }
  }

  previousQuestion(): void {
    const currentIndex = this.currentIndexSource.getValue();

    if (currentIndex > 0) {
      this.currentIndexSource.next(currentIndex - 1);
    }
  }

}

