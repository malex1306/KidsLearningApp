import {Component, WritableSignal, signal} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {Router, RouterLink} from '@angular/router';
import {Auth} from '../../../services/auth';

@Component({
  selector: 'app-register.component',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  email = '';
  password = '';
  confirmPassword = '';
  userName = '';
  errorMessage: WritableSignal<string> = signal('');

  constructor(private auth: Auth, private router: Router) {
  }

  onRegister(): void {
    this.errorMessage.set('');

    if (this.password !== this.confirmPassword) {
      this.errorMessage.set('Die Passwörter stimmen nicht überein.');
      return;
    }

    if (!this.email || !this.password || !this.userName) {
      this.errorMessage.set('E-Mail, Benutzername und Passwort sind erforderlich.');
      return;
    }

    this.auth.register({
      email: this.email,
      password: this.password,
      confirmedPassword: this.confirmPassword,
      userName: this.userName
    }).subscribe({
      next: () => {
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.errorMessage.set(err.message || 'Registrierung fehlgeschlagen.');
      }
    });
  }
}
