import { FormGroup } from "@angular/forms";

export interface FormComponent {
  hasUnsavedChanges: boolean
  form: FormGroup
}

export interface FormsComponent {
  forms: FormGroup[]
}
