import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Router} from "@angular/router";
import { Advert } from '../../model/interfaces';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  findAdverts : Advert[] = [];

  constructor(private httpService: HttpClient,
              private router: Router) {

  }

  getSearch(phraze:string){
    const params = new HttpParams().set('phraze', phraze);

    this.httpService.get<Advert[]>(environment.apiURL + 'Search', {
      params: params,
      responseType: 'json',
    }).subscribe(
      (adverts: Advert[]) => {
        this.findAdverts = adverts;
      },
      (error) => {
        console.error('Wystąpił błąd podczas wyszukiwania:', error);
      }
    );
  }


}
