import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Router} from "@angular/router";
import { Advert } from '../../model/interfaces';
import { AdvertCategory, allAdvertsCategories } from 'src/app/model/types';


@Injectable({
  providedIn: 'root'
})
export class SearchService {

  findAdverts : Advert[] = [];

  constructor(private httpService: HttpClient,
              private router: Router) {

  }

  getSearch(phrase:string) {
    const params = new HttpParams().set('phrase', phrase);

    this.httpService.get<Advert[]>(environment.apiURL + 'Search', {
      params: params,
      responseType: 'json'
    }).subscribe({
      next:  (adverts: Advert[]) => {
        this.findAdverts = adverts;
        console.log("service: ");
        console.log(this.findAdverts);

      },
      error: (error) => {
        console.error('Wystąpił błąd podczas wyszukiwania:', error);
      }
    });
    console.log('getSearch')
  }

  getCategoryAdverts(category: string) {
    let allCategories: AdvertCategory[] = [...allAdvertsCategories]
    const params = new HttpParams().set('category', allCategories[parseInt(category)]);

    this.httpService.get<Advert[]>(environment.apiURL + 'Search/category', {
      params: params,
      responseType: 'json'
    }).subscribe({
      next:  (adverts: Advert[]) => {
        this.findAdverts = adverts;
        console.log("service: ");
        console.log(this.findAdverts);

      },
      error: (error) => {
        console.error('Wystąpił błąd podczas wyszukiwania:', error);
      }
    });
    console.log('getSearch')
  }


}
