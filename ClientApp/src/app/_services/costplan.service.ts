import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { CostPlan } from '../_models/CostPlan';

@Injectable({
  providedIn: 'root'
})
export class CostplanService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getCostPlans(): Observable<CostPlan[]> {
    return this.http.get<CostPlan[]>(this.baseUrl + 'costplan/costplans/' + this.authService.decodedToken.nameid);
  }

  addCostPlans(model: any) {
    console.log(this.authService.decodedToken.nameid);
    return this.http.post(this.baseUrl + 'costplan/saveCostPlan/' + this.authService.decodedToken.nameid, model);
  }
}
