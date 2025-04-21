import { ComponentFixture, TestBed, fakeAsync, flush, tick } from '@angular/core/testing';
import { BoatsComponent } from './boats.component';
import { BoatService } from '../../core/services/boat.service';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { of, Subject } from 'rxjs';
import { BoatsDataSource } from '../../shared/data/boats-datasource';
import { BoatDto } from '../../shared/models/boat.model';
import { MatDialogHarness } from '@angular/material/dialog/testing';
import { SnackbarService } from '../../core/services/snackbar.service';

describe('BoatsComponent', () => {
  let component: BoatsComponent;
  let fixture: ComponentFixture<BoatsComponent>;

  let boatServiceSpy: jasmine.SpyObj<BoatService>;
  let snackbarServiceSpy: jasmine.SpyObj<SnackbarService>;
  let dialogSpy: jasmine.SpyObj<MatDialog>;
  let routerSpy: jasmine.SpyObj<Router>;


  const mockBoat: BoatDto = { id: "1", name: 'Titanic', description: 'Big boat' };

  beforeEach(async () => {
    boatServiceSpy = jasmine.createSpyObj('BoatService', ['create', 'update', 'delete', 'deleteMany', 'getAll']);
    boatServiceSpy.getAll.and.returnValue(of([mockBoat]));
    snackbarServiceSpy = jasmine.createSpyObj('SnackbarService', ['showSuccess', 'showError']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);


    await TestBed.configureTestingModule({
      imports: [ BoatsComponent ],
      providers: [
        { provide: BoatService, useValue: boatServiceSpy },
        { provide: SnackbarService, useValue: snackbarServiceSpy },
        { provide: Router, useValue: routerSpy },
        { provide: BoatsDataSource, useValue: new BoatsDataSource(boatServiceSpy) }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(BoatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should add a boat after dialog returns data', async () => {
    boatServiceSpy.create.and.returnValue(of(mockBoat));
    spyOn(component.dataSource, 'addBoat');

    component.onCreate();


    tick();

    expect(boatServiceSpy.create).toHaveBeenCalledWith(mockBoat);
    expect(component.dataSource.addBoat).toHaveBeenCalledWith(mockBoat);
    expect(snackbarServiceSpy.showSuccess).toHaveBeenCalledWith(jasmine.stringMatching(/added successfully/));
    flush();
  });

 
});
