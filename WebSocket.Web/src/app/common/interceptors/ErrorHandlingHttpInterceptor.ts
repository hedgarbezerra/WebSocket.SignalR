import { HttpInterceptor, HttpRequest, HttpHandler, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, throwError } from "rxjs";
import { MatSnackBar } from "@angular/material/snack-bar";
import { isApiResult } from "../interfaces/api-result";

@Injectable()
export class ErrorHandlingHttpInterceptor implements HttpInterceptor {
  constructor(private snackBar: MatSnackBar) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    return next.handle(req)
    .pipe(catchError((error) => {

      if(isApiResult(error)){
        error.errors.forEach(err =>
          this.snackBar.open(err, 'Fechar', { duration: 5000}));
      }
      else if(error instanceof HttpErrorResponse && error.status === 500){
        this.snackBar.open('Houve um erro ao processar sua solicitação.', 'Fechar', { duration: 5000});
      }

        return throwError(() => error);
      })
    );
  }
}
