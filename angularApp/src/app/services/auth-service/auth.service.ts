import { LOCALSTORAGE_TOKEN_KEY } from './../../app.module';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, Observable, of, Subscription, tap } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoginRequest, RegisterRequest } from '../../model/interfaces';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenSubscription = new Subscription()

  constructor(
    private httpService: HttpClient,
    private snackBar: MatSnackBar,
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

          const expirationTimeInMilisecondsSinceEpoch: number = Number(jwtDecode<{ [key: string]: string }>(token, { header: false })['exp']) * 1000
          const milisecondsSinceEpoch: number = new Date().getTime()
          const offset: number = expirationTimeInMilisecondsSinceEpoch - milisecondsSinceEpoch

          this.tokenSubscription.unsubscribe()
          this.tokenSubscription = of(null).pipe(
            delay(offset)
            ).subscribe(() => {
              this.logout()
              this.snackBar.open('Session timeout', 'Close', { duration: 2000, horizontalPosition: 'right', verticalPosition: 'top' })
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

  public register(registerRequest: RegisterRequest) : Observable<string> {
    return this.httpService.post(environment.apiURL + "Identity/register", registerRequest, { 
      headers: { 'Content-Type': 'application/json'},
      responseType: 'text'
    });
  }

  public getToken(): string | null {
    return localStorage.getItem(LOCALSTORAGE_TOKEN_KEY)
  }

  public getUserId(): number {
    return Number(jwtDecode<{ [key: string]: string }>(localStorage.getItem(LOCALSTORAGE_TOKEN_KEY)!, { header: false })['sub'])
  }

}
