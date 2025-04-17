import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BoatItem } from '../models/boat.model'; 
import { environment } from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BoatService {
  private apiUrl = `${environment.apiBaseUrl}/boats`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<BoatItem[]> {
    return this.http.get<BoatItem[]>(this.apiUrl);
  }

  getById(id: number): Observable<BoatItem> {
    return this.http.get<BoatItem>(`${this.apiUrl}/${id}`);
  }

  create(boat: Partial<BoatItem>): Observable<BoatItem> {
    return this.http.post<BoatItem>(this.apiUrl, boat);
  }

  update(id: string, boat: Partial<BoatItem>): Observable<BoatItem> {
    return this.http.put<BoatItem>(`${this.apiUrl}/${id}`, boat);
  }

  delete(id: string): Observable<void> {
    console.log("asd")
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
