import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private apiUrl = environment.WEBSITE_URL;

  constructor(private http: HttpClient) {}

  getAll(apiName: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${apiName}`);
  }

  getById(apiName: string, id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${apiName}/${id}`);
  }

  add(apiName: string, payload: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/${apiName}`, payload);
  }

  update(apiName: string, payload: any, id: number): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${apiName}/${id}`, payload);
  }

  delete(apiName: string, id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${apiName}/${id}`);
  }
}
