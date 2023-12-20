import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import {LOCALSTORAGE_TOKEN_KEY} from "../../app.module";

@Component({
  selector: 'main-view',
  templateUrl: './main-view.component.html',
  styleUrls: ['./main-view.component.scss']
})
export class MainViewComponent {
  title = 'angularApp';
  isLogIn: boolean;

  constructor(
    private router: Router
  ) {
    //TODO: chwolowo ustawione żeby było widać przycisk, docelowo trzeba sprawdzać czy zalogowany
    this.isLogIn = true;
  }

  openStartPage(){
    this.router.navigate(['start']);
  }

  logout(){
    localStorage.removeItem(LOCALSTORAGE_TOKEN_KEY);
    this.router.navigate(['login']);
  }

  login(){
    this.router.navigate(['login']);
  }

  add(){
    this.router.navigate(['add']);
    this.router.navigate(['add']);
  }

}
