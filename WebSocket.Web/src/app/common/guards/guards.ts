import { CanDeactivateFn, Router, type CanActivateChildFn, type CanActivateFn } from '@angular/router';
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

export const unsavedChangesGuard: CanDeactivateFn<any> = (component: FormComponentUnion) => {
  if (isSingleFormComponent(component) && component.form.dirty) {
    return confirm('You have unsaved changes in the form! If you leave, your changes will be lost.');
  }
  else if (isMultipleFormsComponent(component) && component.forms.some(form => form.dirty)) {
    return confirm('You have unsaved changes in one of the forms! If you leave, your changes will be lost.');
  }

  return true;
};

