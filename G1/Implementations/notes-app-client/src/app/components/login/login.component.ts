import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/models/auth.models';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm = new FormGroup({
    Username: new FormControl(),
    Password: new FormControl()
  })

  constructor(private _authService: AuthService,
              private _router: Router) { }

  onSubmit() {

    let usernameValue = this.loginForm.value.Username;
    let passwordValue = this.loginForm.value.Password;

    let loginModel = new LoginModel(usernameValue, passwordValue)

    this._authService.login(loginModel).subscribe({
      next: data => {
        localStorage.setItem("id", data.Id)
        localStorage.setItem("fullname", `${data.FirstName} ${data.LastName}`)
        localStorage.setItem("token", data.Token)
      },
      error: err => console.warn(err.error),
      complete: () => {
        this._router.navigate(["/home"])
      }
    })

  }


}
