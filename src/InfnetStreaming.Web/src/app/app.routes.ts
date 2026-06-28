import { Routes } from '@angular/router';
import { authGuard, publicGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'busca', pathMatch: 'full' },
  {
    path: 'login',
    canActivate: [publicGuard],
    loadComponent: () => import('./pages/login/login').then(m => m.LoginPage)
  },
  {
    path: 'criar-conta',
    canActivate: [publicGuard],
    loadComponent: () => import('./pages/criar-conta/criar-conta').then(m => m.CriarContaPage)
  },
  {
    path: 'busca',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/busca/busca').then(m => m.BuscaPage)
  },
  {
    path: 'favoritos',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/favoritos/favoritos').then(m => m.FavoritosPage)
  },
  {
    path: 'assinatura',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/assinatura/assinatura').then(m => m.AssinaturaPage)
  },
  {
    path: 'transacao',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/transacao/transacao').then(m => m.TransacaoPage)
  },
  {
    path: 'bandas/:id',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/banda-detalhe/banda-detalhe').then(m => m.BandaDetalhePage)
  },
  {
    path: 'playlists',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/playlists/playlists').then(m => m.PlaylistsPage)
  },
  { path: '**', redirectTo: 'busca' }
];
