import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { BoatDetailComponent } from './boat-detail.component';
import { Router, ActivatedRoute, NavigationExtras } from '@angular/router';
import { of, throwError } from 'rxjs';
import { BoatService } from '../../../core/services/boat.service';
import { BoatsDataSource } from '../../../shared/data/boats-datasource';
import { BoatDto } from '../../../shared/models/boat.model';
import { SnackbarService } from '../../../core/services/snackbar.service';

describe('BoatDetailComponent', () => {
  let component: BoatDetailComponent;
  let fixture: ComponentFixture<BoatDetailComponent>;

  let routerSpy: jasmine.SpyObj<Router>;
  let routeStub: Partial<ActivatedRoute>;
  let boatServiceSpy: jasmine.SpyObj<BoatService>;
  let snackbarSpy: jasmine.SpyObj<SnackbarService>;
  let dataSourceSpy: jasmine.SpyObj<BoatsDataSource>;

  const mockBoat: BoatDto = {
    id: '1',
    name: 'Test Boat',
    description: 'This is a test boat',
  };

  beforeEach(async () => {
    routerSpy = jasmine.createSpyObj('Router', ['navigate', 'getCurrentNavigation']);
    routeStub = {
      snapshot: {
        paramMap: new Map([['id', '1']])
      } as any
    };
    boatServiceSpy = jasmine.createSpyObj('BoatService', ['getById', 'delete']);
    snackbarSpy = jasmine.createSpyObj('SnackbarService', ['showError', 'showSuccess']);
    dataSourceSpy = jasmine.createSpyObj('BoatsDataSource', ['deleteBoat']);

    await TestBed.configureTestingModule({
      imports: [BoatDetailComponent],
      providers: [
        { provide: Router, useValue: routerSpy },
        { provide: ActivatedRoute, useValue: routeStub },
        { provide: BoatService, useValue: boatServiceSpy },
        { provide: SnackbarService, useValue: snackbarSpy },
        { provide: BoatsDataSource, useValue: dataSourceSpy }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoatDetailComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load boat from navigation state if available', () => {
    routerSpy.getCurrentNavigation.and.returnValue({
      extras: {
        state: { boat: mockBoat }
      }
    } as any);

    component.ngOnInit();

    expect(component.boat).toEqual(mockBoat);
  });

  it('should fetch boat by ID if not in navigation state', () => {
    routerSpy.getCurrentNavigation.and.returnValue(null);
    boatServiceSpy.getById.and.returnValue(of(mockBoat));

    component.ngOnInit();

    expect(boatServiceSpy.getById).toHaveBeenCalledWith('1');
    expect(component.boat).toEqual(mockBoat);
  });

  it('should show error if boat ID is missing', () => {
    (routeStub.snapshot as any).paramMap.get = () => null;
    component.ngOnInit();

    expect(snackbarSpy.showError).toHaveBeenCalledWith('No boat data or ID provided.');
  });

  it('should handle boat fetch error', () => {
    routerSpy.getCurrentNavigation.and.returnValue(null);
    boatServiceSpy.getById.and.returnValue(throwError(() => 'error'));

    component.ngOnInit();

    expect(snackbarSpy.showError).toHaveBeenCalledWith('Boat not found or failed to fetch.');
  });

  it('should call services on deleteBoat', fakeAsync(() => {
    component.boat = mockBoat;
    boatServiceSpy.delete.and.returnValue(of(undefined));

    component.deleteBoat();
    tick();

    expect(boatServiceSpy.delete).toHaveBeenCalledWith('1');
    expect(dataSourceSpy.deleteBoat).toHaveBeenCalledWith('1');
    expect(snackbarSpy.showSuccess).toHaveBeenCalled();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/boats']);

  }));

  it('should show error if delete fails', fakeAsync(() => {
    component.boat = mockBoat;
    boatServiceSpy.delete.and.returnValue(throwError(() => 'error'));

    component.deleteBoat();
    tick();

    expect(snackbarSpy.showError).toHaveBeenCalledWith('Failed to delete boat!');
  }));

  it('should navigate back when goBack is called', () => {
    component.goBack();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/boats']);
  });
});
