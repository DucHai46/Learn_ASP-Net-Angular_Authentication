import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from './user.model';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  readonly Url = 'https://localhost:7207'
  constructor(private http: HttpClient) { }

  registerUser(user: User) {
    const body: User = {
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      password: user.password,
      confirmPassword: user.confirmPassword,
    }

    return this.http.post(this.Url + '/api/Account/Registration', body);
  }

}
