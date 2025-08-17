// src/app/components/start-page/start-page.ts

import { Component, OnInit, OnDestroy, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { Auth } from '../../services/auth'; 
import { ParentDashboardService } from '../../services/parent-dashboard.service';
import { ParentDashboardDto, ChildDto } from '../../dtos/parent-dashboard.dto'; // ChildDto importieren
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

  constructor(
    private authService: Auth,
    private parentDashboardService: ParentDashboardService, 
    private activeChildService: ActiveChildService
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
    },
    error: (err) => console.error('Fehler beim Laden der Dashboard-Daten', err)
  });
}

  // Korrigierte Methode, um ChildDto zu akzeptieren
  selectChild(child: ChildDto): void {
    this.activeChildService.setActiveChild(child);
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
}