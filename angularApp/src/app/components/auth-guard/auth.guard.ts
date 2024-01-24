import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth-service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    // isTokenExpired() will return true, if either:
    // - token is expired
    // - no token or key/value pair in localStorage
    // - ... (since the backend should validate the token, even if there is another false token, 
    //       then he can access the frontend route, but will not get any data from the backend)
    // --> then redirect to the base route and deny the routing
    // --> else return true and allow the routing
    if (!this.authService.getToken()) {
      this.router.navigate(['']);
      return false;
    } else {
      return true;
    }
  }
}
