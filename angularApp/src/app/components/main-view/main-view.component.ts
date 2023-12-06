import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { LOCALSTORAGE_TOKEN_KEY } from 'src/app/app.module';

@Component({
  selector: 'main-view',
  templateUrl: './main-view.component.html',
  styleUrls: ['./main-view.component.scss']
})
export class MainViewComponent {

  constructor(
    private router: Router
  ) {}

  logout() {
    localStorage.removeItem(LOCALSTORAGE_TOKEN_KEY);
    this.router.navigate(['../../']);
  }

}
