import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RewardService {
    private apiUrl = environment.apiUrl + '/Learning';

    constructor(private http: HttpClient){}

    rewardChild(childId: number, taskId: number) : Observable<any> {
      const body = { childId, taskId};
      return this.http.post(`${this.apiUrl}/complete-task`, body);
    }
}
