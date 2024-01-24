import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { MatCardModule } from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MainViewComponent } from './components/main-view/main-view.component';

import { StartComponent } from './components/start/start.component';
import { AddAdvertComponent } from './components/add-advert/add-advert.component';
import { AdvertsListComponent } from './components/adverts-list/adverts-list.component';
import { JwtInterceptor } from './components/interceptors/jwt.interceptor';
import { AdvertViewComponent } from './components/advert-view/advert-view.component';

// specify the key where the token is stored in the local storage
export const LOCALSTORAGE_TOKEN_KEY = 'angularApp';

@NgModule({
  declarations: [
    MainViewComponent,
    StartComponent,
    LoginComponent,
    RegisterComponent,
    AddAdvertComponent,
    AdvertsListComponent,
    AdvertViewComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    CommonModule,

    ReactiveFormsModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule,
    MatSelectModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
     }
  ],
  bootstrap: [MainViewComponent]
})
export class AppModule { }
