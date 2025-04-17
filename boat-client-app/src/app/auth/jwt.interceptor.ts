import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from './auth.service';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();
  const router = inject(Router);
  
  if (token) {
    const clonedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
    return next(clonedRequest).pipe(
      catchError((error) => {
        if (error.status === 401) {
          // Token is expired or invalid
          authService.removeToken();
          router.navigate(['/login']);  // Redirect to login page
        }
        return throwError(() => error);
      })
    );;
  }

  return next(req);
};
