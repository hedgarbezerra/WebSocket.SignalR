import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { AuthenticationResult } from '../interfaces/authentication-result';
import { Login, RefreshToken } from '../interfaces/login';
import { RegisterUser } from '../interfaces/register-user';
import { ApiEmptyResult } from '../../../common/interfaces/api-result';
import moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

constructor(protected httpClient : HttpClient) { }
  private AUTH_KEY = 'bearer-token';
  private AUTH_REFRESH = 'refresh-token';
  private AUTH_VALIDITY = 'token-validity';

  authenticate(login: Login) : Observable<AuthenticationResult>{
    return this.httpClient.post<AuthenticationResult>(`${environment.apiUrl}/identity/login`, login)
      .pipe(tap(authResult =>{
        if(authResult != null){
          this.saveToken(authResult);
        }
      }));
  }

  refresh(refreshToken: RefreshToken) : Observable<AuthenticationResult>{
    return this.httpClient.post<AuthenticationResult>(`${environment.apiUrl}/identity/refresh`, refreshToken)
      .pipe(tap(authResult =>{
        if(authResult != null){
          this.saveToken(authResult);
        }
      }));
  }

  register(user: RegisterUser) : Observable<ApiEmptyResult>{
    return this.httpClient.post<ApiEmptyResult>(`${environment.apiUrl}/identity/refresh`, user);
  }

  signOut(){
    localStorage.clear();
  }

  saveToken(loginResult: AuthenticationResult){
    localStorage.setItem(this.AUTH_KEY, loginResult.accessToken);
    localStorage.setItem(this.AUTH_REFRESH, loginResult.refreshToken);
    let validity = moment().add(loginResult.expiresIn, 's').toDate()
    localStorage.setItem(this.AUTH_VALIDITY, JSON.stringify(validity));
  }


  get bearerToken() : string | null {
    return localStorage.getItem(this.AUTH_KEY);
  }

  get refreshToken() : string | null {
    return localStorage.getItem(this.AUTH_REFRESH);
  }

  get tokenValidity() : Date{
    let dateStr = localStorage.getItem(this.AUTH_VALIDITY) || '';

    return new Date(JSON.parse(dateStr));
  }

  get isUserAuthenticated() : boolean{
    return this.bearerToken != null && this.tokenValidity >= new Date();
  }
}
