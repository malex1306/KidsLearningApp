import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NgClass, AsyncPipe, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../../services/auth';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, NgClass, AsyncPipe, FormsModule, NgIf],
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit, OnDestroy {
  isExpanded = false;

  showDashboardModal = false;
  showLogoutModal = false;

  passwordInput = '';
  errorMessage = '';

  constructor(private router: Router, public authService: Auth) { }

  ngOnInit(): void { }
  ngOnDestroy(): void { }

  toggle(): void {
    this.isExpanded = !this.isExpanded;
  }

  collapse(): void {
    this.isExpanded = false;
  }

  openDashboardModal(): void {
    this.passwordInput = '';
    this.errorMessage = '';
    this.showDashboardModal = true;
  }

  openLogoutModal(): void {
    this.passwordInput = '';
    this.errorMessage = '';
    this.showLogoutModal = true;
  }

  submitDashboard(): void {
    const parentEmail = sessionStorage.getItem('parent_email');
    if (!parentEmail) { this.errorMessage = 'E-Mail-Adresse nicht gefunden.'; return; }

    this.authService.login({ email: parentEmail, password: this.passwordInput, rememberMe: false })
      .pipe(first())
      .subscribe({
        next: () => {
          this.showDashboardModal = false;
          this.router.navigate(['/parent-dashboard']);
        },
        error: () => this.errorMessage = 'Falsches Passwort. Zugriff verweigert.'
      });
  }

  submitLogout(): void {
    const parentEmail = sessionStorage.getItem('parent_email');
    if (!parentEmail) { this.errorMessage = 'E-Mail-Adresse nicht gefunden.'; return; }

    this.authService.login({ email: parentEmail, password: this.passwordInput, rememberMe: false })
      .pipe(first())
      .subscribe({
        next: () => {
          this.showLogoutModal = false;
          this.authService.logout();
          this.router.navigate(['/']);
        },
        error: () => this.errorMessage = 'Falsches Passwort. Zugriff verweigert.'
      });
  }

  cancelModal(): void {
    this.showDashboardModal = false;
    this.showLogoutModal = false;
  }
}
