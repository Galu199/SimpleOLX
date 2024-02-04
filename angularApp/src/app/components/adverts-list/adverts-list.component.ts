import {Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { AdvertService } from "../../services/advert/advert.service";
import { Advert } from "../../model/interfaces";
import {FormBuilder} from "@angular/forms";

/**
 * Klasa do wyświetlania listy ofert z danej kategorii lub po wprowadzeniu słowa kluczowego w wyszukiwarce
 */
@Component({
  selector: 'app-adverts-list',
  templateUrl: './adverts-list.component.html',
  styleUrls: ['./adverts-list.component.scss']
})
export class AdvertsListComponent implements OnInit {

  numberOfAdverts: number = 0;
  findAdverts: Advert[] = [];

  @Input('phrase')
  searchPhrase: string = '';

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
    const search = this.route.snapshot.paramMap.get('search');
    if (search === null) {
      console.log('Przekazany został null');
    } else {
      if (search === '0' ||
        search === '1' ||
        search === '2' ||
        search === '3' ||
        search === '4') {
        this.advertService.getAdverts();
        this.showList();
      } else {
        this.searchPhrase = search;
        this.search();
      }
    }
  }

  //Funkcja do wyświetlania listy ofert z danej kategorii
  showList(){
    this.advertService.adverts$.subscribe((adverts) => {
      this.findAdverts = adverts;
      this.przypisz_adverty(); // Tymczasowe
      this.numberOfAdverts = this.findAdverts.length;
      console.log("this.findAdverts from showList()");
      console.log(this.findAdverts);
    });

  }

  //Funkcja do wyświetlania listy ofert z po wpisanu słowa kluczowego w wyszukiwarke
  search() {
    this.advertService.deleteAdvertsFromTable();
    this.advertService.getSearch(this.searchPhrase);

    this.advertService.adverts$.subscribe((adverts) => {
      this.findAdverts = adverts;
      this.przypisz_adverty(); // Tymczasowe
      this.numberOfAdverts = this.findAdverts.length;
      console.log("this.findAdverts from start component");
      console.log(this.findAdverts);
    });
  }

  //Funkcja przekierowująca do widoku konkretnej oferty
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
      localizationLatitude: 1300,
      localizationLongitude: 1000,
      category: "Electronics",
      image: file,
      userOwnerId: 0
    };

    this.findAdverts.push(advert);
    this.findAdverts.push(advert);
    this.findAdverts.push(advert);
  }
}
