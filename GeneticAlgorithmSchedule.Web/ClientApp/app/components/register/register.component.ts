import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AlertService } from '../shared/alert/alert.service';
import { Register } from '../register/register';
import { AuthenticationService } from '../shared/authentication/authentication.service';


@Component({
    templateUrl: 'register.component.html'
})

export class RegisterComponent {
    model: Register = new Register();
    loginUrl: string = "/login";

    constructor(
        private router: Router,
        private alertService: AlertService,
        private authentitationService: AuthenticationService) { }

    register() {
        this.authentitationService.register(this.model)
            .subscribe(
                data => {
                    this.router.navigate([this.loginUrl]);
                },
                error => {
                    console.log(error);
                });
    }
}