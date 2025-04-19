import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BoatDeleteDialogComponent, BoatDeleteDialogData } from './boat-delete-dialog.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BoatDto } from '../../core/models/boat.model';

describe('BoatDeleteDialogComponent', () => {
  let component: BoatDeleteDialogComponent;
  let fixture: ComponentFixture<BoatDeleteDialogComponent>;
  let mockDialogRef: jasmine.SpyObj<MatDialogRef<BoatDeleteDialogComponent>>;

  const boatMock: BoatDto = {
    id: "6302e53f-f63c-4119-8fc3-52e9b2ebed34",
    name: 'Mock Boat',
    description: 'This is a mock boat'
  };

  beforeEach(async () => {
    mockDialogRef = jasmine.createSpyObj('MatDialogRef', ['close']);

    await TestBed.configureTestingModule({
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: { boat: boatMock } as BoatDeleteDialogData },
        { provide: MatDialogRef, useValue: mockDialogRef }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoatDeleteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should receive boat data via MAT_DIALOG_DATA', () => {
    expect(component.data.boat).toEqual(boatMock);
  });

  it('should close dialog with true when onDelete is called', () => {
    component.onDelete();
    expect(mockDialogRef.close).toHaveBeenCalledWith(true);
  });
});
