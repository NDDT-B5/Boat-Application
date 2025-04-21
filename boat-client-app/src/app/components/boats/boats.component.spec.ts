import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BoatsComponent } from './boats.component';
import { BoatService } from '../../core/services/boat.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { BoatsDataSource } from '../../shared/data/boats-datasource';
import { BoatDto } from '../../shared/models/boat.model';
import { SnackbarService } from '../../core/services/snackbar.service';

describe('BoatsComponent', () => {
  let component: BoatsComponent;
  let fixture: ComponentFixture<BoatsComponent>;

  let boatServiceSpy: jasmine.SpyObj<BoatService>;
  let snackbarServiceSpy: jasmine.SpyObj<SnackbarService>;
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
});
