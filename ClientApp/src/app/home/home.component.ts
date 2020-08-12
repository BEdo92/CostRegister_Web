import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-home', 
  templateUrl: './home.component.html',
})

export class HomeComponent implements OnInit {
  registerMode = true;

  
  constructor() {}

  ngOnInit(): void {
  }

  registerToggle() {
    this.registerMode = true;
  }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }
  
}
