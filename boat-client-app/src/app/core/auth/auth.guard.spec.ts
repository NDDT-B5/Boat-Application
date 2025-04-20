import { TestBed } from '@angular/core/testing';
import { CanActivateFn, Router } from '@angular/router';
import { authGuard } from './auth.guard';
import { AuthService } from './auth.service';

describe('authGuard', () => {
  let isLoggedInMock = false;
  const navigateSpy = jasmine.createSpy('navigate');

  const mockAuthService = {
    isLoggedIn: () => isLoggedInMock,
  };

  const mockRouter = {
    navigate: navigateSpy
  };

  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => authGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        { provide: AuthService, useValue: mockAuthService },
        { provide: Router, useValue: mockRouter }
      ]
    });

    navigateSpy.calls.reset();
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });

  it('should allow navigation if the user is logged in', () => {
    isLoggedInMock = true;

    const result = executeGuard({} as any, {} as any);
    expect(result).toBeTrue();
    expect(navigateSpy).not.toHaveBeenCalled();
  });

  it('should block navigation and redirect to login if the user is not logged in', () => {
    isLoggedInMock = false;

    const result = executeGuard({} as any, {} as any);
    expect(result).toBeFalse();
    expect(navigateSpy).toHaveBeenCalledWith(['/login']);
  });
});
