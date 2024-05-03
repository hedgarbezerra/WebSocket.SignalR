import { HttpInterceptor, HttpRequest, HttpHandler } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { AuthenticationService } from "../../modules/users/services/authentication.service";

@Injectable()
export class BearerTokenRequestAssignerInterceptor implements HttpInterceptor {
  constructor(private authService: AuthenticationService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const authToken = "";//this.authService.token;

    req = req.clone({
      setHeaders: {
        Authorization: 'Bearer ' + authToken,
        'x-api-version': environment.apiVersion,
      },
    });
    return next.handle(req);
  }
}

