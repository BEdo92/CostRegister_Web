import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User } from '../_models/User';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  registerForm: FormGroup;
  newUser: User;
  @Output() cancelRegister = new EventEmitter();

  constructor(private authService: AuthService, private fb: FormBuilder) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      userName: ['Felhasznalonev', Validators.required],
      password: ['Jelszo', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['Jelszo ismet', Validators.required],
    }, {validator: this.passwordMatchValidator});
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch' : true};
  }

  register() {
    if (this.registerForm.valid) {
      this.newUser = Object.assign({}, this.registerForm.value);
      this.authService.register(this.newUser).subscribe(next => {
        console.log('Data saved successfully!');
        alert('Mentes sikeres!');
      }, error => {
        console.log('Failed to save data!');
        alert('Adatmentes sikertelen!');
        return;
      });

      console.log(this.newUser);
    }
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }

}
