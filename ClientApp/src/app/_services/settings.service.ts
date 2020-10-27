import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
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

    // TODO: implement user settings
    // saveSettings(settings: Setting) {
    //     return this.http.post(`${this.baseUrl}settings/add/${this.authService.decodedToken.nameid}`, settings);
    // }
}
