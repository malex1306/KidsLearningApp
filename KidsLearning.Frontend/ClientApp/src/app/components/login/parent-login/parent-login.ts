import {Component, signal, Signal, WritableSignal} from '@angular/core';
import {Auth} from '../../../services/auth';

;
import {FormsModule} from '@angular/forms';
import {Router} from '@angular/router';


@Component({
  selector: 'app-parent-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './parent-login.html',
  styleUrl: './parent-login.css'
})
export class ParentLogin {
  email: Signal<string> = signal('');
  password: Signal<string> = signal('');
  errorMessage: WritableSignal<string> = signal('');
  showPassword: WritableSignal<boolean> = signal(false);

  constructor(private auth: Auth, private router: Router) {
  }

  onLogin(): void {
    this.auth.login({email: this.email(), password: this.password(), rememberMe: false}).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (err) => {
        this.errorMessage.set(err.message);
      }
    });
  }

  togglePasswordVisibility(): void {
    this.showPassword.set(!this.showPassword())
  }
}
