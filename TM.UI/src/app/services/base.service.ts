import { Injectable } from '@angular/core';
import {Router} from '@angular/router';
import {BehaviorSubject, Observable, throwError} from 'rxjs';
import {HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse} from '@angular/common/http';
import { AppNotify } from '../common/AppNotify.class';
import {catchError, map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  constructor(private router: Router, private httpClient: HttpClient) { }

  TOKEN_LOGIN_NAME : string = "tkn";
  get headerAuthorizationKey(): string {
		return 'Bearer ' + sessionStorage.getItem(this.TOKEN_LOGIN_NAME);
	}

  get options() {
		return {
			headers: new HttpHeaders({
				'Content-Type': 'application/json',
				Authorization: this.headerAuthorizationKey,
			})
		};
	}

  get baseURL(): string {
		return 'http://localhost:44322';
	}
  get fileOptions() {
		return {
			headers: new HttpHeaders({
				Authorization: this.headerAuthorizationKey,
			})
		};
	}

  private handleError(error: HttpErrorResponse){
	let messageError = error.error.message;

	if(error.status != 500)
		return throwError(messageError);
		
	return throwError('Something bad happened; please try again later.');
  }

  get<T>(url: string): Observable<T> {
		return this.httpClient
			.get<T>(`${this.baseURL}/${url}`, this.options)
			.pipe(catchError((error) => this.handleError(error)));
	}

	post<T>(url: string, data: any, isCatchError: boolean = true): Observable<T> {
		return this.httpClient
			.post<T>(`${this.baseURL}/${url}`, data, this.options)
			.pipe(catchError(isCatchError ? this.handleError : (error) => throwError(error)));
	}

  delete<T>(url: string): Observable<T> {
		return this.httpClient
			.delete<T>(`${this.baseURL}/${url}`, this.options)
			.pipe(catchError(this.handleError));
	}

  put<T>(url: string, data: any): Observable<T> {
		return this.httpClient
			.put<T>(`${this.baseURL}/${url}`, data, this.options)
			.pipe(catchError(this.handleError));
	}
}