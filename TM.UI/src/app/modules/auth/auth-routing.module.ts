import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from '@app/modules/auth/login/login.component';
import { AuthComponent } from './auth.component';


const routes: Routes = [
	{
		path: '',
		component: AuthComponent,
		children: [
			{path: '', redirectTo: 'login', pathMatch: 'full'},
			{
				path: 'login',
				component: LoginComponent
			}
		]
	}
];

@NgModule({
	declarations: [],
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class AuthRoutingModule { }