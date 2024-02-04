import { Injectable } from '@angular/core';
import { Advert } from '../../model/interfaces';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { BehaviorSubject, Observable, throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { AdvertCategory, allAdvertsCategories } from "../../model/types";


/**
 * Serwis obsługujący endpointy backendu dotyczące ofert (Adverts) i ich wyszukiwania (Search)
 */
@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  private advertsSubject: BehaviorSubject<Advert[]> = new BehaviorSubject<Advert[]>([]);

  adverts$: Observable<Advert[]> = this.advertsSubject.asObservable();

  constructor(private httpService: HttpClient, private router: Router) {
    //this.getAdverts();
  }

  //Funkcja zerująca tablicę ofert
  deleteAdvertsFromTable(){
    this.adverts$ = new BehaviorSubject<Advert[]>([])
    console.log('delete form table method')
  }

  //Pobieranie wszytskich ofert dostępnych w serwisie
  getAdverts() {
    this.httpService.get<Advert[]>(environment.apiURL + 'Adverts').pipe(
      tap(adverts => {
        this.advertsSubject.next(adverts);
        console.log(adverts);
      }),
      catchError(this.handleError)
    ).subscribe();
  }

  //Funkcja dodająca nową ofertę
  postAdvert(newAdvert: Advert) {
    return this.httpService.post(environment.apiURL + "Adverts", newAdvert, { responseType: 'text' })
      .pipe(
        tap(newA => console.log(newA)),
        catchError(this.handleError)
      );
  }

  //Funkcja pobierająca dane oferty o podanym id
  public getOneAdvert(id: number): Observable<Advert> {
    return this.httpService.get<Advert>(environment.apiURL + 'Adverts/' + id)
      .pipe(
        catchError(this.handleError)
      );
  }

  //Funkcja pobierająca tablicę ofert, które pasują do podanej frazy
  getSearch(phrase: string) {
    const params = new HttpParams().set('phrase', phrase);

    this.httpService.get<Advert[]>(environment.apiURL + 'Search', {
      params: params,
      responseType: 'json'
    }).pipe(
      tap(adverts => {
        this.advertsSubject.next(adverts);
        console.log("service: ");
        console.log(adverts);
      }),
      catchError(this.handleError)
    ).subscribe();
  }

  //Funkcja pobierająca tablicę ofert z danej kategorii
  getCategoryAdverts(category: string) {
    let allCategories: AdvertCategory[] = [...allAdvertsCategories];
    const params = new HttpParams().set('category', allCategories[parseInt(category)]);

    this.httpService.get<Advert[]>(environment.apiURL + 'Search/category/' + allCategories[parseInt(category)], {
      responseType: 'json'
    }).pipe(
      tap(adverts => {
        this.advertsSubject.next(adverts);
        console.log("service: ");
        console.log(adverts);
      }),
      catchError(this.handleError)
    ).subscribe();
  }

  //Funkcja do obsługi wyjątków
  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      errorMessage = `Server returned code: ${error.status}, error message is ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }
}
