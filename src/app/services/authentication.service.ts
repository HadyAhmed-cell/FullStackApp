import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Login } from '../models/login';
import { Register } from '../models/register';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { JwtAuth } from '../models/jwtAuth';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  registerUrl ="/api/auth/Register"
  loginUrl = "/api/auth/Login"
  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http:HttpClient) { }

  public register(user:Register):Observable<JwtAuth>{
    return this.http.post<JwtAuth>(this.baseApiUrl + this.registerUrl , user)
  }
  public login(user:Login):Observable<JwtAuth>{
    return this.http.post<JwtAuth>(this.baseApiUrl + this.loginUrl , user)
  }
}
