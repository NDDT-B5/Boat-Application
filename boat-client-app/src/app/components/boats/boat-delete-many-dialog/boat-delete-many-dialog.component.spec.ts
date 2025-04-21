import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BoatDeleteManyDialogComponent, BoatDeleteDialogData } from './boat-delete-many-dialog.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BoatDto } from '../../../shared/models/boat.model';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';

describe('BoatDeleteManyDialogComponent', () => {
  let component: BoatDeleteManyDialogComponent;
  let fixture: ComponentFixture<BoatDeleteManyDialogComponent>;
  let mockDialogRef: jasmine.SpyObj<MatDialogRef<BoatDeleteManyDialogComponent>>;

  const boatMock1: BoatDto = {
    id: "6302e53f-f63c-4119-8fc3-52e9b2ebed34",
    name: 'Boat A',
    description: 'Mock Boat A'
  };

  const boatMock2: BoatDto = {
    id: "6302e53f-f63c-4119-8fc3-52e9b2ebed39",
    name: 'Boat B',
    description: 'Mock Boat B'
  };

  const boatDeleteDialogData: BoatDeleteDialogData = {
    boats: [boatMock1, boatMock2]
  };

  beforeEach(async () => {
    mockDialogRef = jasmine.createSpyObj('MatDialogRef', ['close']);

    await TestBed.configureTestingModule({
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: boatDeleteDialogData },
        { provide: MatDialogRef, useValue: mockDialogRef }
      ],
      imports: [
        CommonModule,
        MatButtonModule,
        MatIconModule,
        MatListModule
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(BoatDeleteManyDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the dialog component', () => {
    expect(component).toBeTruthy();
  });

  it('should receive boat data via MAT_DIALOG_DATA', () => {
    expect(component.data.boats).toEqual(boatDeleteDialogData.boats);
  });

  it('should display all boat names and IDs in the dialog', () => {
    const boatName1 = fixture.nativeElement.querySelector('.mat-mdc-list-item-title');
    const boatId1 = fixture.nativeElement.querySelector('.mat-mdc-list-item-line');

    expect(boatName1.textContent).toContain(boatMock1.name);
    expect(boatId1.textContent).toContain(boatMock1.id);

    const boatName2 = fixture.nativeElement.querySelectorAll('.mat-mdc-list-item-title')[1];
    const boatId2 = fixture.nativeElement.querySelectorAll('.mat-mdc-list-item-line')[1];

    expect(boatName2.textContent).toContain(boatMock2.name);
    expect(boatId2.textContent).toContain(boatMock2.id);
  });

  it('should close the dialog with true when onDelete is called', () => {
    component.onDelete();
    expect(mockDialogRef.close).toHaveBeenCalledWith(true);
  });

  it('should close the dialog with false when onCancel is called', () => {
    component.onCancel();
    expect(mockDialogRef.close).toHaveBeenCalledWith(false);
  });
});
