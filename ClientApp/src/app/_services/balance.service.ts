import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})

export class BalanceService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getBalance(): Observable<number> {
    return this.http.get<number>(this.baseUrl + 'usersdata/balance/' + this.authService.decodedToken.nameid);
  }
}



