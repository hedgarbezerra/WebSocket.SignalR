import { HttpInterceptor, HttpRequest, HttpHandler, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EMPTY, catchError, throwError } from "rxjs";
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable()
export class ErrorHandlingHttpInterceptor implements HttpInterceptor {
  constructor(private snackBar: MatSnackBar) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    return next.handle(req)
    .pipe(catchError((err) => {
      let errorWithCode = HandleRequestError(err);
      this.snackBar.open(errorWithCode[1], 'Fechar', { duration: 5000});
        return throwError(() => err);
      })
    );
  }
  HandleRequestError<T extends DefaultResponse<any>>(err : any): [number, string] {
    if(err.status == 400){
      if(typeof(err.error) === 'string')
        return [400, err.error];

      let errAsResult = err.error as T;
      return [400, errAsResult.messages.join(' \n')];
    }
    else if(err.status = 500){
      let errorMessage = err.error as string;
      if(typeof(errorMessage) == 'string')
        return [err.status, errorMessage]

      return [err.status, 'Houve um erro inesperado com a conexão com o servidor, tente novamente em instantes.']
    }
    return [err?.status ?? 500, 'Houve um erro inesperado com a conexão com o servidor, tente novamente em instantes.']
}
}
