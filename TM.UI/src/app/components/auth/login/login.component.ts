import { Component, Input, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { LoginUserRequest, LoginUserResponse } from '@app/models';
import { AuthService } from '@app/services';
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
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  onClickLogin() {
    this.isLoading = true;

    this.userService.login(this.userRequest).pipe(finalize(() => {
      this.isLoading = false;
    })).subscribe(data => {
      sessionStorage.setItem('tkn', data.message);
      this.router.navigate(['projects']).then(()=>{
          window.location.reload();
      });
    }, err => {
      this.userRequest.userName = '';
      this.userRequest.password = '';
    });
  }
}
