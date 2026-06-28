import { DecimalPipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { ApiService, AssinarOutput, PlanoOutput, UsuarioOutput } from '../../core/services/api.service';
import { AuthService } from '../../core/services/auth.service';
import { Navbar } from '../../shared/navbar/navbar';

@Component({
  selector: 'app-assinatura',
  imports: [Navbar, DecimalPipe],
  templateUrl: './assinatura.html',
  styleUrl: './assinatura.css'
})
export class AssinaturaPage implements OnInit {
  private api = inject(ApiService);
  private auth = inject(AuthService);

  usuario = signal<UsuarioOutput | null>(null);
  planos = signal<PlanoOutput[]>([]);
  loading = signal(true);
  assinandoId = signal<string | null>(null);
  resultado = signal<AssinarOutput | null>(null);

  ngOnInit() {
    const uid = this.auth.currentUser()?.id;
    if (!uid) return;
    this.api.obterUsuario(uid).subscribe({ next: u => this.usuario.set(u) });
    this.api.listarPlanos().subscribe({ next: p => { this.planos.set(p); this.loading.set(false); } });
  }

  assinar(planoId: string) {
    const uid = this.auth.currentUser()?.id;
    if (!uid) return;
    this.assinandoId.set(planoId);
    this.resultado.set(null);
    this.api.assinar(uid, planoId).subscribe({
      next: r => {
        this.resultado.set(r);
        this.assinandoId.set(null);
        if (r.status === 1) this.api.obterUsuario(uid).subscribe({ next: u => this.usuario.set(u) });
      },
      error: () => this.assinandoId.set(null)
    });
  }

  isAtual(planoId: string) { return this.usuario()?.plano_id === planoId; }
}
