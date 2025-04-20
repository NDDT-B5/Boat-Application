import { Component, computed, signal } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { filter } from 'rxjs';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './components/navigation/navigation.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavigationComponent, CommonModule ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'boat-client-app';
  currentUrl = signal('');
  urlsWithoutMenu = ['/login'];

  constructor(router: Router) {
    router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        console.log(event.urlAfterRedirects);
        this.currentUrl.set(event.urlAfterRedirects);
      });
  }

  showNavigation = computed(() => {
    const url = this.currentUrl();
    return !this.urlsWithoutMenu.some(path => url.startsWith(path));
  });
}
