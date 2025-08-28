import {Component, OnInit} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {ParentDashboardService} from '../../services/parent-dashboard.service';
import {ParentDashboardDto, ChildDto, AddChildDto, EditChildDto} from '../../dtos/parent-dashboard.dto';
import {MatButtonModule} from '@angular/material/button';
import {ActiveChildService} from '../../services/active-child.service';

@Component({
  selector: 'app-parent-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule
  ],
  templateUrl: './parent-dashboard.html',
  styleUrls: ['./parent-dashboard.css']
})
export class ParentDashboardComponent implements OnInit {
  showAddChildForm = false;
  showEditChildForm = false;
  editingChild: ChildDto | null = null;
  minDate: string = '';
  maxDate: string = '';
  message = '';
  availableAvatars: string[] = [
    'assets/images/bat.png',
    'assets/images/dog.png',
    'assets/images/fairy.png',
    'assets/images/pumpkin.png',
    'assets/images/firefighter.png',
  ];
  availableDifficulties: string[] = [
    'Vorschule',
    '1 Klasse',
    '2 Klasse',
    '3 Klasse',
    '4 Klasse',
  ];

  dashboardData: ParentDashboardDto = {
    welcomeMessage: '',
    children: [],
    recentActivities: []
  };

  newChild: AddChildDto = {
    name: '',
    avatarUrl: '',
    dateOfBirth: new Date(),
    difficulty: '',
  };

  constructor(
    private dashboardService: ParentDashboardService,
    private activeChildService: ActiveChildService
  ) {
  }

  ngOnInit(): void {
    this.setDateLimits();
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.dashboardService.getDashboardData().subscribe({
      next: (data) => {
        this.dashboardData = data;
      },
      error: (err) => {
        this.showMessage('Fehler beim Laden der Dashboard-Daten. Bitte melden Sie sich erneut an.', 5000);
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
          this.showMessage(response.message, 3000);
          this.newChild = {name: '', avatarUrl: '', dateOfBirth: new Date(), difficulty: ''};
          this.showAddChildForm = false;
          this.loadDashboardData();
        },
        error: (err: any) => {
          const errorMessage = err?.error?.message || 'Fehler beim Hinzufügen des Kindes.';
          this.showMessage(errorMessage, 5000);
          console.error(err);
        }
      });
    }
  }

  onDeleteChild(childId: number): void {
    this.dashboardService.removeChild(childId).subscribe({
      next: (response: any) => {
        this.showMessage(response.message, 3000);
        this.loadDashboardData();
      },
      error: (err: any) => {
        this.showMessage('Fehler beim Entfernen des Kindes.', 5000);
        console.error(err);
      }
    });
  }

  onEditChild(child: ChildDto): void {
    this.editingChild = {...child};
    this.showEditChildForm = true;
    this.showAddChildForm = false;
  }

  onEditChildSubmit(): void {
    if (this.editingChild && this.editingChild.name.trim()) {
      const editedChildData = {...this.editingChild};

      this.dashboardService.editChild(editedChildData.childId, editedChildData as EditChildDto).subscribe({
        next: (response: any) => {
          this.showMessage(response.message, 1500);
          this.showEditChildForm = false;
          this.loadDashboardData();

          this.activeChildService.setActiveChild(editedChildData as ChildDto);

          this.editingChild = null;
        },
        error: (err: any) => {
          const errorMessage = err?.error?.message || 'Fehler beim Hinzufügen des Kindes.';
          this.showMessage(errorMessage, 5000);
          console.error(err);
        }
      });
    }
  }

  selectNewChildAvatar(avatarUrl: string): void {
    this.newChild.avatarUrl = avatarUrl;
  }

  selectEditChildAvatar(avatarUrl: string): void {
    if (this.editingChild) {
      this.editingChild.avatarUrl = avatarUrl;
    }
  }

  private showMessage(msg: string, duration: number): void {
    this.message = msg;
    setTimeout(() => {
      this.message = '';
    }, duration);
  }
  private setDateLimits(): void {
    const today = new Date();

    const min = new Date(today.getFullYear() - 18, today.getMonth(), today.getDate());
    const max = new Date(today.getFullYear() - 3, today.getMonth(), today.getDate());

    this.minDate = min.toISOString().split('T')[0];
    this.maxDate = max.toISOString().split('T')[0];
  }
}
