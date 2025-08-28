import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {LearningTask} from '../models/learning-task';
import {Question} from '../models/question';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  private apiUrl = 'http://localhost:5207/api/Tasks';

  constructor(private http: HttpClient) {
  }

  getTasksBySubject(subject: string): Observable<LearningTask[]> {
    return this.http.get<LearningTask[]>(`${this.apiUrl}/by-subject/${subject}`)
  }

  getTaskById(taskId: number): Observable<LearningTask> {
    return this.http.get<LearningTask>(`${this.apiUrl}/${taskId}`);
  }

  getAllQuestions(): Observable<Question[]> {
    return this.http.get<Question[]>(`${this.apiUrl}/all-questions`);
  }
}
