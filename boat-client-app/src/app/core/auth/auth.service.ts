import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthResponse } from '../../shared/models/authResponse.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${environment.apiBaseUrl}/auth`;
  private tokenSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(this.getToken());

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.apiUrl}/login`, { username, password })
      .pipe(
        tap((response) => {
          this.setToken(response.jwtToken);
        }),
        catchError((error) => {
          const message = error?.error?.message || error?.message || 'An unknown error occurred';
          throw new Error(message);
        })
      );
  }

  isLoggedIn(): boolean {
    return !!this.tokenSubject.value;
  }

  private getToken(): string | null {
    return localStorage.getItem('jwt_token');
  }

  setToken(token: string): void {
    localStorage.setItem('jwt_token', token);
    this.tokenSubject.next(token);
  }

  removeToken(): void {
    localStorage.removeItem('jwt_token');
    this.tokenSubject.next(null);
  }

  getTokenObservable() {
    return this.tokenSubject.asObservable();
  }
}
