import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ParentDashboardDto, AddChildDto } from '../dtos/parent-dashboard.dto';

@Injectable({
  providedIn: 'root'
})
export class ParentDashboardService {
  private apiUrl = 'http://localhost:5207/api/ParentDashboard';

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('jwt_token'); 
    return new HttpHeaders({
      'Authorization': `Bearer ${token ?? ''}`
    });
  }

  getDashboardData(): Observable<ParentDashboardDto> {
    return this.http.get<ParentDashboardDto>(this.apiUrl, { headers: this.getAuthHeaders() });
  }

  addChild(childData: AddChildDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/add-child`, childData, { headers: this.getAuthHeaders() });
  }
}
