import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Setting } from '../_models/Setting';
import { AuthService } from '../_services/auth.service';
import { SettingsService } from '../_services/settings.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  settingsForm: FormGroup;
  settings: Setting;

  constructor(private authService: AuthService, 
    private settingsService: SettingsService,
    private fb: FormBuilder) { }

  ngOnInit() {
    this.createSettingsForm();
  }

  createSettingsForm() {
     this.settingsForm = this.fb.group({
      includePlansToBalance: ['', '']
    });
  }

   saveSettings() {
  //   if (this.settingsForm.valid) {
  //       this.settings = Object.assign({}, this.settingsForm.value);
  //       this.settingsService.saveSettings(this.settings).subscribe(next => {
  //         console.log('Data saved successfully!');
  //         alert('Mentes sikeres!');
  //       }, error => {
  //         console.log('Failed to save data!');
  //         alert('Adatmentes sikertelen!');
  //       });
  //   }
  //   else {
  //     alert('Az urlap nincs megfeleloen kitoltve!');
  //   }
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
