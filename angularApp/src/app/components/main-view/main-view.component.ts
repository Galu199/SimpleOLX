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
  public isLogIn: boolean;

  constructor(
    private router: Router,

  ) {
    this.isLogIn = false;
  }

  public checkIfLogIn(){
    //TODO: przeładowanie strony po zalogowaniu żeby przyciski się uaktywniły??
    if(localStorage.getItem(LOCALSTORAGE_TOKEN_KEY)){
      this.isLogIn = true;
    }
    else
      this.isLogIn = false;
  }

  openStartPage(){
    this.router.navigate(['start']);
  }

  logout(){
    localStorage.removeItem(LOCALSTORAGE_TOKEN_KEY);
    this.isLogIn = false;
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
