import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginUser } from './LoginUser.model';
import { Subject } from 'rxjs';

@Injectable()
export class LoginService {

    public readonly url = 'https://localhost:7207';
    authChanged: any;
    constructor(private http: HttpClient) { }

    loginUser(user: LoginUser) {
        const body: LoginUser = {
            email: user.email,
            password: user.password
        }

        return this.http.post(this.url + "/api/Account/Login", body);
    }

    public changedAuth() {
        return this.authChanged;
    }

    public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
        this.authChanged = isAuthenticated;
    }

    public logout = () => {
        localStorage.removeItem("userToken");
        this.sendAuthStateChangeNotification(false);
    }

}
