import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LearningService {
  private apiUrl = `${environment.apiUrl}/api/Learning`;

  constructor(private http: HttpClient) { }

  completeTask(childId: number, taskId: number): Observable<any> {
    const completionDto = { childId, taskId };
    return this.http.post(`${this.apiUrl}/complete-task`, completionDto);
  }
}