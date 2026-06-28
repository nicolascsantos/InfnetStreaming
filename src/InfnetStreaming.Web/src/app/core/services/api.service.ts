import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

const API = 'http://localhost:5103';

export interface LoginResponse {
  token: string;
  expira_em: string;
  usuario: { id: string; nome: string; username: string };
}

export interface CriarUsuarioInput {
  nome: string;
  username: string;
  senha: string;
  plano_id: string;
}

export interface PlanoOutput {
  id: string;
  nome: string;
  valor: number;
}

export interface UsuarioOutput {
  id: string;
  nome: string;
  username: string;
  plano_id: string;
  nome_plano: string;
  valor_plano: number;
  data_criada: string;
}

export interface BandaOutput {
  id: string;
  nome: string;
  data_formacao: string;
  data_criacao: string;
}

export interface MusicaOutput {
  id: string;
  nome: string;
  duracao: string;
  ordem_musica: number;
  data_criacao: string;
}

export interface BuscaResult<T> {
  pagina_atual: number;
  qtd_por_pagina: number;
  total: number;
  itens: T[];
}

export interface FavoritosOutput {
  musicas_favoritas: { musica_id: string; nome: string; duracao: string }[];
  bandas_favoritas: { banda_id: string; nome: string }[];
}

export interface AssinarOutput {
  transacao_id: string;
  status: number;
  mensagem_exibicao: string;
  usuario_id: string;
  plano_id: string;
  valor: number;
}

export interface AutorizarOutput {
  transacao_id: string;
  status: number;
  mensagem_exibicao: string;
  valor: number;
  data_transacao: string;
}

export interface MusicaAlbumOutput {
  id: string;
  nome: string;
  duracao: string;
  ordem_musica: number;
}

export interface AlbumOutput {
  id: string;
  nome: string;
  data_lancamento: string;
  musicas: MusicaAlbumOutput[];
}

export interface BandaDetalheOutput {
  id: string;
  nome: string;
  data_formacao: string;
  integrantes: { id: string; nome: string; data_de_nascimento: string }[];
  generos: string[];
  albuns: AlbumOutput[];
  singles: { id: string; nome: string; duracao: string }[];
}

export interface PlaylistOutput {
  id: string;
  nome: string;
  data_criacao: string;
}

export interface PlaylistDetalheOutput {
  id: string;
  nome: string;
  data_criacao: string;
  musicas: { id: string; nome: string; duracao: string; ordem_musica: number }[];
}

@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);

  login(username: string, senha: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${API}/usuarios/login`, { username, senha });
  }

  criarUsuario(input: CriarUsuarioInput): Observable<any> {
    return this.http.post(`${API}/usuarios`, input);
  }

  obterUsuario(id: string): Observable<UsuarioOutput> {
    return this.http.get<UsuarioOutput>(`${API}/usuarios/${id}`);
  }

  listarPlanos(): Observable<PlanoOutput[]> {
    return this.http.get<PlanoOutput[]>(`${API}/planos`);
  }

  buscarBandas(search: string, page = 1, perPage = 10): Observable<BuscaResult<BandaOutput>> {
    const params = new HttpParams()
      .set('search', search).set('page', page).set('perPage', perPage);
    return this.http.get<BuscaResult<BandaOutput>>(`${API}/busca/bandas`, { params });
  }

  buscarMusicas(search: string, page = 1, perPage = 10): Observable<BuscaResult<MusicaOutput>> {
    const params = new HttpParams()
      .set('search', search).set('page', page).set('perPage', perPage);
    return this.http.get<BuscaResult<MusicaOutput>>(`${API}/busca/musicas`, { params });
  }

  obterBanda(bandaId: string): Observable<BandaDetalheOutput> {
    return this.http.get<BandaDetalheOutput>(`${API}/bandas/${bandaId}`);
  }

  obterFavoritos(usuarioId: string): Observable<FavoritosOutput> {
    return this.http.get<FavoritosOutput>(`${API}/usuarios/${usuarioId}/favoritos`);
  }

  favoritarMusica(usuarioId: string, musicaId: string): Observable<void> {
    return this.http.post<void>(`${API}/usuarios/${usuarioId}/favoritos/musicas/${musicaId}`, {});
  }

  desfavoritarMusica(usuarioId: string, musicaId: string): Observable<void> {
    return this.http.delete<void>(`${API}/usuarios/${usuarioId}/favoritos/musicas/${musicaId}`);
  }

  favoritarBanda(usuarioId: string, bandaId: string): Observable<void> {
    return this.http.post<void>(`${API}/usuarios/${usuarioId}/favoritos/bandas/${bandaId}`, {});
  }

  desfavoritarBanda(usuarioId: string, bandaId: string): Observable<void> {
    return this.http.delete<void>(`${API}/usuarios/${usuarioId}/favoritos/bandas/${bandaId}`);
  }

  assinar(usuarioId: string, planoId: string): Observable<AssinarOutput> {
    return this.http.post<AssinarOutput>(`${API}/assinaturas`, { usuario_id: usuarioId, plano_id: planoId });
  }

  autorizarTransacao(usuarioId: string, planoId: string, valor: number): Observable<AutorizarOutput> {
    return this.http.post<AutorizarOutput>(`${API}/transacoes/autorizar`, {
      usuario_id: usuarioId, plano_id: planoId, valor
    });
  }

  listarPlaylists(usuarioId: string): Observable<PlaylistOutput[]> {
    return this.http.get<PlaylistOutput[]>(`${API}/usuarios/${usuarioId}/playlists`);
  }

  criarPlaylist(usuarioId: string, nome: string): Observable<PlaylistOutput> {
    return this.http.post<PlaylistOutput>(`${API}/usuarios/${usuarioId}/playlists`, { nome });
  }

  obterPlaylist(usuarioId: string, playlistId: string): Observable<PlaylistDetalheOutput> {
    return this.http.get<PlaylistDetalheOutput>(`${API}/usuarios/${usuarioId}/playlists/${playlistId}`);
  }

  deletarPlaylist(usuarioId: string, playlistId: string): Observable<void> {
    return this.http.delete<void>(`${API}/usuarios/${usuarioId}/playlists/${playlistId}`);
  }

  adicionarMusicaPlaylist(usuarioId: string, playlistId: string, musicaId: string): Observable<void> {
    return this.http.post<void>(`${API}/usuarios/${usuarioId}/playlists/${playlistId}/musicas/${musicaId}`, {});
  }

  removerMusicaPlaylist(usuarioId: string, playlistId: string, musicaId: string): Observable<void> {
    return this.http.delete<void>(`${API}/usuarios/${usuarioId}/playlists/${playlistId}/musicas/${musicaId}`);
  }
}
