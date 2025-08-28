import {Component, OnInit, computed} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {ActiveChildService} from '../../services/active-child.service';
import {AvatarDto, BadgeDto, EditChildDto} from '../../dtos/parent-dashboard.dto';
import {HttpClient} from '@angular/common/http';
import {ParentDashboardService} from '../../services/parent-dashboard.service';

@Component({
  selector: 'app-inventory',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './inventory.html',
  styleUrl: './inventory.css'
})
export class InventoryComponent implements OnInit {
  activeChild = computed(() => this.activeChildService.activeChild());
  
  // hier
  unlockedAvatars = computed(() => this.activeChild()?.unlockedAvatars ?? []);
  badges = computed(() => this.activeChild()?.badges ?? []);
  
  selectedAvatar: AvatarDto | null = null;

  constructor(
    private activeChildService: ActiveChildService,
    private route: ActivatedRoute,
    private http: HttpClient,
    private dashboardService: ParentDashboardService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    const child = this.activeChild();
    if (child) {
      const currentAvatar = this.unlockedAvatars().find(a => a.imageUrl === child.avatarUrl);
      if (currentAvatar) {
        this.selectedAvatar = currentAvatar;
      }
    }
  }

  selectAvatar(avatar: AvatarDto): void {
    this.selectedAvatar = avatar;
    console.log(`Avatar ${avatar.name} wurde lokal ausgewählt.`);
  }

  onConfirmSelection(): void {
    const child = this.activeChild();
    if (child && this.selectedAvatar) {
      console.log('Sende Anfrage zur Aktualisierung des Avatars...');
      const editChildDto: EditChildDto = {
        childId: Number(child.id),
        name: child.name,
        avatarUrl: this.selectedAvatar.imageUrl,
        dateOfBirth: child.dateOfBirth,
        difficulty: child.difficulty,
      };

      this.dashboardService.editChild(Number(child.id), editChildDto).subscribe({
        next: () => {
          console.log('✅ API-Aufruf erfolgreich! Aktualisiere Frontend-Status und navigiere...');
          
          this.activeChildService.updateChildInfo({
            avatarUrl: this.selectedAvatar!.imageUrl
          });

          this.router.navigate(['/start-page']);
        },
        error: (err: any) => {
          console.log('❌ API-Aufruf fehlgeschlagen!');
          console.error('Fehler beim Ändern des Avatars:', err);
        }
      });
    } else {
      console.log('Kein Kind ausgewählt oder kein Avatar ausgewählt.');
    }
  }
}