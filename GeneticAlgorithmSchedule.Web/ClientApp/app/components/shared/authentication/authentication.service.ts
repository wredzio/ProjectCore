import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
 
import { getBaseUrl } from '../../../app.module.browser';
import { Login } from '../../login/login';
import { Register } from '../../register/register';

@Injectable()
export class AuthenticationService {
    constructor(private http: Http) { }
 
    login(login: Login) {
        return this.http.post(getBaseUrl() + 'api/users/Login', login)
            .map((response: Response) => {
                let user = response.json();
                if (user) {
                    console.log(user);
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }
            });
    }
 
    logout() {
        localStorage.removeItem('currentUser'); 
    }

    register(register: Register) {
        return this.http.post(getBaseUrl() + 'api/users/Register', register)
            .map((response: Response) => {
                let user = response.json();
                console.log(user);
            });
    }
}