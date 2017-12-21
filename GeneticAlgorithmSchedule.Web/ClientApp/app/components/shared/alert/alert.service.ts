import { Injectable } from '@angular/core';
import { Router, NavigationStart, Event } from '@angular/router';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';

import { Alert, AlertType } from '../alert/alert'

@Injectable()
export class AlertService {
    private subject = new Subject<Alert>();
    private keepAfterNavigationChange = false;

    constructor(private router: Router) {
        router.events.subscribe((event: Event) => {
            if (event instanceof NavigationStart) {
                if (this.keepAfterNavigationChange) {
                    this.keepAfterNavigationChange = false;
                } else {
                    this.subject.next();
                }
            }
        });
    }

    alert(type: AlertType, message: string, keepAfterNavigationChange = false) {
        this.keepAfterNavigationChange = keepAfterNavigationChange;
        this.subject.next({ type, content: message, dissmissed: true });
    }

    getAlert(): Observable<Alert> {
        return this.subject.asObservable();
    }    
}