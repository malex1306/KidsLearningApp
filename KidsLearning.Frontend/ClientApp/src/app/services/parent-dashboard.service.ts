import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable, tap} from 'rxjs';
import {ParentDashboardDto, AddChildDto, ChildDto, EditChildDto} from '../dtos/parent-dashboard.dto';

@Injectable({
  providedIn: 'root'
})
export class ParentDashboardService {
  private apiUrl = 'http://localhost:5207/api/ParentDashboard';


  private childrenSubject = new BehaviorSubject<ChildDto[] | null>(null);
  public children$ = this.childrenSubject.asObservable();

  constructor(private http: HttpClient) {
  }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('jwt_token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token ?? ''}`
    });
  }

  getDashboardData(): Observable<ParentDashboardDto> {
    return this.http.get<ParentDashboardDto>(this.apiUrl, {headers: this.getAuthHeaders()}).pipe(
      tap(data => {
        this.childrenSubject.next(data.children);
      })
    );
  }

  addChild(childData: AddChildDto): Observable<ChildDto> {
    return this.http.post<ChildDto>(`${this.apiUrl}/add-child`, childData, {headers: this.getAuthHeaders()}).pipe(
      tap(newChild => {
        const currentChildren = this.childrenSubject.getValue() || [];
        this.childrenSubject.next([...currentChildren, newChild]);
      })
    );
  }

  removeChild(childId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/remove-child/${childId}`, {headers: this.getAuthHeaders()}).pipe(
      tap(() => {
        const currentChildren = this.childrenSubject.getValue() || [];
        const updatedChildren = currentChildren.filter(child => child.childId !== childId);
        this.childrenSubject.next(updatedChildren);
      })
    );
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
    ).pipe(
      tap(updatedChild => {
        const currentChildren = this.childrenSubject.getValue() || [];
        const updatedChildren = currentChildren.map(child =>
          child.childId === childId ? {...child, avatarUrl: updatedChild.avatarUrl} : child
        );
        this.childrenSubject.next(updatedChildren);
      })
    );
  }
}
