import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BoatDto } from '../models/boat.model'; 
import { environment } from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BoatService {
  private apiUrl = `${environment.apiBaseUrl}/boats`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<BoatDto[]> {
    return this.http.get<BoatDto[]>(this.apiUrl);
  }

  getById(id: string): Observable<BoatDto> {
    return this.http.get<BoatDto>(`${this.apiUrl}/${id}`);
  }

  create(boat: Partial<BoatDto>): Observable<BoatDto> {
    return this.http.post<BoatDto>(this.apiUrl, boat);
  }

  update(id: string, boat: Partial<BoatDto>): Observable<BoatDto> {
    return this.http.put<BoatDto>(`${this.apiUrl}/${id}`, boat);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
