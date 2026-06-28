import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService, BandaDetalheOutput } from '../../core/services/api.service';
import { AuthService } from '../../core/services/auth.service';
import { Navbar } from '../../shared/navbar/navbar';

@Component({
  selector: 'app-banda-detalhe',
  imports: [Navbar],
  templateUrl: './banda-detalhe.html',
  styleUrl: './banda-detalhe.css'
})
export class BandaDetalhePage implements OnInit {
  private api = inject(ApiService);
  private auth = inject(AuthService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  banda = signal<BandaDetalheOutput | null>(null);
  loading = signal(true);
  erro = signal('');
  favoritando = signal(false);
  favoritado = signal(false);

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) { this.router.navigate(['/busca']); return; }
    this.api.obterBanda(id).subscribe({
      next: b => { this.banda.set(b); this.loading.set(false); },
      error: () => { this.erro.set('Banda não encontrada.'); this.loading.set(false); }
    });
  }

  favoritar() {
    const uid = this.auth.currentUser()?.id;
    const bid = this.banda()?.id;
    if (!uid || !bid) return;
    this.favoritando.set(true);
    this.api.favoritarBanda(uid, bid).subscribe({
      next: () => { this.favoritado.set(true); this.favoritando.set(false); },
      error: () => this.favoritando.set(false)
    });
  }

  formatarDuracao(d: string): string {
    const [, m, s] = d.split(':');
    const mm = parseInt(m || '0'), ss = Math.floor(parseFloat(s || '0'));
    return `${mm}:${ss.toString().padStart(2, '0')}`;
  }

  formatarAno(d: string): string {
    return new Date(d).getFullYear().toString();
  }
}
