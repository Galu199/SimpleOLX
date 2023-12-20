import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { MainViewComponent } from './components/main-view/main-view.component';
import { StartComponent } from "./components/start/start.component";
import {AddAdwertComponent} from "./components/add-adwert/add-adwert.component";
import {AdwertsListComponent} from "./components/adwerts-list/adwerts-list.component";

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'main', component: MainViewComponent },
  { path: 'start', component: StartComponent },
  { path: 'add', component: AddAdwertComponent },
  { path: 'list', component: AdwertsListComponent },
  { path: '**', redirectTo: 'login',pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
