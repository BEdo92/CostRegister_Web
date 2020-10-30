import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BalanceSetting } from '../_models/BalanceSetting';
//import { Setting } from '../_models/Setting';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root'
  })
export class SettingsService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient, private authService: AuthService) { }

    deleteUserAccount() {
        return this.http.delete(`${this.baseUrl}usersdata/delete/${this.authService.decodedToken.nameid}`);
    }

    // TODO: it is not binding correctly. (endpoint worked fine from postman.)
    saveSettings(model: any) {
        console.log(model);
        return this.http.post(`${this.baseUrl}settings/saveBalanceSetting/${this.authService.decodedToken.nameid}`, model);
    }
}
