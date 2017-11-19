import { Component} from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    currentUser: any;

    constructor() {
        //this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }
}
