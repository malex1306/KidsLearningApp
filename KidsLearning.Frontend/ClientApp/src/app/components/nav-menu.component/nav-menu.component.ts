import {Component, OnDestroy, OnInit, effect} from '@angular/core';
import {RouterLink, RouterLinkActive} from '@angular/router';
import {NgClass, AsyncPipe} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {Router} from '@angular/router';
import {Auth} from '../../services/auth';
import {first, Subscription} from 'rxjs';
import {ActiveChildService} from '../../services/active-child.service';
import {ParentDashboardService} from '../../services/parent-dashboard.service';
import {ChildDto} from '../../dtos/parent-dashboard.dto';

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
  private childrenSubscription!: Subscription;

  constructor(
    private router: Router,
    public authService: Auth,
    public activeChildService: ActiveChildService,
    private parentDashboardService: ParentDashboardService
  ) {
    effect(() => {
      const child = this.activeChildService.activeChild();
      if (child && this.children) {
        const index = this.children.findIndex(c => c.childId === child.id);
        if (index >= 0) {
          this.children[index].avatarUrl = child.avatarUrl;
        }
      }
    });
  }

  ngOnInit(): void {
    this.authStatusSubscription = this.authService.isLoggedIn$.subscribe(isLoggedInStatus => {
      if (isLoggedInStatus) {
        this.loadChildren();
      } else {
        this.children = null;
      }
    });


    this.childrenSubscription = this.parentDashboardService.children$.subscribe(
      children => {
        this.children = children;
        const currentActiveChild = this.activeChildService.activeChild();
        if (currentActiveChild && this.children) {
          const updatedChild = this.children.find(c => c.childId === currentActiveChild.id);
          if (updatedChild) {
            this.activeChildService.setActiveChild(updatedChild);
          }
        }
      }
    );
  }

  ngOnDestroy(): void {
    if (this.authStatusSubscription) this.authStatusSubscription.unsubscribe();
    if (this.childrenSubscription) this.childrenSubscription.unsubscribe();
  }

  loadChildren(): void {
    this.parentDashboardService.getDashboardData().subscribe({
      error: (err) => {
        console.error('Fehler beim Laden der Kinderdaten:', err);
        this.children = null;
      }
    });
  }

  selectChild(child: ChildDto): void {
    this.activeChildService.setActiveChild(child);
    this.collapse();
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

    this.authService.login({email: parentEmail, password: this.passwordInput, rememberMe: false})
      .pipe(first())
      .subscribe({
        next: () => {
          this.showDashboardModal = false;
          this.router.navigate(['/parent-dashboard']);
          this.loadChildren();
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
