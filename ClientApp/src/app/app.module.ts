import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { JwtModule } from '@auth0/angular-jwt';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AuthService } from './_services/auth.service';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { CostsComponent } from './costs/costs.component';
import { IncomeComponent } from './income/income.component';
import { CostplansComponent } from './costplans/costplans.component';
import { AuthGuard } from './_guards/auth.guard';
import { CostsResolver } from './_resolver/costs.resolver';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      HomeComponent,
      RegisterComponent,
      CostsComponent,
      IncomeComponent,
      CostplansComponent
   ],
   imports: [
      BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      HttpClientModule,
      JwtModule.forRoot({
        config: {
          tokenGetter: tokenGetter,
          allowedDomains: ['localhost:5000'],
          disallowedRoutes: ['localhost:5000/api/auth']
        }
      }),
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'costs', component: CostsComponent, canActivate: [AuthGuard], resolve: {costs: CostsResolver}},
      { path: 'income', component: IncomeComponent, canActivate: [AuthGuard] },
      { path: 'costplans', component: CostplansComponent, canActivate: [AuthGuard] },
    ])
  ],
  providers: [ 
    AuthService, 
    ErrorInterceptorProvider, 
    AuthGuard, 
    CostsResolver 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
