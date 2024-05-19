import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Login } from '../../interfaces/login';
import { FormComponent } from '../../../../common/interfaces/form-component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements FormComponent {

  constructor(private authService: AuthenticationService, private router: Router, private snackBar: MatSnackBar){}
  hasUnsavedChanges: boolean = true;

  hidePassword: boolean = true;

  get email():FormControl{
    return this.form.get('email') as FormControl;
  }
  get password():FormControl{
    return this.form.get('password') as FormControl;
  }

  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  onSubmit(): void{
    let login = this.form.value as Login;

    this.authService.authenticate(login)
      .subscribe({
        next: authResult =>{
          if(this.authService.isUserAuthenticated){
            this.router.navigate(['/'])
          }
        },
        error: err =>{
          this.snackBar.open('Usuário ou senha inválidos, tente novamente.')
        }
      });
  }
}
