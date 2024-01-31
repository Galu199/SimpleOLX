import {Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { AdvertService } from "../../services/advert/advert.service";
import { Advert } from "../../model/interfaces";
import {FormBuilder} from "@angular/forms";
import { AdvertCategory, allAdvertsCategories } from "../../model/types";

@Component({
  selector: 'app-adverts-list',
  templateUrl: './adverts-list.component.html',
  styleUrls: ['./adverts-list.component.scss']
})
export class AdvertsListComponent implements OnInit {

  numberOfAdverts: number = 0;
  findAdverts: Advert[] = [];

  @Input('phrase')
  searchPhrase: string | number | null = '';

  searchGroup = this.formBuilder.group({
    phrase: [''],
  });

  constructor(
    private router: Router,
    private advertService: AdvertService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.searchPhrase = this.route.snapshot.paramMap.get('search');
    if (this.searchPhrase === null) {
      console.log('Przekazany został null');
    } else {
      if (this.searchPhrase === '0' ||
        this.searchPhrase === '1' ||
        this.searchPhrase === '2' ||
        this.searchPhrase === '3' ||
        this.searchPhrase === '4') {
        this.search();
      } else {
        this.advertService.getAdverts();
        this.showList();
      }
    }
  }

  showList(){
    this.advertService.adverts$.subscribe((adverts) => {
      this.findAdverts = adverts;
      //this.przypisz_adverty(); // Tymczasowe
      this.numberOfAdverts = this.findAdverts.length;
      console.log("this.findAdverts from start component");
      console.log(this.findAdverts);
    });

  }

  search() {
    if(typeof this.searchPhrase == 'string'){
      this.advertService.getSearch(this.searchPhrase);
    }
    this.advertService.adverts$.subscribe((adverts) => {
      this.findAdverts = adverts;
      //this.przypisz_adverty(); // Tymczasowe
      this.numberOfAdverts = this.findAdverts.length;
      console.log("this.findAdverts from start component");
      console.log(this.findAdverts);
    });
  }

  advert() {
    this.router.navigate(['advert']);
  }

  //Funkcja używana tylko do testów bez backendu
  przypisz_adverty() {
    const fileContent = 'To jest zawartość sztucznego pliku.';
    const blob = new Blob([fileContent], { type: 'text/plain' });
    let file = new File([blob], 'fakeFile.txt');

    let advert: Advert = {
      title: "title" ,
      description: "decription",
      mail: "string",
      phoneNumber: "string",
      price: 100,
      localizationLatitude: 0,
      localizationLongitude: 0,
      category: "Electronics",
      image: file,
      userOwnerId: 0
    };

    this.findAdverts.push(advert);
    this.findAdverts.push(advert);
    this.findAdverts.push(advert);
  }
}
