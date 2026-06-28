import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService, BuscaResult, MusicaOutput, PlaylistDetalheOutput, PlaylistOutput } from '../../core/services/api.service';
import { AuthService } from '../../core/services/auth.service';
import { Navbar } from '../../shared/navbar/navbar';

@Component({
  selector: 'app-playlists',
  imports: [Navbar, FormsModule],
  templateUrl: './playlists.html',
  styleUrl: './playlists.css'
})
export class PlaylistsPage implements OnInit {
  private api = inject(ApiService);
  private auth = inject(AuthService);

  playlists = signal<PlaylistOutput[]>([]);
  playlistAberta = signal<PlaylistDetalheOutput | null>(null);
  loading = signal(true);
  novaPlaylistNome = signal('');
  criando = signal(false);
  erro = signal('');

  buscaMusica = signal('');
  resultadoBusca = signal<MusicaOutput[]>([]);
  buscando = signal(false);
  adicionando = signal<string | null>(null);
  removendo = signal<string | null>(null);

  private get uid() { return this.auth.currentUser()?.id ?? ''; }

  ngOnInit() { this.carregar(); }

  carregar() {
    this.loading.set(true);
    this.api.listarPlaylists(this.uid).subscribe({
      next: p => { this.playlists.set(p); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  abrirPlaylist(id: string) {
    const atual = this.playlistAberta();
    if (atual?.id === id) { this.playlistAberta.set(null); return; }
    this.api.obterPlaylist(this.uid, id).subscribe({
      next: p => this.playlistAberta.set(p)
    });
  }

  criarPlaylist() {
    const nome = this.novaPlaylistNome().trim();
    if (!nome) return;
    this.criando.set(true);
    this.api.criarPlaylist(this.uid, nome).subscribe({
      next: () => {
        this.novaPlaylistNome.set('');
        this.criando.set(false);
        this.carregar();
      },
      error: () => this.criando.set(false)
    });
  }

  deletarPlaylist(playlistId: string) {
    this.api.deletarPlaylist(this.uid, playlistId).subscribe({
      next: () => {
        if (this.playlistAberta()?.id === playlistId) this.playlistAberta.set(null);
        this.carregar();
      }
    });
  }

  buscarMusicas() {
    const termo = this.buscaMusica().trim();
    if (!termo) return;
    this.buscando.set(true);
    this.api.buscarMusicas(termo, 1, 8).subscribe({
      next: r => { this.resultadoBusca.set(r.itens); this.buscando.set(false); },
      error: () => this.buscando.set(false)
    });
  }

  adicionarMusica(musicaId: string) {
    const playlist = this.playlistAberta();
    if (!playlist) return;
    this.adicionando.set(musicaId);
    this.api.adicionarMusicaPlaylist(this.uid, playlist.id, musicaId).subscribe({
      next: () => {
        this.adicionando.set(null);
        this.api.obterPlaylist(this.uid, playlist.id).subscribe({ next: p => this.playlistAberta.set(p) });
      },
      error: () => this.adicionando.set(null)
    });
  }

  removerMusica(musicaId: string) {
    const playlist = this.playlistAberta();
    if (!playlist) return;
    this.removendo.set(musicaId);
    this.api.removerMusicaPlaylist(this.uid, playlist.id, musicaId).subscribe({
      next: () => {
        this.removendo.set(null);
        this.api.obterPlaylist(this.uid, playlist.id).subscribe({ next: p => this.playlistAberta.set(p) });
      },
      error: () => this.removendo.set(null)
    });
  }

  formatarDuracao(d: string): string {
    const [, m, s] = d.split(':');
    const mm = parseInt(m || '0'), ss = Math.floor(parseFloat(s || '0'));
    return `${mm}:${ss.toString().padStart(2, '0')}`;
  }

  jaNaPlaylist(musicaId: string): boolean {
    return this.playlistAberta()?.musicas.some(m => m.id === musicaId) ?? false;
  }
}
