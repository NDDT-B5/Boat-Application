import { TestBed } from '@angular/core/testing';
import { SnackbarService } from './snackbar.service';
import { MatSnackBar } from '@angular/material/snack-bar';

describe('SnackbarService', () => {
  let service: SnackbarService;
  let snackBarSpy: jasmine.SpyObj<MatSnackBar>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('MatSnackBar', ['open']);

    TestBed.configureTestingModule({
      providers: [
        SnackbarService,
        { provide: MatSnackBar, useValue: spy }
      ]
    });

    service = TestBed.inject(SnackbarService);
    snackBarSpy = TestBed.inject(MatSnackBar) as jasmine.SpyObj<MatSnackBar>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should show success snackbar', () => {
    const message = 'Success!';
    service.showSuccess(message);

    expect(snackBarSpy.open).toHaveBeenCalledWith(message, 'Close', {
      duration: 3000,
      panelClass: ['success-snackbar']
    });
  });

  it('should show error snackbar', () => {
    const message = 'Something went wrong.';
    service.showError(message);

    expect(snackBarSpy.open).toHaveBeenCalledWith(message, 'Close', {
      duration: 3000,
      panelClass: ['error-snackbar']
    });
  });

  it('should show info snackbar', () => {
    const message = 'Just so you know.';
    service.showInfo(message);

    expect(snackBarSpy.open).toHaveBeenCalledWith(message, 'Close', {
      duration: 3000,
      panelClass: ['info-snackbar']
    });
  });
});
