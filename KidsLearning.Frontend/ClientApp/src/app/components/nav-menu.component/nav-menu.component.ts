// src/app/components/nav-menu/nav-menu.component.ts
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NgClass, AsyncPipe } from '@angular/common';
import { Router } from '@angular/router';
import { Auth } from '../../services/auth';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [RouterLink, NgClass, RouterLinkActive, AsyncPipe],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent implements OnInit, OnDestroy {
  isExpanded = false;
  
  constructor(private router: Router, public authService: Auth) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  navigateToDashboardWithPassword(): void {
    this.collapse();
    const parentPassword = window.prompt("Bitte gib das Eltern-Passwort ein, um zum Dashboard zu gelangen:");
    if (parentPassword) {
      const parentEmail = sessionStorage.getItem('parent_email');

      if (parentEmail) {
        this.authService.login({ email: parentEmail, password: parentPassword, rememberMe: false }).pipe(
          first()
        ).subscribe(
          () => {
            this.router.navigate(['/parent-dashboard']);
          },
          () => {
            alert("Falsches Passwort. Zugriff verweigert.");
          }
        );
      } else {
        alert("Fehler: E-Mail-Adresse nicht gefunden. Bitte melde dich erneut an.");
      }
    }
  }

  logoutWithPassword(): void {
    this.collapse();
    const parentPassword = window.prompt("Bitte gib das Eltern-Passwort ein, um dich auszuloggen:");
    if (parentPassword) {
      const parentEmail = sessionStorage.getItem('parent_email');

      if (parentEmail) {
        this.authService.login({ email: parentEmail, password: parentPassword, rememberMe: false }).pipe(
          first()
        ).subscribe(
          () => {
            this.authService.logout();
            this.router.navigate(['/']);
          },
          () => {
            alert("Falsches Passwort. Zugriff verweigert.");
          }
        );
      } else {
        alert("Fehler: E-Mail-Adresse nicht gefunden. Bitte melde dich erneut an.");
      }
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  collapse(): void {
    this.isExpanded = false;
  }

  toggle(): void {
    this.isExpanded = !this.isExpanded;
  }
}