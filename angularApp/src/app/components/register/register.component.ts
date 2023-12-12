import { MatSnackBar } from '@angular/material/snack-bar'
import { HttpErrorResponse } from '@angular/common/http'
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { CustomValidators } from '../../model/custom-validator';
import { AuthService } from '../../services/auth-service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  registerForm = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    firstname: new FormControl(null, [Validators.required]),
    lastname: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required]),
    passwordConfirm: new FormControl(null, [Validators.required])
  },
    { validators: CustomValidators.passwordsMatching }
  )

  constructor(
    private router: Router,
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) { }

  public onRegister(): void {
    if (!this.registerForm.valid) return;
    
    this.registerForm.removeControl('passwordConfirm');
    this.authService.register(this.registerForm.value).subscribe({
      next: (value: string) => {
        this.snackBar.open(value, 'Close', { duration: 2000, horizontalPosition: 'right', verticalPosition: 'top' })
        this.router.navigate(['../login']); 
      },
      error: (err: HttpErrorResponse) => { console.log(err.message); }
    });
  }

}
