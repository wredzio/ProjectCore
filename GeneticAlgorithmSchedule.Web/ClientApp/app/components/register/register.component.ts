import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AlertService } from '../shared/alert/alert.service';
import { Register } from '../register/register';
import { AuthenticationService } from '../shared/authentication/authentication.service';
import { AlertType } from '../shared/alert/alert';
import { ReturnData } from '../shared/returnData/returnData';


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
                var returnData = data as ReturnData<Register>;
                this.router.navigate([this.loginUrl]);
                this.alertService.alert(AlertType.Success, returnData.message, true);
            },
            error => {
                console.log(error);
            });
    }
}