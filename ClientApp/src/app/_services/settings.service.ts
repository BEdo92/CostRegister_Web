import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root'
  })
export class SettingsService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient, private authService: AuthService) { }

    deleteUserAccount() {
        return this.http.delete(this.baseUrl + 'usersdata/delete/' + this.authService.decodedToken.nameid);
    }
}
