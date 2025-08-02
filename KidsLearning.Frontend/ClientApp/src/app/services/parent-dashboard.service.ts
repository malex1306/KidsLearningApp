import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ParentDashboardService {
  private apiUrl = 'http://localhost:5207/api/ParentDashboard/';

  constructor(private http: HttpClient) {}

  getDashboardData() : Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
