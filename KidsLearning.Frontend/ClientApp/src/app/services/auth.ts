// src/app/services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private apiUrl = 'http://localhost:5207/api/Account/';
  
  // NEU: BehaviorSubject, um den Status zu speichern und zu teilen
  private isLoggedInSubject: BehaviorSubject<boolean>;
  public isLoggedIn$: Observable<boolean>;

  constructor(private http: HttpClient) {
    const initialStatus = !!localStorage.getItem('jwt_token');
    this.isLoggedInSubject = new BehaviorSubject<boolean>(initialStatus);
    this.isLoggedIn$ = this.isLoggedInSubject.asObservable();
  }

  login(credentials: { email: string; password: string; rememberMe: boolean }): Observable<any> {
    return this.http.post(this.apiUrl + 'login', credentials)
      .pipe(
        tap((response: any) => {
          if (response.token) {
            localStorage.setItem('jwt_token', response.token);
            this.isLoggedInSubject.next(true); 
          }
        }),
        catchError(error => {
          console.error('Login failed', error);
          this.isLoggedInSubject.next(false); 
          return throwError(() => new Error(error?.error?.message || 'Login failed.'));
        })
      );
  }

  logout(): void {
    localStorage.removeItem('jwt_token');
    this.isLoggedInSubject.next(false);
  }

  register(user: { email: string; password: string, confirmedPassword: string, userName: string }): Observable<any> {
    return this.http.post(this.apiUrl + 'register', user)
      .pipe(
        catchError(error => {
          console.error('Registration failed', error);
          return throwError(() => new Error(error?.error?.message || 'Registration failed.'));
        })
      );
  }
}