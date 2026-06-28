import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ApiService } from '../../core/services/api.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginPage {
  private api = inject(ApiService);
  private auth = inject(AuthService);
  private router = inject(Router);

  username = signal('admin');
  senha = signal('Admin123!');
  loading = signal(false);
  erro = signal('');

  entrar() {
    this.loading.set(true);
    this.erro.set('');
    this.api.login(this.username(), this.senha()).subscribe({
      next: res => {
        this.auth.setToken(res.token);
        this.router.navigate(['/busca']);
      },
      error: () => {
        this.erro.set('Usuário ou senha inválidos.');
        this.loading.set(false);
      }
    });
  }
}
