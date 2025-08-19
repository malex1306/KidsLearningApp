import { Routes } from '@angular/router';
import { ParentLogin } from './components/login/parent-login/parent-login';
import { ParentDashboardComponent } from './components/parent-dashboard/parent-dashboard.component';
import { authGuard } from './guards/auth-guard';
import { StartPageComponent } from './components/start-page/start-page';
import { RegisterComponent } from './components/account/register.component/register.component';
import { LearningTasksComponent } from './components/learning-tasks/learning-tasks';
import { LearningTaskDetail } from './components/learning-task-detail/learning-task-detail';
import { LearningLetterTasks } from './components/learning-letter-tasks/learning-letter-tasks';
import { InventoryComponent } from './components/inventory/inventory';
import { LearningTaskEnglish } from './components/learning-task-english/learning-task-english';

export const routes: Routes = [
  { path: '', component: StartPageComponent, pathMatch: 'full' },
  { path: 'start-page', component: StartPageComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: ParentLogin },

  { path: 'parent-dashboard', component: ParentDashboardComponent, canActivate: [authGuard] },
  { path: 'inventory/:childId', component: InventoryComponent, canActivate: [authGuard] },
  { path: 'tasks/:subject/child/:childId', component: LearningTasksComponent, canActivate: [authGuard] },
  { path: 'task/:id/child/:childId', component: LearningTaskDetail, canActivate: [authGuard] },
  { path: 'learning-letter-tasks/:subject/:id/child/:childId', component: LearningLetterTasks, canActivate: [authGuard] },
  { path: 'learning-task-english/:subject/:id/child/:childId', component: LearningTaskEnglish, canActivate: [authGuard] },

  // Fallback
  { path: '**', redirectTo: '' }
];
