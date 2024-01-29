import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { MainViewComponent } from './components/main-view/main-view.component';
import { StartComponent } from "./components/start/start.component";
import {AddAdvertComponent} from "./components/add-advert/add-advert.component";
import {AdvertsListComponent} from "./components/adverts-list/adverts-list.component";

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'main', component: MainViewComponent },
  { path: 'start', component: StartComponent },
  { path: 'add', component: AddAdvertComponent },
  { path: 'list/:search', component: AdvertsListComponent },
  { path: 'list/:category', component: AdvertsListComponent },
  { path: '**', redirectTo: 'start',pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
