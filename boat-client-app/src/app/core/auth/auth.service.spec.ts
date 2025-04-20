import { TestBed } from '@angular/core/testing';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;
  const apiUrl = `${environment.apiBaseUrl}/auth`;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthService, provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should login and return token and role', () => {
    const mockResponse = { jwtToken: 'test-token', role: 'USER' };

    service.login('testuser', 'testpass').subscribe((res) => {
      expect(res).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${apiUrl}/login`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ username: 'testuser', password: 'testpass' });
    req.flush(mockResponse);
  });

  it('should throw an error if login fails', () => {
    const errorResponse = { status: 401, statusText: 'Unauthorized' };

    service.login('invalid', 'user').subscribe({
      next: () => fail('should have failed with 401 error'),
      error: (error) => {
        expect(error).toBeTruthy();
        expect(error.message).toContain('Unauthorized');
      }
    });

    const req = httpMock.expectOne(`${apiUrl}/login`);
    req.flush('Invalid login', errorResponse);
  });

  it('should get/set/remove token correctly from localStorage', () => {
    expect(service.getToken()).toBeNull();

    service.setToken('abc123');
    expect(localStorage.getItem('jwt_token')).toBe('abc123');
    expect(service.getToken()).toBe('abc123');

    service.removeToken();
    expect(localStorage.getItem('jwt_token')).toBeNull();
  });

  it('should return true from isLoggedIn when token exists and should return false from isLoggedIn when token is missing', () => {
    service.setToken('valid-token');
    expect(service.isLoggedIn()).toBeTrue();

    service.removeToken();
    expect(service.isLoggedIn()).toBeFalse();
  });
});
