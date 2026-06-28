import { Component, OnInit, inject, signal } from '@angular/core';
import { ApiService, FavoritosOutput } from '../../core/services/api.service';
import { AuthService } from '../../core/services/auth.service';
import { Navbar } from '../../shared/navbar/navbar';

@Component({
  selector: 'app-favoritos',
  imports: [Navbar],
  templateUrl: './favoritos.html',
  styleUrl: './favoritos.css'
})
export class FavoritosPage implements OnInit {
  private api = inject(ApiService);
  private auth = inject(AuthService);

  loading = signal(true);
  favoritos = signal<FavoritosOutput>({ musicas_favoritas: [], bandas_favoritas: [] });

  ngOnInit() { this.carregar(); }

  carregar() {
    const usuarioId = this.auth.currentUser()?.id;
    if (!usuarioId) return;
    this.loading.set(true);
    this.api.obterFavoritos(usuarioId).subscribe({
      next: f => { this.favoritos.set(f); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  desfavoritarMusica(musicaId: string) {
    const uid = this.auth.currentUser()?.id;
    if (!uid) return;
    this.api.desfavoritarMusica(uid, musicaId).subscribe({ next: () => this.carregar() });
  }

  desfavoritarBanda(bandaId: string) {
    const uid = this.auth.currentUser()?.id;
    if (!uid) return;
    this.api.desfavoritarBanda(uid, bandaId).subscribe({ next: () => this.carregar() });
  }

  formatarDuracao(d: string): string {
    const [, m, s] = d.split(':');
    const mm = parseInt(m || '0'), ss = Math.floor(parseFloat(s || '0'));
    return `${mm}:${ss.toString().padStart(2, '0')}`;
  }
}
