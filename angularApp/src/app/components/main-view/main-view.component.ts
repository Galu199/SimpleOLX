import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'main-view',
  templateUrl: './main-view.component.html',
  styleUrls: ['./main-view.component.scss']
})
export class MainViewComponent {
  title = 'angularApp';

  constructor(
    private router: Router
  ) {}

  openStartPage(){
    this.router.navigate(['start']);
  }

  logout(){

  }

}
