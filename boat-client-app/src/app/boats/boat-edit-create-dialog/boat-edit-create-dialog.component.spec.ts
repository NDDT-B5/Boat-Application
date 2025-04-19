import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BoatEditCreateDialogComponent } from './boat-edit-create-dialog.component';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { BoatDto } from '../../core/models/boat.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

describe('BoatEditCreateDialogComponent', () => {
  let component: BoatEditCreateDialogComponent;
  let fixture: ComponentFixture<BoatEditCreateDialogComponent>;
  let mockDialogRef: jasmine.SpyObj<MatDialogRef<BoatEditCreateDialogComponent>>;

  const boatMock: BoatDto = {
    id: "6302e53f-f63c-4119-8fc3-52e9b2ebed34",
    name: 'Mock Boat',
    description: 'This is a mock boat'
  };

  describe('When mode is "edit"', () => {
    beforeEach(async () => {
      mockDialogRef = jasmine.createSpyObj('MatDialogRef', ['close']);
  
      await TestBed.configureTestingModule({
        imports: [ReactiveFormsModule, MatDialogModule, FormsModule],
        providers: [
          { provide: MAT_DIALOG_DATA, useValue: { mode: 'edit', boat: boatMock } },
          { provide: MatDialogRef, useValue: mockDialogRef }
        ]
      })
      .compileComponents();
  
      fixture = TestBed.createComponent(BoatEditCreateDialogComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });
  
    it('should create', () => {
      expect(component).toBeTruthy();
    });
  
    it('should initialize the form with boat data', () => {
      expect(component.form.get('name')?.value).toBe(boatMock.name);
      expect(component.form.get('description')?.value).toBe(boatMock.description);
      expect(component.form.get('id')?.value).toBe(boatMock.id);
    });
  
    it('should make the name and description fields required', () => {
      const nameControl = component.form.get('name');
      const descriptionControl = component.form.get('description');
      
      nameControl?.setValue('');
      descriptionControl?.setValue('');
      
      expect(nameControl?.valid).toBeFalse();
      expect(descriptionControl?.valid).toBeFalse();
    });
  
    it('should submit the form with valid data and without ID', () => {
      const formValue = { name: 'Boat 2', description: 'Description of Boat 2', id: '6302e53f-f63c-4119-8fc3-52e9b2ebed34' };
      const expectedSubmission = { name: 'Boat 2', description: 'Description of Boat 2' };
      component.form.setValue(formValue);
  
      component.onSubmit();
  
      expect(mockDialogRef.close).toHaveBeenCalledWith(expectedSubmission);
    });
  
    it('should not submit the form if invalid', () => {
      const formValue = { name: '', description: '', id: '6302e53f-f63c-4119-8fc3-52e9b2ebed34' };
      component.form.setValue(formValue);
  
      component.onSubmit();
  
      expect(mockDialogRef.close).not.toHaveBeenCalled();
    });
  
    it('should handle "edit" mode and prefill data', () => {
      expect(component.isEdit).toBeTrue();
      expect(component.form.get('name')?.value).toBe(boatMock.name);
    });
  });


  describe('When mode is "add"', () => {
    beforeEach(async () => {
      mockDialogRef = jasmine.createSpyObj('MatDialogRef', ['close']);

      await TestBed.configureTestingModule({
        imports: [ReactiveFormsModule, MatDialogModule, FormsModule],
        providers: [
          { provide: MAT_DIALOG_DATA, useValue: { mode: 'add', boat: null } },
          { provide: MatDialogRef, useValue: mockDialogRef }
        ]
      }).compileComponents();

      fixture = TestBed.createComponent(BoatEditCreateDialogComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('should handle "add" mode and initialize form with empty values', () => {
      expect(component.isEdit).toBeFalse();
      expect(component.form.get('name')?.value).toBe('');
      expect(component.form.get('description')?.value).toBe('');
      expect(component.form.get('id')?.value).toBe('');
    });
  });
});
