import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'

})
export class Auth {
    private apiUrl = 'http://localhost:5207/api/Account/';
    
    constructor(private http: HttpClient) {}

    login(credentials: { email: string; password: string; rememberMe: boolean }): Observable<any> {
  return this.http.post(this.apiUrl + 'login', credentials)
    .pipe(
      tap((response: any) => {
        if (response.token) {
          localStorage.setItem('jwt_token', response.token);
        }
      }),
      catchError(error => {
        console.error('Login failed', error);
        return throwError(() => new Error(error?.error?.message || 'Login failed.'));
      })
    );
}

    logout() : void {
      localStorage.removeItem('jwt_token');
    }

    isLoggedIn() : boolean {
      return !!localStorage.getItem('jwt_token');
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
