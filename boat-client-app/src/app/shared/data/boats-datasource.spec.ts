import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { BoatsDataSource } from './boats-datasource';
import { BoatService } from '../../core/services/boat.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

describe('BoatsDataSource', () => {
  let dataSource: BoatsDataSource;
  let boatServiceSpy: jasmine.SpyObj<BoatService>;
  let paginator: MatPaginator;
  let sort: MatSort;

  // Mock data
  const mockBoats = [
    { id: '1', name: 'Boat 1', description: 'Description 1' },
    { id: '2', name: 'Boat 2', description: 'Description 2' },
    { id: '3', name: 'Boat 3', description: 'Description 3' }
  ];

  beforeEach(() => {
    // Create a mock BoatService
    const spy = jasmine.createSpyObj('BoatService', ['getAll']);

    TestBed.configureTestingModule({
      providers: [
        BoatsDataSource,
        { provide: BoatService, useValue: spy },
        MatPaginator,
        MatSort
      ]
    });

    dataSource = TestBed.inject(BoatsDataSource);
    boatServiceSpy = TestBed.inject(BoatService) as jasmine.SpyObj<BoatService>;
    paginator = TestBed.inject(MatPaginator);
    sort = TestBed.inject(MatSort);

    // Mock the getAll method to return an observable
    boatServiceSpy.getAll.and.returnValue(of(mockBoats));
  });

  it('should be created', () => {
    expect(dataSource).toBeTruthy();
  });

  it('should load boats on init', () => {
    // Manually call the loadBoats method to ensure it's triggered
    dataSource.loadBoats();
    dataSource.dataLoaded$.subscribe((boats) => {
      expect(boats.length).toBe(3);
      expect(boats).toEqual(mockBoats);
    });
  });

  // Add more tests here as necessary
});
