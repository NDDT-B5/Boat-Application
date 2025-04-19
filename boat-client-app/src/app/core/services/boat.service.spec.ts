import { TestBed } from '@angular/core/testing';
import { BoatService } from './boat.service';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { environment } from '../../../environments/environment';
import { BoatDto } from '../models/boat.model';
import { provideHttpClient } from '@angular/common/http';

describe('BoatService', () => {
  let service: BoatService;
  let httpMock: HttpTestingController;
  const apiUrl = `${environment.apiBaseUrl}/boats`;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BoatService, provideHttpClient(), provideHttpClientTesting()]
    });
    service = TestBed.inject(BoatService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all boats', () => {
    const mockBoats: BoatDto[] = [
      { id: "6302e53f-f63c-4119-8fc3-52e9b2ebed34", name: 'Boat A', description: 'Test A' },
      { id: "6302e53f-f63c-4119-8fc3-52e9b2ebed39", name: 'Boat B', description: 'Test B' }
    ];

    service.getAll().subscribe(boats => {
      expect(boats).toEqual(mockBoats);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockBoats);
  });

  it('should get boat by ID', () => {
    const mockBoat: BoatDto = { id: "6302e53f-f63c-4119-8fc3-52e9b2ebed34", name: 'Boat A', description: 'Test A' };
    const id = '6302e53f-f63c-4119-8fc3-52e9b2ebed34';

    service.getById(id).subscribe(boat => {
      expect(boat).toEqual(mockBoat);
    });

    const req = httpMock.expectOne(`${apiUrl}/${id}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockBoat);
  });

  it('should create a new boat', () => {
    const newBoat: Partial<BoatDto> = { name: 'New Boat', description: 'Desc' };
    const createdBoat: BoatDto = { id: "6302e53f-f63c-4119-8fc3-52e9b2ebed34", ...newBoat } as BoatDto;

    service.create(newBoat).subscribe(boat => {
      expect(boat).toEqual(createdBoat);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newBoat);
    req.flush(createdBoat);
  });

  it('should update a boat', () => {
    const updatedBoat: Partial<BoatDto> = { name: 'Updated Boat' };
    const id = '26302e53f-f63c-4119-8fc3-52e9b2ebed34';
    const response: BoatDto = { id: id, name: 'Updated Boat', description: 'Updated Desc' };

    service.update(id, updatedBoat).subscribe(boat => {
      expect(boat).toEqual(response);
    });

    const req = httpMock.expectOne(`${apiUrl}/${id}`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updatedBoat);
    req.flush(response);
  });

  it('should delete a boat', () => {
    const id = '6302e53f-f63c-4119-8fc3-52e9b2ebed34';

    service.delete(id).subscribe(response => {
      expect(response).toBeNull();
    });

    const req = httpMock.expectOne(`${apiUrl}/${id}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
});
