import { Injectable } from '@angular/core';
import { HttpClient , HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
 
import { getBaseUrl } from '../../../app.module.browser';
import { Login } from '../../login/login';
import { Register } from '../../register/register';

@Injectable()
export class AuthenticationService {
    constructor(private httpClient: HttpClient) { }

    login(login: Login) {
        return this.httpClient.post(getBaseUrl() + 'api/user/Login', login);
    }
 
    logout() {
        localStorage.removeItem('currentUser'); 
    }

    register(register: Register) {
        return this.httpClient.post(getBaseUrl() + 'api/user/Register', register);
    }
}