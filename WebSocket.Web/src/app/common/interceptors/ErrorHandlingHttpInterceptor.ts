import { HttpInterceptor, HttpRequest, HttpHandler, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, throwError } from "rxjs";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ApiEmptyResult } from "../interfaces/api-result";

@Injectable()
export class ErrorHandlingHttpInterceptor implements HttpInterceptor {
  constructor(private snackBar: MatSnackBar) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    return next.handle(req)
    .pipe(catchError((result : ApiEmptyResult) => {
      result.errors.forEach(err =>
         this.snackBar.open(err, 'Fechar', { duration: 5000}));

        return throwError(() => result);
      })
    );
  }
}
