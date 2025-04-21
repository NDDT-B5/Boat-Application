import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { BoatsDataSource } from './boats-datasource';
import { BoatService } from '../../core/services/boat.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { BoatDto } from '../models/boat.model';

describe('BoatsDataSource', () => {
  let dataSource: BoatsDataSource;
  let boatServiceSpy: jasmine.SpyObj<BoatService>;

  const mockBoats: BoatDto[] = [
    { id: '1', name: 'Alpha', description: 'Desc A' },
    { id: '2', name: 'Bravo', description: 'Desc B' },
    { id: '3', name: 'Charlie', description: 'Desc C' },
  ];

  beforeEach(() => {
    boatServiceSpy = jasmine.createSpyObj('BoatService', ['getAll']);
    boatServiceSpy.getAll.and.returnValue(of(mockBoats));
    dataSource = new BoatsDataSource(boatServiceSpy);
  });

  it('should be created', () => {
    expect(dataSource).toBeTruthy();
  });

  it('should create and load boats', (done) => {
    dataSource.dataLoaded$.subscribe(data => {
      expect(data).toEqual(mockBoats);
      done();
    });
  });

  it('should add a new boat', () => {
    const newBoat: BoatDto = { id: '4', name: 'Delta', description: 'Desc D' };
    dataSource.addBoat(newBoat);

    expect(dataSource.dataLoaded$.value).toContain(newBoat);
    expect(dataSource.dataLoaded$.value.length).toBe(4);
  });

  it('should update an existing boat', () => {
    const updated: BoatDto = { id: '2', name: 'Bravo X', description: 'Updated Desc' };
    dataSource.updateBoat(updated);

    const found = dataSource.dataLoaded$.value.find(b => b.id === '2');
    expect(found?.name).toBe('Bravo X');
    expect(found?.description).toBe('Updated Desc');
  });

  it('should delete a boat by ID', () => {
    dataSource.deleteBoat('1');

    expect(dataSource.dataLoaded$.value.length).toBe(2);
    expect(dataSource.dataLoaded$.value.find(b => b.id === '1')).toBeUndefined();
  });

  it('should delete multiple boats by ID', () => {
    dataSource.deleteMany(['1', '2']);

    expect(dataSource.dataLoaded$.value.length).toBe(1);
    expect(dataSource.dataLoaded$.value[0].id).toBe('3');
  });  
});
