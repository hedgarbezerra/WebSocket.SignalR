import { FormGroup } from "@angular/forms";
import { FormComponent, FormsComponent } from "../interfaces/form-component";

export type FormComponentUnion = FormComponent | FormsComponent;

export function isSingleFormComponent(component: any): component is FormComponent {
  return (component as FormComponent).form instanceof FormGroup;
}

export function isMultipleFormsComponent(component: any): component is FormsComponent {
  return Array.isArray((component as FormsComponent).forms) &&
         (component as FormsComponent).forms.every(form => form instanceof FormGroup);
}
