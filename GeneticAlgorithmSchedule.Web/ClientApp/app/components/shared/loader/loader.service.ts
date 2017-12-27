import { Injectable } from '@angular/core';
import { Event as RouterEvent, NavigationStart, NavigationEnd, NavigationCancel, NavigationError } from '@angular/router';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class LoaderService {
    private subject = new Subject<boolean>();

    navigationInterceptor(event: RouterEvent): boolean {

        if (event instanceof NavigationStart) {
            return true;
        }

        if (event instanceof NavigationEnd) {
            return false;
        }
        if (event instanceof NavigationCancel) {
            return false;
        }
        if (event instanceof NavigationError) {
            return false;
        }

        return false;
    }

    show() {
        this.subject.next(true);
    }

    hide() {
        this.subject.next(false);
    }

    getLoaderStatus(): Observable<boolean> {
        return this.subject.asObservable();
    }
}