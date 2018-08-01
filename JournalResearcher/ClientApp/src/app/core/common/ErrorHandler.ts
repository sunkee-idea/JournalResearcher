import {HttpErrorResponse} from "@angular/common/http";
import {Observable} from "rxjs";
/**
 * Created by Sunkee on 31-July-18.
 */
export class ErrorHandler{
  static handleError(error: HttpErrorResponse) {
    //console.error('server error:', error);
    const errors:any = error.error;
    if(errors){
      let modelStateError = '';
      for(const key in errors){
        if(errors[key]){
          modelStateError += errors[key] + '\n';
        }
      }
      if(modelStateError){return Observable.throw(modelStateError || 'Unexpected error occurred')}
    }

    if (error.error instanceof Error) {
      const errMessage = error.error.message;
      return Observable.throw(errMessage);
      // Use the following instead if using lite-server
      // return Observable.throw(err.text() || 'backend server error');
    }
    return Observable.throw(error || 'Server error');
  }

  static ErrorConnection(err: HttpErrorResponse) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (err.error instanceof Error) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return Observable.throw(errorMessage);
  }

  static ErrorServerConnection(error: HttpErrorResponse) {
    console.error('server error:', error);
    if (error.error instanceof Error) {
      const errMessage = error.error.message;
      return Observable.throw(errMessage);
      // Use the following instead if using lite-server
      // return Observable.throw(err.text() || 'backend server error');
    }
    return Observable.throw(error || 'Server error');
  }

  private Errorhandler(error:any){
     const applicationError = error.headers.get('Application-Error');
      if(applicationError){
        return Observable.throw(applicationError);
      }
      const serverError = error.json();
    let modelStateErrors = '';
    if(serverError){
      for(const key in serverError){
        if(serverError[key]){
          modelStateErrors += serverError[key] +'\n';
        }
      }
    }

    return Observable.throw(modelStateErrors || 'server Error' )
  }

}
