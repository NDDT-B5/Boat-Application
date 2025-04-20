import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginComponent } from './login.component';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    authServiceSpy = jasmine.createSpyObj('AuthService', ['login', 'setToken']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [LoginComponent, ReactiveFormsModule],
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy },
      ],
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have invalid form when empty', () => {
    expect(component.loginForm.valid).toBeFalse();
  });

  it('should have valid form when filled correctly', () => {
    component.loginForm.setValue({ username: 'admin', password: '1234' });
    expect(component.loginForm.valid).toBeTrue();
  });

  it('should have invalid form when username or password is too short', () => {
    component.loginForm.setValue({ username: 'a', password: 'validPass' });
    expect(component.loginForm.valid).toBeFalse();
  
    component.loginForm.setValue({ username: 'validUser', password: 'b' });
    expect(component.loginForm.valid).toBeFalse();
  
    component.loginForm.setValue({ username: 'a', password: 'b' });
    expect(component.loginForm.valid).toBeFalse();
  });

  it('should call AuthService.login and navigate on successful login', () => {
    const token = 'mock-token';
    authServiceSpy.login.and.returnValue(of({ jwtToken: token, role: "User" }));
    component.loginForm.setValue({ username: 'admin', password: '1234' });

    component.login();

    expect(authServiceSpy.login).toHaveBeenCalledWith('admin', '1234');
    expect(authServiceSpy.setToken).toHaveBeenCalledWith(token);
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/boats']);
  });

  it('should show error on failed login', () => {
    authServiceSpy.login.and.returnValue(throwError(() => ({ status: 401 })));
    component.loginForm.setValue({ username: 'admin', password: 'wrongpass' });

    component.login();

    expect(authServiceSpy.login).toHaveBeenCalled();
    expect(component.errorMessage).toBe('Invalid username or password');
  });
});
