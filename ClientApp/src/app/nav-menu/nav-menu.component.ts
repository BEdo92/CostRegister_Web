import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BalanceService } from '../_services/balance.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent implements OnInit {
  isExpanded = false;
  model: any = {};

  constructor(public authService: AuthService, private router: Router, private balanceService: BalanceService) { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ngOnInit(): void {
  }


  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in successfully!');
    }, error => {
      console.log('Failed to log in!');
      alert('Bejelentkezes sikertelen!');
    }, () => {
      this.router.navigate(['/costs']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logOut() {
    //localStorage.removeItem('token');
    this.authService.logOut();
    console.log('Logged out!');
    this.router.navigate(['/']);
  }
}
