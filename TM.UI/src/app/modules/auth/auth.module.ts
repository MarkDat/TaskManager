import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '@app/modules/auth/login/login.component';
import { SharedModule } from '@app/modules/shared/shared.module';
import { AuthComponent } from './auth.component';
import {  } from '@angular/router';
import {RouterModule, Routes} from '@angular/router';

export const routes: Routes = [
	{
		path: '',
		component: LoginComponent,
		children: [
			{
				path: 'login',
				component: LoginComponent
			}
		]
	}
];

@NgModule({
  declarations: [
    LoginComponent,
    AuthComponent
  ],
  imports: [
    CommonModule,
	RouterModule.forChild(routes),
    SharedModule,
  ]
})
export class AuthModule { }
