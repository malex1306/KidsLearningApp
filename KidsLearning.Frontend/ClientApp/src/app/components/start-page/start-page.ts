// src/app/components/start-page/start-page.ts
import { Component, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
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
export class StartPageComponent implements OnInit {
  isLoggedIn = false;
  dashboardData: ParentDashboardDto | null = null;
  
  activeChild = computed(() => this.activeChildService.activeChild());

  constructor(
    private authService: Auth,
    private parentDashboardService: ParentDashboardService, 
    private activeChildService: ActiveChildService
  ) { }

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
    if (this.isLoggedIn) {
      this.loadDashboardData();
    }
  }

  loadDashboardData(): void {
    this.parentDashboardService.getDashboardData().subscribe({
      next: (data) => {
        this.dashboardData = data;
      },
      error: (err) => console.error('Fehler beim Laden der Dashboard-Daten', err)
    });
  }

  // Korrigierte Methode: Der Parameter ist ChildDto
  selectChild(child: ChildDto): void {
    const childInfo: ChildInfo = {
      id: child.childId.toString(), // childId wird zu id gemappt (und als String gespeichert)
      name: child.name,
      avatarUrl: child.avatarUrl
    };
    this.activeChildService.setActiveChild(childInfo);
  }
}