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

  //adverts = new List<Advert>();

  constructor(private httpService: HttpClient,
              private router: Router) {
     //this.getAdverts();
  }

  getAdverts(){
    const adverts = this.httpService.get(environment.apiURL + "Adverts", {
                   headers: { 'Content-Type': 'application/json'},
                   responseType: 'text'
                 }
               );
  }

   postAdvert(newAdvert: Advert) {
    return this.httpService.post(environment.apiURL + "Adverts", newAdvert, {responseType: 'text'})
    .pipe(
      tap(newA => console.log(newA)),
      catchError(this.handleError)
    );
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
