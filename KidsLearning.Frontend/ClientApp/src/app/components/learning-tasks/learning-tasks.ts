// src/app/components/learning-tasks/learning-tasks.component.ts

import { Component, OnInit } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { LearningTask } from '../../models/learning-task';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-learning-tasks',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './learning-tasks.html',
  styleUrl: './learning-tasks.css'
})
export class LearningTasksComponent implements OnInit {
  tasks: LearningTask[] = [];
  subject: string = '';
  childId: number | null = null; // Hinzugefügte Eigenschaft

  constructor(
    private tasksService: TasksService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Parameter aus der URL auslesen
    this.route.paramMap.subscribe(params => {
      this.subject = params.get('subject') || '';
      const childIdParam = params.get('childId'); // childId aus der Route lesen
      
      // Hinzugefügt: Die childId-Eigenschaft der Klasse zuweisen
      if (childIdParam) {
        this.childId = +childIdParam;
      }

      if (this.subject) {
        this.loadTasksBySubject();
      }
    });
  }

  loadTasksBySubject(): void {
    this.tasksService.getTasksBySubject(this.subject).subscribe({
      next: (tasks) => {
        this.tasks = tasks;
        console.log(`Geladene Aufgaben für ${this.subject}:`, this.tasks);
      },
      error: (err) => {
        console.error('Fehler beim Laden der Aufgaben:', err);
      }
    });
  }
}