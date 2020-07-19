import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

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
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'costs', component: CostsComponent, canActivate: [AuthGuard] },
      { path: 'income', component: IncomeComponent },
      { path: 'costplans', component: CostplansComponent },
    ])
  ],
  providers: [ AuthService, ErrorInterceptorProvider ],
  bootstrap: [AppComponent]
})
export class AppModule { }
