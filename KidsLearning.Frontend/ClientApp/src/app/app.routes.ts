import { Routes } from '@angular/router';
import { ParentLogin} from './components/login/parent-login/parent-login';
import { ParentDashboardComponent } from  './components/parent-dashboard/parent-dashboard.component';
import { authGuard } from './guards/auth-guard';


export const routes: Routes = [
    { path: 'login', component: ParentLogin },
    { path: 'dashboard', component: ParentDashboardComponent, canActivate: [authGuard] },
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' }
];
