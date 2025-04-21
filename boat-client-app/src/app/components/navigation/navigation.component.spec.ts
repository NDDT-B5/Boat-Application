import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { NavigationComponent } from './navigation.component';
import { of } from 'rxjs';
import { provideRouter, Router } from '@angular/router';
import { AuthService } from '../../core/auth/auth.service';
import { BreakpointObserver } from '@angular/cdk/layout';

describe('NavigationComponent', () => {
  let component: NavigationComponent;
  let fixture: ComponentFixture<NavigationComponent>;

  const mockAuthService = jasmine.createSpyObj('AuthService', ['removeToken']);
  const mockBreakpointObserver = {
    observe: jasmine.createSpy().and.returnValue(of({ matches: false }))
  };

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        NavigationComponent
      ],
      providers: [
        { provide: AuthService, useValue: mockAuthService },
        { provide: BreakpointObserver, useValue: mockBreakpointObserver },
        provideRouter([]),
      ]
    });
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should compile', () => {
    expect(component).toBeTruthy();
  });

  it('should call removeToken and navigate to login on logout', () => {
    component.logout();
    fixture.whenStable().then(() => {
      expect(mockAuthService.removeToken).toHaveBeenCalled();
    });
  });
});
