import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { Auth } from '../services/auth';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(Auth);
  const router = inject(Router);

  if (authService.isAuthenticated()) {
    return true; // Zugriff erlaubt
  } else {
    router.navigate(['/login']); // Zur Login-Seite
    return false;
  }
};
