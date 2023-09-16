import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {MatSnackBar} from "@angular/material/snack-bar";
import {ErrorSnackbarComponent} from "../modals/error-snackbar/error-snackbar.component";
import {ErrorMessages} from "../utils/ErrorMessages";
import {AuthService} from "../services/auth.service";


@Injectable()
export class HttpErrorsInterceptor implements HttpInterceptor {

  constructor(public snackBar: MatSnackBar,
              public authService: AuthService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if (error.status > 401 && error.status < 500) {
            const errorText = ErrorMessages.getTextMessage(error);
            this.snackBar.openFromComponent(ErrorSnackbarComponent, {data: errorText});
          }
          if (error.status == 401 && this.authService.isAuthenticated()) {
            this.authService.logout();
          }
          return throwError(() => error);
        })
      )
  }
}
