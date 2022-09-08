import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterModel } from 'src/app/models/auth.models';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor(private _authService: AuthService, 
              private _router: Router) {}

  registerForm = new FormGroup({
    FirstName: new FormControl(),
    LastName: new FormControl(),
    Username: new FormControl(),
    Password: new FormControl(),
    ConfirmPassword: new FormControl()
  })

  onSubmit() {

    let firstNameValue = this.registerForm.value.FirstName;
    let lastNameValue = this.registerForm.value.LastName;
    let usernameValue = this.registerForm.value.Username;
    let passwordValue = this.registerForm.value.Password;
    let confirmPasswordValue = this.registerForm.value.ConfirmPassword;

    let registerModel = new RegisterModel(firstNameValue, 
                                          lastNameValue, 
                                          usernameValue, 
                                          passwordValue, 
                                          confirmPasswordValue)

    this._authService.register(registerModel).subscribe({
      next: data => console.log(data),
      error: err => console.warn(err.error),
      complete: () => {
        this._router.navigate(["/login"])
      }
    })                                   
  }

}
