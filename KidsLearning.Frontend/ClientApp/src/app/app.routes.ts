import { Routes } from '@angular/router';
import { ParentLogin} from './components/login/parent-login/parent-login';
import { ParentDashboardComponent } from  './components/parent-dashboard/parent-dashboard.component';
import { authGuard } from './guards/auth-guard';
import { StartPageComponent } from './components/start-page/start-page';
import { RegisterComponent } from './components/account/register.component/register.component';
import { LearningTasksComponent } from './components/learning-tasks/learning-tasks';


export const routes: Routes = [
   { path: '', component: StartPageComponent, pathMatch: 'full' },
  { path: 'start', component: StartPageComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: ParentLogin },
  {
    path: 'parent-dashboard',
    component: ParentDashboardComponent,
    canActivate: [authGuard]
  },
  { path: 'tasks/:subject',
    component: LearningTasksComponent,
    canActivate: [authGuard]
  }
];
