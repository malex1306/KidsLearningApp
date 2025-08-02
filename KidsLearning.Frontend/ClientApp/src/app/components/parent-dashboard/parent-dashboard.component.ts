import { Component, OnInit } from '@angular/core';
import { ParentDashboardService } from '../../services/parent-dashboard.service';
import { CommonModule } from '@angular/common'; 


@Component({
  selector: 'app-parent-dashboard',
  imports: [CommonModule],
  templateUrl: './parent-dashboard.html',
  styleUrl: './parent-dashboard.css'
})
export class ParentDashboardComponent implements OnInit {
  dashboardMessage: string = '';

  constructor(private parentDashboardService: ParentDashboardService) {}

  ngOnInit(): void {
    this.parentDashboardService.getDashboardData().subscribe({
      next: (data) => {
        this.dashboardMessage = data.message;
      },
      error: (err) => {
        console.error('Failed to load dashboard data', err);
        this.dashboardMessage = 'Error loading dashboard data. Please log in again.';
      }
    });
  }
}
