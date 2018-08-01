import { Injectable } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DataKeeperService } from '../data-keeper.service';
import { routes } from '../../../shared/constant';
import { map } from "rxjs/operators/map";
import { catchError } from "rxjs/operators/catchError";
import { Observable } from "rxjs";
import { ErrorHandler } from '../../common/ErrorHandler';
import {ITokenparams} from '../../models/IUser'; 

@Injectable()
export class AuthService {
  isAuthenticate = false;
  helper: JwtHelperService = new JwtHelperService();
  currentUser: any;
  redirectUrl: string;
  constructor(private http:HttpClient,private store:DataKeeperService) { }

  public isAuthenticated(): boolean {
    const token = this.Token;
    return !this.helper.isTokenExpired(token);
  }

  get role() {
    return this.store.getData('role');
  }

  get decodeToken() {
    return this.helper.decodeToken(this.Token);
  }

  get Key() {
    return this.store.getData('key');
  }

  get Token() {
    return this.store.getData('token');
  }

  get Udata() {
    return JSON.parse(this.store.getData('udata'));
  }

  tokenSetter(key: string, value: string): void {
    localStorage.setItem(key, value);
  }

  tokenGetter(key): any {
    return localStorage.getItem(key);
  }

  authenticate(username: string, password: string):Observable<ITokenparams>{
    var data = "username=" + username + "&password=" + password;
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded', 'No-Auth': 'True' });
    return this.http.post(routes.LOGIN, data, { headers: reqHeader })
      .pipe(map(loggedin => {
        return loggedin;
      }),
      catchError(ErrorHandler.ErrorServerConnection)
      );

  }

  logout(): boolean {
    if (this.isAuthenticated())
      this.store.removeAllPersistedData();
    localStorage.clear();
    return true;
  }


}
