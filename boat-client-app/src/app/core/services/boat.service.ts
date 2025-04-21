import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { BoatDto } from '../../shared/models/boat.model';


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

  deleteMany(ids: string[]): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete-many`,{ body: { ids }});
  }
}
