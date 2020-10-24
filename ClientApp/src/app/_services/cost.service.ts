import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Cost } from '../_models/Cost';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { RealCostFromPlan } from '../_models/RealCostFromPlan';

@Injectable({
  providedIn: 'root'
})

export class CostService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getCosts(showAllRows): Observable<Cost[]> {
    return this.http.get<Cost[]>(`${this.baseUrl}cost/costs/${this.authService.decodedToken.nameid}/${showAllRows}`);
  }

  getPlans(): Observable<RealCostFromPlan[]> {
    return this.http.get<RealCostFromPlan[]>(this.baseUrl + 'cost/plansrealize/' + this.authService.decodedToken.nameid);
  }

  addCost(model: any) {
    console.log(this.authService.decodedToken.nameid);
    return this.http.post(this.baseUrl + 'cost/saveCost/' + this.authService.decodedToken.nameid, model);
  }

  deletePlan(id: number) {
    return this.http.delete<RealCostFromPlan[]>(this.baseUrl + 'cost/plandelete/' + this.authService.decodedToken.nameid + '/' + id);
  }
  
  deleteCost(id: number) {
    return this.http.delete<Cost[]>(this.baseUrl + 'cost/costdelete/' + this.authService.decodedToken.nameid + '/' + id);
  }
}
