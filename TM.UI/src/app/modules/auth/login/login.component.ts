import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { LoginUserRequest, LoginUserResponse } from '@app/models';
import { AuthService, BaseService } from '@app/services';
import { Router } from '@angular/router';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

	userRequest: LoginUserRequest = {
		userName: '',
		password: ''
	};
	userResponse: LoginUserResponse;
	isLoading: boolean = false;

	constructor(
		private userService: AuthService,
		private router: Router,
		private baseService: BaseService
	) { }

	ngOnInit(): void {
	}

	onClickLogin() {
		this.isLoading = true;

		this.userService.login(this.userRequest).pipe(finalize(() => {
			this.isLoading = false;
		})).subscribe(data => {
			sessionStorage.setItem('tkn', data.message);
			this.router.navigate(['projects']).then();
		}, err => {
			this.userRequest.userName = '';
			this.userRequest.password = '';
		});
	}
}

