import { LoginComponent } from './../../modules/users/components/login/login.component';
import { ActivatedRouteSnapshot, CanDeactivateFn, Router, RouterStateSnapshot, type CanActivateChildFn, type CanActivateFn } from '@angular/router';
import { AuthenticationService } from '../../modules/users/services/authentication.service';
import { inject } from '@angular/core';
import { FormComponentUnion, isMultipleFormsComponent, isSingleFormComponent } from '../types/form-component';

export const canActivateAuthenticatedRouteGuard: CanActivateFn = (route, state) => {
  let service = inject(AuthenticationService);
  let router = inject(Router);

  let isAuthenticated = service.isUserAuthenticated;

  if(!isAuthenticated)
    router.navigate(['/login']);

  return isAuthenticated;
};

export const canActivateChildAuthenticatedRouteGuard: CanActivateChildFn = (route, state) => {
  let service = inject(AuthenticationService);
  let router = inject(Router);

  let isAuthenticated = service.isUserAuthenticated;

  if(!isAuthenticated)
    router.navigate(['/login']);

  return isAuthenticated;
};

export const unsavedChangesGuard: CanDeactivateFn<FormComponentUnion> = (component: FormComponentUnion, currentRoute: ActivatedRouteSnapshot,
  currentState: RouterStateSnapshot, nextState?: RouterStateSnapshot) => {

    if(component instanceof LoginComponent && nextState?.url.includes('app')){
      return true;
    }

    if (isSingleFormComponent(component) && component.form.dirty) {
      return confirm('Você tem alterações não salvas, deseja continuar a navegação? As alteraçõe serão perdidas.');
    }
    else if (isMultipleFormsComponent(component) && component.forms.some(form => form.dirty)) {
      return confirm('Você tem alterações não salvas em um ou mais formulários, deseja continuar a navegação? As alteraçõe serão perdidas.');
    }

  return true;
};

export const canActivateAlreadyAuthenticatedRouteGuard: CanActivateFn = (route, state) => {
  let service = inject(AuthenticationService);
  let router = inject(Router);

  let isAuthenticated = service.isUserAuthenticated;

  if(isAuthenticated)
    router.navigate(['/app']);

  return !isAuthenticated;
};
