import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-adwerts-list',
  templateUrl: './adwerts-list.component.html',
  styleUrls: ['./adwerts-list.component.scss']
})
export class AdwertsListComponent implements OnInit {

  numberOfAdwerts: number =0;


  constructor(
    private router: Router
  ) {}

  ngOnInit(): void {
  }

  search()
  {

  }

  adwert(){
    this.router.navigate(['start']);
  }

}
