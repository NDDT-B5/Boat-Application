import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { BoatsComponent } from './boats/boats.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'boats', component: BoatsComponent },
    { path: '', redirectTo: 'login', pathMatch: 'full' }
];
