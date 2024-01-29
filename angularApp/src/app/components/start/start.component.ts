import { Router } from '@angular/router';
import {Component, Input, OnInit} from '@angular/core';
import {SearchService} from "../../services/search/search.service";
import {AdvertService} from "../../services/advert/advert.service";
import {FormBuilder, Validators} from "@angular/forms";

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
      private searchService: SearchService,
      private advertService: AdvertService,
      private formBuilder: FormBuilder
    ) {}

  ngOnInit(): void {
  }

  search(){
    console.log("start component search()")
    console.log(this.searchPhrase)
    this.router.navigate(['list', this.searchPhrase]);
  }

  list(category:number){
    console.log("start component list()")
    this.router.navigate(['list', category]);
  }

}
