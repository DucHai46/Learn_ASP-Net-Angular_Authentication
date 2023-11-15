import { Component } from '@angular/core';
import { User } from 'src/app/services/user.model';
import { NgForm } from '@angular/forms';
import { RegisterService } from 'src/app/services/register.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
  user: User;
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";

  constructor(private userService: RegisterService, private router: Router) { }

  ngOnInit() {
    this.resetForm();

  }

  resetForm() {
    this.user = {
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      confirmPassword: "",
    }
  }


  checks(user: User) {
    if (user.confirmPassword === '') {
      return null;
    }

    if (user.password !== user.confirmPassword) {
      return { mustMatch: true };
    }

    return false;
  }

  check: any = '';


  OnSubmit(form: NgForm) {
    this.check = this.checks(this.user);
    if (!this.check) {
      this.userService.registerUser(this.user)
        .subscribe({
          next: (data: any) => {
            if (data.isSuccessfulRegistration == true) {
              this.resetForm();
              console.log(data.errors)
              // alert('User registration successful');
              this.router.navigate(['/signin']);
            }
          }
        });
    }
  }

}

