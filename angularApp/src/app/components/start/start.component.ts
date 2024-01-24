import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { LOCALSTORAGE_TOKEN_KEY } from 'src/app/app.module';
import {SearchService} from "../../services/search/search.service";
import {AdvertService} from "../../services/advert/advert.service";
import {Advert} from "../../model/interfaces";

@Component({
  selector: 'start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {

  searchPhrase: string = '';
  findAdverts : Advert[] = [];

  constructor(
      private router: Router,
      private searchService: SearchService,
      private advertService: AdvertService
    ) {}

  ngOnInit(): void {
  }

  search() {
    console.log('Wartość wprowadzona:', this.searchPhrase);
    this.searchService.getSearch(this.searchPhrase);
    this.findAdverts =this.searchService.findAdverts;
    console.log(this.findAdverts);
  }

  list(){
    this.router.navigate(['list']);
  }

}
