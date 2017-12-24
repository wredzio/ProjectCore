import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/do';

import { AlertService } from '../components/shared/alert/alert.service'
import { AlertType } from '../components/shared/alert/alert'

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private alertService: AlertService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).do((event: HttpEvent<any>) => { },
            (err: any) => {
                let message = err.error.errorMessage;
                this.alertService.alert(AlertType.Error, message, true);
            }
        );
    }

}