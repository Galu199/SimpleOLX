import { AuthService } from './../../services/auth-service/auth.service'
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    let JWT = this.authService.getToken()

    if (JWT) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${JWT}`,
        },
      })
    }

    return next.handle(request)
  }
}
