// src/app/components/parent-dashboard/parent-dashboard.component.ts

import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms'; // <-- HIER FÜGEN WIR FormsModule HINZU
import { CommonModule } from '@angular/common'; // Für *ngIf und *ngFor
import { ParentDashboardService } from '../../services/parent-dashboard.service';
import { ParentDashboardDto, ChildDto, AddChildDto } from '../../dtos/parent-dashboard.dto';

@Component({
  selector: 'app-parent-dashboard',
  standalone: true, // <-- STELL SICHER, DASS DIESE ZEILE VORHANDEN IST
  imports: [
    CommonModule,  // Wird für *ngIf und *ngFor benötigt
    FormsModule    // Wird für [(ngModel)] benötigt
  ],
  templateUrl: './parent-dashboard.html', 
  styleUrls: ['./parent-dashboard.css']
})
export class ParentDashboardComponent implements OnInit {
  // ... der Rest deines Component-Codes bleibt wie besprochen
  dashboardData: ParentDashboardDto = {
    welcomeMessage: '',
    children: [],
    recentActivities: []
  };

  newChild: AddChildDto = {
    name: '',
    avatarUrl: ''
  };

  showAddChildForm = false;
  message = '';

  constructor(private dashboardService: ParentDashboardService) { }

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.dashboardService.getDashboardData().subscribe({
      next: (data) => {
        this.dashboardData = data;
      },
      error: (err) => {
        this.message = 'Error loading dashboard data. Please log in again.';
        console.error(err);
      }
    });
  }

  toggleAddChildForm(): void {
    this.showAddChildForm = !this.showAddChildForm;
  }

  onAddChildSubmit(): void {
    if (this.newChild.name.trim()) {
      this.dashboardService.addChild(this.newChild).subscribe({
        next: (response: any) => {
          this.message = response.message;
          this.newChild = { name: '', avatarUrl: '' };
          this.showAddChildForm = false;
          this.loadDashboardData();
        },
        error: (err: any) => {
          this.message = 'Fehler beim Hinzufügen des Kindes.';
          console.error(err);
        }
      });
    }
  }
}