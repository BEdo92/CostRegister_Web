import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { SettingsService } from '../_services/settings.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  constructor(private authService: AuthService, private settingsService: SettingsService) { }

  ngOnInit() {
  }

  deleteAccount() {
    if (confirm('Valoban szeretne veglegesen torolni a fiokjat?')) {
      this.settingsService.deleteUserAccount().subscribe(() => {
        alert('Torles sikeres!');
        this.authService.logOut();
      }, error => {
        alert(error);
      });
    }
  }

}
