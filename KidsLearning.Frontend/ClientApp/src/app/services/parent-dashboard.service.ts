import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {ParentDashboardDto, AddChildDto, ChildDto, EditChildDto} from '../dtos/parent-dashboard.dto';

@Injectable({
  providedIn: 'root'
})
export class ParentDashboardService {
  private apiUrl = 'http://localhost:5207/api/ParentDashboard';

  constructor(private http: HttpClient) {
  }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('jwt_token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token ?? ''}`
    });
  }

  getDashboardData(): Observable<ParentDashboardDto> {
    return this.http.get<ParentDashboardDto>(this.apiUrl, {headers: this.getAuthHeaders()});
  }

  addChild(childData: AddChildDto): Observable<ChildDto> {
    return this.http.post<ChildDto>(`${this.apiUrl}/add-child`, childData, {headers: this.getAuthHeaders()});
  }

  removeChild(childId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/remove-child/${childId}`, {headers: this.getAuthHeaders()});
  }

  editChild(childId: number, editChildDto: EditChildDto): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/edit-child/${childId}`, editChildDto);
  }

  updateChildAvatar(childId: number, avatarUrl: string): Observable<ChildDto> {
    const editChildDto = {avatarUrl: avatarUrl};
    return this.http.put<ChildDto>(
      `${this.apiUrl}/edit-child/${childId}`,
      editChildDto,
      {headers: this.getAuthHeaders()}
    );
  }
}
