import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { LOCALSTORAGE_TOKEN_KEY } from 'src/app/app.module';

@Component({
  selector: 'start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {

  constructor(
      private router: Router
    ) {}

  ngOnInit(): void {
  }

  search()
  {

  }

  list(){
    this.router.navigate(['list']);
  }

}
