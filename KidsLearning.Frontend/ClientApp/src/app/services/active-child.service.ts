// src/app/services/active-child.service.ts

import { Injectable, signal, Signal } from '@angular/core';
import { ChildDto } from '../dtos/parent-dashboard.dto';

export interface ChildInfo {
  id: number;
  name: string;
  avatarUrl: string;
  age: number;
  starCount: number;
  badges: any[];
  unlockedAvatars: any[];
  // Fügen Sie diese Eigenschaft hinzu
  dateOfBirth: Date;
  difficulty: string;
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
      // Hinzufügen der Geburtsdatums-Wiederherstellung aus localStorage
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
      badges: childDto.badges,
      unlockedAvatars: childDto.unlockedAvatars,
      dateOfBirth: childDto.dateOfBirth, // Fügen Sie hier die Eigenschaft hinzu
      difficulty: childDto.difficulty
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
