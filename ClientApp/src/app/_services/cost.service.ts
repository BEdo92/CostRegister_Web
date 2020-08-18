import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Cost } from '../_models/Cost';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})

export class CostService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getCosts(): Observable<Cost[]> {
    return this.http.get<Cost[]>(this.baseUrl + 'usersdata/costs/' + this.authService.decodedToken.nameid);
  }

  addCost(model: any) {
    console.log(this.authService.decodedToken.nameid);
    return this.http.post(this.baseUrl + 'usersdata/saveCost/' + this.authService.decodedToken.nameid, model);
  }

}
