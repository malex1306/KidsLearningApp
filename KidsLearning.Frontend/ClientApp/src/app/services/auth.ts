import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable, tap, catchError, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private apiUrl = 'http://localhost:5207/api/Account/';
  private tokenKey = 'jwt_token';

  private isLoggedInSubject: BehaviorSubject<boolean>;
  public isLoggedIn$: Observable<boolean>;

  constructor(private http: HttpClient) {
    const initialStatus = !!sessionStorage.getItem(this.tokenKey);
    this.isLoggedInSubject = new BehaviorSubject<boolean>(initialStatus);
    this.isLoggedIn$ = this.isLoggedInSubject.asObservable();
  }

  login(credentials: { email: string; password: string; rememberMe: boolean }): Observable<any> {
    return this.http.post(this.apiUrl + 'login', credentials).pipe(
      tap((response: any) => {
        if (response.token) {
          sessionStorage.setItem(this.tokenKey, response.token);
          sessionStorage.setItem('parent_email', credentials.email);
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
    sessionStorage.removeItem('jwt_token');
    sessionStorage.removeItem('parent_email');
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

  isAuthenticated(): boolean {
    return !!sessionStorage.getItem(this.tokenKey);
  }

  getToken(): string | null {
    return sessionStorage.getItem(this.tokenKey);
  }
}
