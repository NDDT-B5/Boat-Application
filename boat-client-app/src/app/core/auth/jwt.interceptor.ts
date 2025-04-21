import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from './auth.service';
import { catchError, switchMap, take, throwError } from 'rxjs';
import { Router } from '@angular/router';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const authService = inject(AuthService);

  return authService.getTokenObservable().pipe(
    take(1),
    switchMap((token) => {
      let modifiedReq = req;

      if (token) {
        modifiedReq = req.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`,
          },
        });
      }

      return next(modifiedReq).pipe(
        catchError((error) => {
          if (error.status === 401) {
            authService.removeToken();
            router.navigate(['/login']);
          }
          return throwError(() => error);
        })
      );
    })
  );
};
