import { SignInDTO } from './../models/SignInDTO';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { urls } from '../const.ts';
import { UserDTO } from '../models/UserDTO.js';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  private http = inject(HttpClient);

  constructor() {

  }

  user: UserDTO | undefined;
  isLogin: boolean = false;

  CheckLogin(){
    var request = this.http.get<UserDTO>(urls.server + 'login');
    request.subscribe(
      date=>{
        this.user = date;
        this.isLogin = true;
      }
    );

    return request
  }

  SignIn(signin: SignInDTO ) {

    var request = this.http.post<UserDTO>(urls.server + 'login', signin);
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
