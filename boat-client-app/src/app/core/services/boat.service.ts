import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BoatItem } from '../models/boat.model'; 
import { environment } from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BoatService {

  private baseUrl = `${environment.apiBaseUrl}/boats`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<BoatItem[]> {
    return this.http.get<BoatItem[]>(this.baseUrl);
  }

  getById(id: number): Observable<BoatItem> {
    return this.http.get<BoatItem>(`${this.baseUrl}/${id}`);
  }

  create(boat: Partial<BoatItem>): Observable<BoatItem> {
    return this.http.post<BoatItem>(this.baseUrl, boat);
  }

  update(id: string, boat: Partial<BoatItem>): Observable<BoatItem> {
    return this.http.put<BoatItem>(`${this.baseUrl}/${id}`, boat);
  }

  delete(id: string): Observable<void> {
    console.log("asd")
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
