import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {SearchService} from "../../services/search/search.service";
import {Advert} from "../../model/interfaces";
import {AdvertService} from "../../services/advert/advert.service";

@Component({
  selector: 'app-adverts-list',
  templateUrl: './adverts-list.component.html',
  styleUrls: ['./adverts-list.component.scss']
})
export class AdvertsListComponent implements OnInit {

  numberOfAdverts: number =0;

  findAdverts : Advert[] = [];
  adverts : Advert[] = [];
  searchPhrase: string = '';

  constructor( private router: Router,
               private searchService: SearchService,
               private advertService: AdvertService
  ) {
    this.adverts = this.advertService.getAdvertsList()
  }

  ngOnInit(): void {

  }

  search() {
    this.searchService.getSearch(this.searchPhrase);
    this.findAdverts =this.searchService.findAdverts;
    console.log(this.findAdverts);
  }

  advert(){
    this.router.navigate(['start']);
  }

}
