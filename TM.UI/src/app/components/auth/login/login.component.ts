import { Component, OnInit } from '@angular/core';
import { LoginUserRequest, LoginUserResponse } from '../../../models/user.class';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userRequest : LoginUserRequest = {
    userName : '',
    password : ''
  };
  userResponse : LoginUserResponse; 

  constructor(
    private userrService : AuthService
  ) { }

  ngOnInit(): void {
  }

  onClickLogin(){
      this.userrService.login(this.userRequest).subscribe(data =>{
          console.log(data);
      });
  }
}
