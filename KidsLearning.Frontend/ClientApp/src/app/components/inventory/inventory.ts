// src/app/components/inventory/inventory.component.ts

import { Component, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ActiveChildService } from '../../services/active-child.service';
import { AvatarDto, BadgeDto } from '../../dtos/parent-dashboard.dto';
import { HttpClient } from '@angular/common/http'; // Importieren Sie HttpClient

@Component({
  selector: 'app-inventory',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './inventory.html',
  styleUrl: './inventory.css'
})
export class InventoryComponent implements OnInit {
  activeChild = computed(() => this.activeChildService.activeChild());
  unlockedAvatars: AvatarDto[] = [];
  badges: BadgeDto[] = [];
  
  constructor(
    private activeChildService: ActiveChildService,
    private route: ActivatedRoute,
    private http: HttpClient 
  ) {}

  ngOnInit(): void {
    const child = this.activeChild();
    if (child) {
     
      this.getUnlockedAvatars(child.id); 
    
      this.badges = child.badges;
    }
  }

  // Neue Methode zum Abrufen der freigeschalteten Avatare vom Backend
  getUnlockedAvatars(childId: string): void {
   
    this.http.get<AvatarDto[]>(`api/Inventory/avatars/${childId}`)
      .subscribe(avatars => {
        this.unlockedAvatars = avatars;
        console.log('Freigeschaltete Avatare:', this.unlockedAvatars);
      });
  }

  selectAvatar(avatar: AvatarDto): void {
    const child = this.activeChild();
    if (child) {
      const updatedChild = {
        ...child,
        avatarUrl: avatar.imageUrl
      };
      // Verwenden Sie die neue updateAvatar-Methode
      this.activeChildService.updateAvatar(updatedChild);
      console.log(`Avatar für Kind ${child.name} wurde geändert.`);
    }
  }
}