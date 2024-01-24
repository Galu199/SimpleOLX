import { Injectable } from '@angular/core';
import { Advert } from '../../model/interfaces';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {catchError, Observable, tap, throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  adverts : Advert[] = [];

  constructor(private httpService: HttpClient,
              private router: Router) {
      this.getAdverts();

  }

  getAdvertsList(){
    return this.adverts;
  }


   postAdvert(newAdvert: Advert) {
    return this.httpService.post(environment.apiURL + "Adverts", newAdvert, {responseType: 'text'})
    .pipe(
      tap(newA => console.log(newA)),
      catchError(this.handleError)
    );
  }

  getAdverts() {
    this.httpService.get<Advert[]>(environment.apiURL + 'Adverts').subscribe(
      (adverts: Advert[]) => {
        this.adverts = adverts;
      },
      (error) => {
        console.error('Wystąpił błąd podczas pobierania ogłoszeń: ', error);
      }
    );
  }

  public getOneAdvert( id: number) : Observable<Advert> {
    return this.httpService.get<Advert>(environment.apiURL + 'Adverts/' + id);
  }

   private handleError(err: HttpErrorResponse){
      let errorMessage = '';
      if(err.error instanceof ErrorEvent){
        errorMessage = 'An error occured: ${err.error.message}';
      } else {
        errorMessage = 'Server returned code: ${err.status},' +
          'error message is ${err.message}';
      }

      console.error(errorMessage);
      return throwError(() => errorMessage);

    }

}
