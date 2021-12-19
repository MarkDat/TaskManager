import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '@app/modules/auth/login/login.component';
import { SharedModule } from '@app/modules/shared/shared.module';
import { AuthComponent } from './auth.component';
import {  } from '@angular/router';
import {RouterModule, Routes} from '@angular/router';
import { AuthRoutingModule } from '@app/modules/auth/auth-routing.module';

@NgModule({
  declarations: [
    LoginComponent,
    AuthComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
	  AuthRoutingModule,
  ]
})
export class AuthModule { }
