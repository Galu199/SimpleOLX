import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

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
    private router: Router
  ) { }


  public login() : void {
    if (!this.loginForm.valid) return;
    
    this.authService.login(this.loginForm.value).subscribe({
      next: (value: string) => { this.router.navigate(['../start']); },
      error: (err: HttpErrorResponse) => { console.log(err.message); }
    });

  }
}
