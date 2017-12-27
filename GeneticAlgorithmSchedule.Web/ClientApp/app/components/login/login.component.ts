/// <reference path="login.ts" />
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
 
import { AlertService} from '../shared/alert/alert.service';
import { AuthenticationService } from '../shared/authentication/authentication.service';
import { Login } from '../login/login';
 
@Component({
    templateUrl: 'login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    model: Login = new Login();
    returnUrl: string;
 
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private alertService: AlertService) { }
 
    ngOnInit() {
        this.authenticationService.logout();
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }
 
    login() {
        this.authenticationService.login(this.model)
            .subscribe(
                data => {
                    this.router.navigate([this.returnUrl]);
                    localStorage.setItem('currentUser', JSON.stringify(data));
                },
                error => {
                    console.log(error);
                });
    }
}