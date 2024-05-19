import { RefreshToken } from './../../modules/users/interfaces/login';
import { AuthenticationService } from './../../modules/users/services/authentication.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, throwError } from "rxjs";

@Injectable()
export class UnauthenticatedUserInterceptor implements HttpInterceptor {
  constructor(private router: Router, private authService: AuthenticationService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    return next.handle(req).pipe(
      catchError((err) => {
        if (err instanceof HttpErrorResponse && err.status === 401 && !err.url?.includes('login')){
          if(this.authService.refreshToken == null || this.authService.refreshToken.length <= 0){
            this.router.navigate(['/login']);
          }
          else{
            let refreshToken = { refreshToken: this.authService.refreshToken } as RefreshToken
            this.authService.refresh(refreshToken)
            .subscribe(result =>{
              if(result == null){
                this.router.navigate(['/login']);
              }
            })
          }
        }

        return throwError(() => err);
      })
    );
  }
}
