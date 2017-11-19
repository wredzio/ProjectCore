import { Component } from '@angular/core';
import { Router } from '@angular/router';
 
import { AlertService} from '../shared/alert/alert.service';
 
@Component({
    templateUrl: 'register.component.html'
})
 
export class RegisterComponent {
    model: any = {};
    loading = false;
 
    constructor(
        private router: Router,
        private alertService: AlertService) { }
 
    register() {
        this.loading = true;
//dodaj logike rejestracji
    }
}