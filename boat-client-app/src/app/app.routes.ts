import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { authGuard } from './core/auth/auth.guard';
import { BoatDetailComponent } from './components/boats/boat-detail/boat-detail.component';
import { BoatsComponent } from './components/boats/boats.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'boats', component: BoatsComponent, canActivate: [authGuard] },
    { path: 'boats/:id', component: BoatDetailComponent, canActivate: [authGuard] },
    { path: '', redirectTo: 'login', pathMatch: 'full' }
];
