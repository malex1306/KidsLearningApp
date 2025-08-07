import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LearningTask } from '../models/learning-task';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  private apiUrl = 'http://localhost:5207/api/tasks';

  constructor(private http: HttpClient){}

  getTasksBySubject(subject: string) : Observable<LearningTask[]> {
    return this.http.get<LearningTask[]>(`${this.apiUrl}/${subject}`)
  }
}
