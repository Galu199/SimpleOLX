import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
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
  searchPhrase: string |number| null = '';

  constructor( private router: Router,
               private searchService: SearchService,
               private advertService: AdvertService,
               private route: ActivatedRoute
  ) {
    this.adverts = this.advertService.getAdvertsList()
  }

  ngOnInit(): void {
    this.searchPhrase = this.route.snapshot.paramMap.get('search');
    if(this.searchPhrase === ''){
      //jeśli nie było szukane wyświetlamy wszytsko
      this.advertService.getAdverts();
      console.log('przekazany został pusty string')

    }
    else if(this.searchPhrase === null) {
      //było szukane ale nie znaleźliśmy nic
      console.log('przekazany został null')
    }

    else{
      this.search(this.searchPhrase);
      console.log('przekazane zostało słowo')
    }

  }


  search(phrase: string) {
  if(this.searchPhrase === '0' || this.searchPhrase === '1' || this.searchPhrase === '2' || this.searchPhrase === '1'
      || this.searchPhrase === '4' )   {
      this.searchService.getCategoryAdverts(this.searchPhrase);
      console.log(parseInt(this.searchPhrase));
    }
  else{
      this.searchService.getSearch(phrase);
      console.log(this.searchPhrase)
    }
    this.findAdverts = this.searchService.findAdverts;

    console.log("this.findAdverts from start component")
    console.log(this.findAdverts)
  }


  advert(){
    this.router.navigate(['start']);
  }

}
