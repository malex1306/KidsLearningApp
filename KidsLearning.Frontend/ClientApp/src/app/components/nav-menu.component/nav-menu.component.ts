// src/app/components/nav-menu/nav-menu.component.ts

import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NgClass, NgIf, AsyncPipe } from '@angular/common';
import { Router } from '@angular/router';
import { Auth } from '../../services/auth'; // Dein zentraler Auth Service

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [RouterLink, NgIf, NgClass, RouterLinkActive, AsyncPipe],
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