// src/app/components/start-page/start-page.ts

import { Component, OnInit, OnDestroy, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { Auth } from '../../services/auth';
import { ParentDashboardService } from '../../services/parent-dashboard.service';
import { ParentDashboardDto, ChildDto, DailyGoal } from '../../dtos/parent-dashboard.dto';
import { ActiveChildService, ChildInfo } from '../../services/active-child.service';

@Component({
  selector: 'app-start-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './start-page.html',
  styleUrl: './start-page.css'
})
export class StartPageComponent implements OnInit, OnDestroy {
  isLoggedIn = false;
  dashboardData: ParentDashboardDto | null = null;
  greetingText: string = '';
  private authStatusSubscription!: Subscription;

  activeChild = computed(() => this.activeChildService.activeChild());
  triggerAnimation: boolean = false;
  showDuduGif: boolean = false;
  subjectIconMap: { [key: string]: string } = {
  'Mathe-Abenteuer': 'math.png',
  'Buchstaben-Land': 'boy.png',
  'Spaßig-Land': 'girl.png',
  'Englisch': 'english.png',
  'Logik-Dschungel': 'logic.png'
};
  dailyGoals: DailyGoal[] = [];

  

  constructor(
    private authService: Auth,
    private parentDashboardService: ParentDashboardService,
    private activeChildService: ActiveChildService,
  ) { }

  ngOnInit(): void {
    this.authStatusSubscription = this.authService.isLoggedIn$.subscribe(isLoggedInStatus => {
      this.isLoggedIn = isLoggedInStatus;
      if (this.isLoggedIn) {
        this.loadDashboardData();
      } else {
        this.dashboardData = null;
        this.activeChildService.clearActiveChild();
      }
    });

    this.setGreetingMessage();
  }

  ngOnDestroy(): void {
    if (this.authStatusSubscription) {
      this.authStatusSubscription.unsubscribe();
    }
  }
  getSubjectIcon(subjectName: string): string {
  if (!subjectName) return 'default.png';

  if (subjectName.includes('Mathe')) return 'math.png';
  if (subjectName.includes('Buchstaben')) return 'boy.png';
  if (subjectName.includes('Spaß')) return 'girl.png';
  if (subjectName.includes('Englisch')) return 'english.png';
  if (subjectName.includes('Logik')) return 'logic.png';

  return 'default.png'; // Fallback
}
  

  loadDashboardData(): void {
    this.parentDashboardService.getDashboardData().subscribe({
      next: (data) => {
        this.dashboardData = data;
        if (data && data.children && data.children.length > 0) {
          if (!this.activeChild()) {
            this.selectChild(data.children[0]);
          }
        } else {
          this.activeChildService.clearActiveChild();
        }
        this.retriggerAnimation();
        // Triggere das GIF, wenn Daten geladen wurden und ein Kind aktiv ist
        if (this.activeChild()) {
            this.toggleDuduGif(true);
        }
      },
      error: (err) => console.error('Fehler beim Laden der Dashboard-Daten', err)
    });
  }

  selectChild(child: ChildDto): void {
    this.activeChildService.setActiveChild(child);
    this.retriggerAnimation();
    // Triggere das GIF bei der Auswahl eines Kindes
    this.toggleDuduGif(true);
  }

  setGreetingMessage(): void {
    const currentHour = new Date().getHours();
    if (currentHour < 12) {
      this.greetingText = "Guten Morgen";
    } else if (currentHour < 18) {
      this.greetingText = "Guten Tag";
    } else {
      this.greetingText = "Guten Abend";
    }
  }

  private retriggerAnimation(): void {
    this.triggerAnimation = false;
    setTimeout(() => {
      this.triggerAnimation = true;
    }, 10);
  }

  private toggleDuduGif(show: boolean): void {
    this.showDuduGif = show;
    if (show) {
      setTimeout(() => {
        this.showDuduGif = false;
      }, 3000); // 5000 Millisekunden = 5 Sekunden
    }
  }
}