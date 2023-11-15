import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from 'src/app/services/LoginUser.model';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
  user: LoginUser;
  isLoginError: boolean = false;
  constructor(private loginservice: LoginService, private router: Router) { }

  ngOnInit() {
    this.user = {
      email: "",
      password: ""
    }
  }

  OnSubmit(form: NgForm) {
    this.loginservice.loginUser(this.user)
      .subscribe({
        next: (data: any) => {
          if (data.isAuthSuccessful == true) {
            localStorage.setItem('userToken', data.token);
            this.loginservice.sendAuthStateChangeNotification(true);
            // alert("Login Successful")
            this.router.navigate(['']);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.isLoginError = true;
        }
      });
  }

}
