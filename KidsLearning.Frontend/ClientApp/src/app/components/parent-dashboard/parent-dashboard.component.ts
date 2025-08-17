// src/app/components/parent-dashboard/parent-dashboard.component.ts

import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms'; // <-- HIER FÜGEN WIR FormsModule HINZU
import { CommonModule } from '@angular/common'; // Für *ngIf und *ngFor
import { ParentDashboardService } from '../../services/parent-dashboard.service';
import { ParentDashboardDto, ChildDto, AddChildDto, RemoveChildDto, EditChildDto } from '../../dtos/parent-dashboard.dto';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-parent-dashboard',
  standalone: true, 
  imports: [
    CommonModule,  
    FormsModule   
  ],
  templateUrl: './parent-dashboard.html', 
  styleUrls: ['./parent-dashboard.css']
})
export class ParentDashboardComponent implements OnInit {
 showAddChildForm = false;
  showEditChildForm = false;
  editingChild: ChildDto | null = null;
  message = '';
  availableAvatars: string[] = [
    'assets/images/bat.png',
    'assets/images/dog.png',
    'assets/images/fairy.png',
    'assets/images/pumpkin.png',
    'assets/images/firefighter.png',
  ];

  dashboardData: ParentDashboardDto = {
    welcomeMessage: '',
    children: [],
    recentActivities: []
  };

  newChild: AddChildDto = {
    name: '',
    avatarUrl: '',
    dateOfBirth: new Date()
  };

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
          this.newChild = { name: '', avatarUrl: '', dateOfBirth: new Date() }; 
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
  onDeleteChild(childId: number): void {
    this.dashboardService.removeChild( childId ).subscribe({
      next: (response: any) => {
        this.message = response.message;
        this.loadDashboardData();
      },
      error: (err: any) => {
        this.message = 'Fehler beim Entfernen des Kindes.';
        console.error(err);
      }
    });
  }
  onEditChild(child: ChildDto): void {
    this.editingChild = { ...child }; 
    this.showEditChildForm = true;
    this.showAddChildForm = false;
    };

  onEditChildSubmit(): void {
  // Überprüfe, ob ein Kind zum Bearbeiten ausgewählt ist
   if (this.editingChild && this.editingChild.name.trim()) {
      // Rufe den Service auf, um die Änderungen zu speichern
      this.dashboardService.editChild(this.editingChild.childId, this.editingChild as EditChildDto).subscribe({
        next: (response: any) => {
          this.message = response.message;
          // Formular zurücksetzen und schließen
          this.editingChild = null;
          this.showEditChildForm = false;
          this.loadDashboardData(); // Dashboard-Daten neu laden
        },
        error: (err: any) => {
          this.message = 'Fehler beim Bearbeiten des Kindes.';
          console.error(err);
      }
    });
  }
}

selectNewChildAvatar(avatarUrl:string): void{
  this.newChild.avatarUrl = avatarUrl;
}

selectEditChildAvatar(avatarUrl: string): void {
  if (this.editingChild) {
    this.editingChild.avatarUrl = avatarUrl;
  }
}
  
}