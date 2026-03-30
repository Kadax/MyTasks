import { SignInDTO } from './../models/SignInDTO';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { UserDTO } from '../models/UserDTO.js';
import { AppSettings } from '../../app.settings';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  private http = inject(HttpClient);

  constructor() {

  }

  user: UserDTO | undefined;
  isLogin: boolean = false;

  isAdmin(): boolean{
    if(this.user)
      return (this.user.roles.findIndex(i=>i === 'Administrator') != -1);
    else
      return false;
  }

  CheckLogin(){
    var request = this.http.get<UserDTO>(AppSettings.env_vars.API_URL + 'login');
    request.subscribe(
      date=>{
        this.user = date;
        this.isLogin = true;
      }
    );

    return request
  }

  SignIn(signin: SignInDTO ) {
    var request = this.http.post<UserDTO>(AppSettings.env_vars.API_URL + 'login', signin);
    request.subscribe(
      date=>{
        this.user = date;
        this.isLogin = true;
      },
      error=>{
        console.warn(error);
      }
    );

    return request;
  }




}
