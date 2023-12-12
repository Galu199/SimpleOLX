import { LOCALSTORAGE_TOKEN_KEY } from './../../app.module';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, Observable, of, Subscription, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoginRequest } from '../../model/interfaces';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenSubscription = new Subscription()

  constructor(
    private httpService: HttpClient,
    private snackbar: MatSnackBar,
    private jwtService: JwtHelperService,
    private router: Router
  ) { }

  public login(loginRequest: LoginRequest): Observable<string> {
    return this.httpService.post(environment.apiURL + "Identity/login", loginRequest, { 
        headers: { 'Content-Type': 'application/json'},
        responseType: 'text' 
      }
    ).pipe(
      tap(token => { 
          localStorage.setItem(LOCALSTORAGE_TOKEN_KEY, token);

          this.tokenSubscription.unsubscribe()
          this.tokenSubscription = of(null).pipe(
            delay(this.jwtService.getTokenExpirationDate(token)!.getTime() - new Date().getTime())
            ).subscribe(() => {
              this.logout()
              this.snackbar.open('Session timeout', 'Close', { duration: 2000, horizontalPosition: 'right', verticalPosition: 'top' })
            }
          )
        }
      )
    )
  } 

  public logout(): void {
    this.tokenSubscription.unsubscribe()
    localStorage.removeItem(LOCALSTORAGE_TOKEN_KEY)
    this.router.navigate(['/login'])
  }
}
