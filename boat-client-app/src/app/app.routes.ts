import { Routes } from '@angular/router';
import { BoatsComponent } from './boats/boats.component';
import { LoginComponent } from './auth/login/login.component';
import { authGuard } from './auth/auth.guard';
import { BoatDetailComponent } from './boats/boat-detail/boat-detail.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'boats', component: BoatsComponent, canActivate: [authGuard] },
    { path: 'boats/:id', component: BoatDetailComponent, canActivate: [authGuard] },
    { path: '', redirectTo: 'login', pathMatch: 'full' }
];
