import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import { LoginUserRequest, LoginUserResponse } from '../models/user.class';
import { BaseService } from './base.service';
import { API_ENDPOINTS } from './endpoint';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = API_ENDPOINTS.Auth;
  constructor(
    private baseService: BaseService
  ) { }


  login(user : LoginUserRequest): Observable<LoginUserResponse>{
    return this.baseService.post(`${this.baseUrl}/login`,user);
  }
}
