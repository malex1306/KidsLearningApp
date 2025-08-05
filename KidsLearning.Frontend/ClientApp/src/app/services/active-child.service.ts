import { Injectable, signal, Signal } from '@angular/core';

export interface ChildInfo {
  id: string;
  name: string;
  avatarUrl: string;
  age: number;
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

  setActiveChild(child: ChildInfo): void {
    this.activeChildSignal.set(child);
    localStorage.setItem('activeChild', JSON.stringify(child));
  }

  clearActiveChild(): void {
    this.activeChildSignal.set(null);
    localStorage.removeItem('activeChild');
  }
}