import { DatePipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ApiService, BandaOutput, MusicaOutput } from '../../core/services/api.service';
import { AuthService } from '../../core/services/auth.service';
import { Navbar } from '../../shared/navbar/navbar';

type Aba = 'bandas' | 'musicas';

@Component({
  selector: 'app-busca',
  imports: [FormsModule, Navbar, DatePipe, RouterLink],
  templateUrl: './busca.html',
  styleUrl: './busca.css'
})
export class BuscaPage implements OnInit {
  private api = inject(ApiService);
  private auth = inject(AuthService);

  aba = signal<Aba>('bandas');
  termo = signal('');
  loading = signal(false);

  bandas = signal<BandaOutput[]>([]);
  musicas = signal<MusicaOutput[]>([]);
  total = signal(0);
  pagina = signal(1);
  porPagina = 10;

  favoritandoId = signal<string | null>(null);
  feedbackMap = signal<Record<string, boolean>>({});

  ngOnInit() { this.buscar(); }

  trocarAba(aba: Aba) {
    this.aba.set(aba);
    this.pagina.set(1);
    this.buscar();
  }

  buscar() {
    this.loading.set(true);
    const t = this.termo();
    const p = this.pagina();
    if (this.aba() === 'bandas') {
      this.api.buscarBandas(t, p, this.porPagina).subscribe({
        next: r => { this.bandas.set(r.itens); this.total.set(r.total); this.loading.set(false); },
        error: () => this.loading.set(false)
      });
    } else {
      this.api.buscarMusicas(t, p, this.porPagina).subscribe({
        next: r => { this.musicas.set(r.itens); this.total.set(r.total); this.loading.set(false); },
        error: () => this.loading.set(false)
      });
    }
  }

  favoritar(id: string, tipo: 'banda' | 'musica') {
    const usuarioId = this.auth.currentUser()?.id;
    if (!usuarioId) return;
    this.favoritandoId.set(id);
    const obs = tipo === 'banda'
      ? this.api.favoritarBanda(usuarioId, id)
      : this.api.favoritarMusica(usuarioId, id);
    obs.subscribe({
      next: () => {
        this.feedbackMap.update(m => ({ ...m, [id]: true }));
        this.favoritandoId.set(null);
      },
      error: () => this.favoritandoId.set(null)
    });
  }

  isFavorito(id: string) { return this.feedbackMap()[id] === true; }

  get totalPaginas() { return Math.ceil(this.total() / this.porPagina); }
  irPara(p: number) { this.pagina.set(p); this.buscar(); }
  formatarDuracao(d: string): string {
    const [h, m, s] = d.split(':');
    const mm = parseInt(m || '0'), ss = Math.floor(parseFloat(s || '0'));
    return `${mm}:${ss.toString().padStart(2, '0')}`;
  }
}
