import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-adverts-list',
  templateUrl: './adverts-list.component.html',
  styleUrls: ['./adverts-list.component.scss']
})
export class AdvertsListComponent implements OnInit {

  numberOfAdverts: number =0;


  constructor(
    private router: Router
  ) {}

  ngOnInit(): void {
  }

  search()
  {

  }

  advert(){
    this.router.navigate(['start']);
  }

}
