import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import {MainViewComponent} from "../main-view/main-view.component";

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  loginForm: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required])
  });

  constructor(
    private authService: AuthService,
    private router: Router,
    private mainComponent: MainViewComponent
  ) { }


  public login() : void {
    if (!this.loginForm.valid) return;

    this.authService.login(this.loginForm.value).subscribe({
      next: (value: string) => {
        this.mainComponent.checkIfLogIn();
        this.router.navigate(['/start']);
      },
      error: (err: HttpErrorResponse) => { console.log(err.message); }
    });

  }
}
