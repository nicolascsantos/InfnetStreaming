import { DecimalPipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ApiService, PlanoOutput } from '../../core/services/api.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-criar-conta',
  imports: [FormsModule, RouterLink, DecimalPipe],
  templateUrl: './criar-conta.html',
  styleUrl: './criar-conta.css'
})
export class CriarContaPage implements OnInit {
  private api = inject(ApiService);
  private auth = inject(AuthService);
  private router = inject(Router);

  nome = signal('');
  username = signal('');
  senha = signal('');
  planoId = signal('');
  planos = signal<PlanoOutput[]>([]);
  loading = signal(false);
  sucesso = signal(false);
  erro = signal('');

  ngOnInit() {
    this.api.listarPlanos().subscribe({
      next: p => { this.planos.set(p); if (p.length) this.planoId.set(p[0].id); },
      error: () => this.erro.set('Não foi possível carregar os planos.')
    });
  }

  criar() {
    if (!this.nome() || !this.username() || !this.senha() || !this.planoId()) {
      this.erro.set('Preencha todos os campos.');
      return;
    }
    this.loading.set(true);
    this.erro.set('');
    this.api.criarUsuario({ nome: this.nome(), username: this.username(), senha: this.senha(), plano_id: this.planoId() }).subscribe({
      next: () => {
        this.api.login(this.username(), this.senha()).subscribe({
          next: res => { this.auth.setToken(res.token); this.router.navigate(['/busca']); },
          error: () => { this.sucesso.set(true); this.loading.set(false); }
        });
      },
      error: err => {
        this.erro.set(err.error?.detail ?? 'Erro ao criar conta.');
        this.loading.set(false);
      }
    });
  }
}
