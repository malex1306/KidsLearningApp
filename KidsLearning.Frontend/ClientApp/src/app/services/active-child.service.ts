import {Injectable, signal, Signal} from '@angular/core';
import {ChildDto} from '../dtos/parent-dashboard.dto';

export interface ChildInfo {
  id: number;
  name: string;
  avatarUrl: string;
  age: number;
  starCount: number;
  totalStarsEarned: number;
  badges: any[];
  unlockedAvatars: any[];
  dateOfBirth: Date;
  difficulty: string;
  progress: { subjectName: string; progressPercentage: number }[];
}

@Injectable({
  providedIn: 'root'
})
export class ActiveChildService {
  private activeChildSignal = signal<ChildInfo | null>(null);

  public activeChild: Signal<ChildInfo | null> = this.activeChildSignal.asReadonly();

  constructor() {
    const storedChild = localStorage.getItem('activeChild');
    if (storedChild) {
      const parsedChild = JSON.parse(storedChild);
      if (parsedChild.dateOfBirth) {
        parsedChild.dateOfBirth = new Date(parsedChild.dateOfBirth);
      }
      this.activeChildSignal.set(parsedChild);
    }
  }

  setActiveChild(childDto: ChildDto): void {
    const childInfo: ChildInfo = {
      id: childDto.childId,
      name: childDto.name,
      age: childDto.age,
      avatarUrl: childDto.avatarUrl,
      starCount: childDto.starCount,
      totalStarsEarned: childDto.starCount,
      badges: childDto.badges,
      unlockedAvatars: childDto.unlockedAvatars,
      dateOfBirth: childDto.dateOfBirth,
      difficulty: childDto.difficulty,
      progress: childDto.progress
    };
    this.activeChildSignal.set(childInfo);
    localStorage.setItem('activeChild', JSON.stringify(childInfo));
  }

  updateAvatar(childInfo: ChildInfo): void {
    this.activeChildSignal.set(childInfo);
    localStorage.setItem('activeChild', JSON.stringify(childInfo));
  }

  clearActiveChild(): void {
    this.activeChildSignal.set(null);
    localStorage.removeItem('activeChild');
  }
}
