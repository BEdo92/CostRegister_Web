import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { Income } from '../_models/Income';

@Injectable({
  providedIn: 'root'
})
export class IncomeService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getIncome(): Observable<Income[]> {
    return this.http.get<Income[]>(this.baseUrl + 'usersdata/income/' + this.authService.decodedToken.nameid);
  }

  addIncome(model: any) {
    console.log(this.authService.decodedToken.nameid);
    return this.http.post(this.baseUrl + 'usersdata/saveIncome/' + this.authService.decodedToken.nameid, model);
  }

}
