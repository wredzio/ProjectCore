import { Router, Event } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { LoaderService } from '../loader/loader.service';

@Component({
    selector: 'loader',
    templateUrl: 'loader.component.html',
    styleUrls: ['./loader.component.scss']
})
export class LoaderComponent {
    loading = false;

    constructor(private router: Router, private loadingService: LoaderService) {
        router.events.subscribe((event: Event) => {
            this.loading = loadingService.navigationInterceptor(event);
        })
    }

    ngOnInit() {
        this.loadingService.getLoaderStatus().subscribe((loading: boolean) => { this.loading = loading; });
    }
}