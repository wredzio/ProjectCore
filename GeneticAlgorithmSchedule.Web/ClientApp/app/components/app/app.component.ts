import { Component, OnInit } from '@angular/core';

import { LoaderService } from '../shared/loader/loader.service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    loading = false;

    constructor( private loadingService: LoaderService) { }

    ngOnInit() {
        this.loadingService.getLoaderStatus().subscribe((loading: boolean) => { this.loading = loading; });
    }
}
