// src/app/components/inventory/inventory.component.ts

import { Component, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ActiveChildService } from '../../services/active-child.service';
import { AvatarDto, BadgeDto } from '../../dtos/parent-dashboard.dto';

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
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const child = this.activeChild();
    if (child) {
      this.unlockedAvatars = child.unlockedAvatars;
      this.badges = child.badges;
    }
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