import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NgClass, NgIf } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [RouterLink, NgIf, NgClass,RouterLinkActive],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent implements OnInit, OnDestroy {
  isExpanded = false;
  isLoggedIn = false;
  private authStatusSubscription!: Subscription;

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.checkLoginStatus();
    
  }

  ngOnDestroy(): void {
    
  }

  checkLoginStatus(): void {
    this.isLoggedIn = !!localStorage.getItem('jwt_token');
  }

  logout(): void {
    localStorage.removeItem('jwt_token');
    this.isLoggedIn = false;
    this.router.navigate(['/']); 
  }

  collapse(): void {
    this.isExpanded = false;
  }

  toggle(): void {
    this.isExpanded = !this.isExpanded;
  }
}