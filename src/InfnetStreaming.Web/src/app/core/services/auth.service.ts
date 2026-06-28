import { Injectable, computed, signal } from '@angular/core';
import { Router } from '@angular/router';

export interface CurrentUser {
  id: string;
  username: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly _token = signal<string | null>(localStorage.getItem('token'));

  readonly currentUser = computed<CurrentUser | null>(() => {
    const t = this._token();
    if (!t) return null;
    try {
      const payload = JSON.parse(atob(t.split('.')[1].replace(/-/g, '+').replace(/_/g, '/')));
      return { id: payload['sub'], username: payload['unique_name'] };
    } catch {
      return null;
    }
  });

  readonly isAuthenticated = computed(() => !!this.currentUser());

  constructor(private router: Router) {}

  setToken(token: string): void {
    localStorage.setItem('token', token);
    this._token.set(token);
  }

  logout(): void {
    localStorage.removeItem('token');
    this._token.set(null);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return this._token();
  }
}
