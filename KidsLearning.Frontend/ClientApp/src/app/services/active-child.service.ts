

import { Injectable, signal, Signal } from '@angular/core';
import { ChildDto } from '../dtos/parent-dashboard.dto';

export interface ChildInfo {
  id: string;
  name: string;
  avatarUrl: string;
  age: number;
  starCount: number;
  badges: any[];
  unlockedAvatars: any[];
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
      this.activeChildSignal.set(JSON.parse(storedChild));
    }
  }

 
  setActiveChild(childDto: ChildDto): void {
    const childInfo: ChildInfo = {
      id: childDto.childId.toString(),
      name: childDto.name,
      age: childDto.age,
      avatarUrl: childDto.avatarUrl,
      starCount: childDto.starCount,
      badges: childDto.badges,
      unlockedAvatars: childDto.unlockedAvatars
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