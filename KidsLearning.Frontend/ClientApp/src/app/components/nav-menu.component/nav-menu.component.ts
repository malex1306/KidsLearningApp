import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NgClass, AsyncPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../../services/auth';
import { first } from 'rxjs/operators';
import { ActiveChildService } from '../../services/active-child.service';
import { Subscription } from 'rxjs';
import { ParentDashboardService } from '../../services/parent-dashboard.service';
import { ChildDto } from '../../dtos/parent-dashboard.dto';

@Component({
 selector: 'app-nav-menu',
 standalone: true,
 imports: [RouterLink, RouterLinkActive, NgClass, AsyncPipe, FormsModule],
 templateUrl: './nav-menu.component.html',
 styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit, OnDestroy {
 isExpanded = false;

 showDashboardModal = false;
 showLogoutModal = false;

 passwordInput = '';
 errorMessage = '';
 starCount = 0;
 children: ChildDto[] | null = null;
 private authStatusSubscription!: Subscription;
 private dashboardDataSubscription!: Subscription;

 constructor(
  private router: Router,
  public authService: Auth,
  public activeChildService: ActiveChildService,
  private parentDashboardService: ParentDashboardService
 ) { }

 ngOnInit(): void {
  this.authStatusSubscription = this.authService.isLoggedIn$.subscribe(isLoggedInStatus => {
   if (isLoggedInStatus) {
    this.loadChildren();
   } else {
    this.children = null;
   }
  });
 }

 ngOnDestroy(): void {
  if (this.authStatusSubscription) {
   this.authStatusSubscription.unsubscribe();
  }
  if (this.dashboardDataSubscription) {
   this.dashboardDataSubscription.unsubscribe();
  }
 }

 loadChildren(): void {
  this.dashboardDataSubscription = this.parentDashboardService.getDashboardData().subscribe({
   next: (data) => {
    this.children = data.children;
   },
   error: (err) => {
    console.error('Fehler beim Laden der Kinderdaten:', err);
    this.children = null;
   }
  });
 }

 selectChild(child: ChildDto): void {
  this.activeChildService.setActiveChild(child);
  this.collapse(); // Schließt das Menü nach Auswahl
 }

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

 submitDashboard(): void {
  const parentEmail = sessionStorage.getItem('parent_email');
  if (!parentEmail) {
   this.errorMessage = 'E-Mail-Adresse nicht gefunden.';
   return;
  }

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
  this.authService.logout();
  this.router.navigate(['/']);
 }

 cancelModal(): void {
  this.showDashboardModal = false;
  this.showLogoutModal = false;
 }
}