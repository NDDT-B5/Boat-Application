import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { Subject } from 'rxjs';
import { NavigationEnd, provideRouter, Router, Routes } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';

const routes: Routes = [];

describe('AppComponent', () => {

  describe('Standard cases', () => {
    beforeEach(async () => {
      await TestBed.configureTestingModule({
        imports: [AppComponent],
        providers: [provideHttpClient(), provideRouter(routes),],
      }).compileComponents();
    });

    it('should create the app', () => {
      const fixture = TestBed.createComponent(AppComponent);
      const app = fixture.componentInstance;
      expect(app).toBeTruthy();
    });

    it(`should have the 'boat-client-app' title`, () => {
      const fixture = TestBed.createComponent(AppComponent);
      const app = fixture.componentInstance;
      expect(app.title).toEqual('boat-client-app');
    });

    it('should hide navigation on /login and not render navigation component', () => {
      const fixture = TestBed.createComponent(AppComponent);
      const app = fixture.componentInstance;
    
      app.currentUrl.set('/login');
      expect(app.showNavigation()).toBeFalse();

      fixture.detectChanges();
    
      const compiled = fixture.nativeElement as HTMLElement;
      expect(compiled.querySelector('app-navigation')).toBeFalsy();
    });

    it('should show navigation on /boats and render navigation component', () => {
      const fixture = TestBed.createComponent(AppComponent);
      const app = fixture.componentInstance;
    
      app.currentUrl.set('/boats');
      expect(app.showNavigation()).toBeTrue();

      fixture.detectChanges();
  
      const compiled = fixture.nativeElement as HTMLElement;
      expect(compiled.querySelector('app-navigation')).toBeTruthy();
    });

  });

  describe('AppComponent Navigation Logic', () => {
    let routerEvents$: Subject<any>;
  
    beforeEach(async () => {
      routerEvents$ = new Subject();
  
      await TestBed.configureTestingModule({
        imports: [AppComponent],
        providers: [
          {
            provide: Router,
            useValue: {
              events: routerEvents$.asObservable(),
            },
          },
        ],
      }).compileComponents();
    });
  
    it('should update currentUrl on NavigationEnd', () => {
      const fixture = TestBed.createComponent(AppComponent);
      const app = fixture.componentInstance;
  
      routerEvents$.next(new NavigationEnd(1, '/boat', '/login'));
      expect(app.currentUrl()).toBe('/login');
    });
  });
});
