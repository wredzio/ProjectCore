import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/do';

import { LoaderService } from '../components/shared/loader/loader.service'

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {

    constructor(private loadingService: LoaderService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.loadingService.show();

        return next.handle(req).do((event: HttpEvent<any>) => {
            if (event instanceof HttpResponse) {
                this.loadingService.hide();
            }

        }, (err: any) => {
            this.loadingService.hide();
        });
    }

}