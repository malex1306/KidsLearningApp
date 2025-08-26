// auth.interceptor.ts
import {HttpRequest, HttpHandlerFn, HttpEvent} from '@angular/common/http';
import {Observable} from 'rxjs';

export const AuthInterceptor = (req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> => {
  const token = sessionStorage.getItem('jwt_token');

  if (token) {
    const authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
    return next(authReq);
  }

  return next(req);
};
