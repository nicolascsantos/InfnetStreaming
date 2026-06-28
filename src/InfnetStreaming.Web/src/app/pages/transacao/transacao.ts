import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { DecimalPipe, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, AutorizarOutput, PlanoOutput } from '../../core/services/api.service';
import { AuthService } from '../../core/services/auth.service';
import { Navbar } from '../../shared/navbar/navbar';

@Component({
  selector: 'app-transacao',
  imports: [FormsModule, Navbar, DecimalPipe, DatePipe],
  templateUrl: './transacao.html',
  styleUrl: './transacao.css'
})
export class TransacaoPage implements OnInit {
  private api = inject(ApiService);
  private auth = inject(AuthService);

  planos = signal<PlanoOutput[]>([]);
  planoSelecionadoId = signal('');
  loading = signal(false);
  resultado = signal<AutorizarOutput | null>(null);
  erro = signal('');

  valorSelecionado = computed(() =>
    this.planos().find(p => p.id === this.planoSelecionadoId())?.valor ?? 0
  );

  ngOnInit() {
    this.api.listarPlanos().subscribe({
      next: p => { this.planos.set(p); if (p.length) this.planoSelecionadoId.set(p[0].id); }
    });
  }

  autorizar() {
    const uid = this.auth.currentUser()?.id;
    if (!uid || !this.planoSelecionadoId()) return;
    this.loading.set(true);
    this.resultado.set(null);
    this.erro.set('');
    this.api.autorizarTransacao(uid, this.planoSelecionadoId(), this.valorSelecionado()).subscribe({
      next: r => { this.resultado.set(r); this.loading.set(false); },
      error: err => { this.erro.set(err.error?.detail ?? 'Erro ao processar.'); this.loading.set(false); }
    });
  }

  get statusLabel() {
    const s = this.resultado()?.status;
    return s === 1 ? 'Aprovada' : s === 2 ? 'Recusada' : s === 3 ? 'Cancelada' : 'Pendente';
  }
  get isAprovada() { return this.resultado()?.status === 1; }
}
