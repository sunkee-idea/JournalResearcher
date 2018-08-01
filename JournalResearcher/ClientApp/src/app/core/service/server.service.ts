import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AuthService } from './guard/auth.service';
import { map } from 'rxjs/operators/map';
import { catchError } from 'rxjs/operators/catchError';
import {Observable} from 'rxjs';
import { IUser } from '../models/IUser';
import { SystemConstant as Roles, routes } from '../../shared/constant';
import { ErrorHandler } from '../common/ErrorHandler';
import { IThesis } from '../models/IThesis';

@Injectable()
export class ServerService {

  constructor(private http: HttpClient, private auth: AuthService) { }

  signupApplicant(user: IUser): Observable<IUser> {
    const header = this.requestHeader({ 'Accept': 'application/json', 'Content-Type': 'application/json' });
    return this.http.post<IUser>(routes.REGISTER, user, { headers: header }).pipe(map(user => {
        return user;
      }),
      catchError(ErrorHandler.handleError));
  }

  submitThesis(thesis: FormData): Observable<any> {
    return this.http.post<IThesis>(routes.SUBMITTHESIS, thesis).pipe(map(thesis => {
        return thesis;
      }),
      catchError(ErrorHandler.handleError));
  }

  getAllThesis(): Observable<IThesis[]> {
    return this.http.get<IThesis[]>(routes.GETALLTHESIS).pipe(map(theses => {
        return theses;
      }),
      catchError(ErrorHandler.ErrorServerConnection));
  }

  approveThesis(data:any): Observable<IThesis> {
    return this.http.post<IThesis>(routes.APPROVE, data).pipe(map(thesis => {
        return thesis;
      }),
      catchError(ErrorHandler.ErrorServerConnection));
  }

  getThesis(id: string): Observable<IThesis[]> {
   // let param = new HttpParams().set('page', page.toFixed(1)){ params: param };
    return this.http.get<IThesis[]>(routes.GETTHESIS + id).pipe(map(thesis => {
        return thesis;
      }),
      catchError(ErrorHandler.ErrorServerConnection));
  }

  private requestHeader(contentType: any): any {
    return new HttpHeaders(contentType);

  }

}
