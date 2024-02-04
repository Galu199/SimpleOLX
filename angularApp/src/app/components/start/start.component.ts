import { Router } from '@angular/router';
import {Component, Input, OnInit} from '@angular/core';
import {AdvertService} from "../../services/advert/advert.service";
import {FormBuilder, Validators} from "@angular/forms";

/**
 * Klasa obsługująca główny widok - kategorii i wyszukiwania
 */
@Component({
  selector: 'start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {

  @Input('phrase')
  searchPhrase: string = '';

  searchGroup = this.formBuilder.group({
    phrase: [''],
  });

  constructor(
      private router: Router,
      private advertService: AdvertService,
      private formBuilder: FormBuilder
    ) {}

  ngOnInit(): void {
    this.advertService.deleteAdvertsFromTable();
  }

  //Funkcja kierująca do wyszukiwanie ofert
  search(){
    console.log("start component search()")
    console.log(this.searchPhrase)
    this.router.navigate(['list', this.searchPhrase]);
  }

  //Funkcja kierująca do listy ofert z danej kategorii
  list(category:number){
    console.log("start component list()")
    this.router.navigate(['list', category]);
  }

}
