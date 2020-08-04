import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Cost } from '../_models/Cost';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class CostService {
  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  getCosts(id): Observable<Cost[]> {
    return this.http.get<Cost[]>(this.baseUrl + 'usersdata/costs/' + id);
  }
}
