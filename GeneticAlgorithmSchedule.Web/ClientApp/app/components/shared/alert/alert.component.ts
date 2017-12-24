import { Component, OnInit } from '@angular/core';

import { AlertService } from '../alert/alert.service';
import { Alert, AlertType } from '../alert/alert'

@Component({
    selector: 'alert',
    templateUrl: 'alert.component.html',
    styleUrls: ['./alert.component.scss'],
})

export class AlertComponent {
    alerts: Alert[] = new Array<Alert>();

    constructor(private alertService: AlertService) { }

    ngOnInit() {
        this.alertService.getAlert().subscribe((alert: Alert) => {
            if (!alert) {
                // clear alerts when an empty alert is received
                this.alerts = [];
                return;
            }

            this.alerts.push(alert);

            setTimeout(() => { this.removeAlert(alert); }, 5000);
        });
    }

    removeAlert(alert: Alert) {
        if (!alert) {
            return;
        }

        this.alerts = this.alerts.filter(x => x !== alert);
    }

    cssClass(alert: Alert) {
        if (!alert) {
            return;
        }

        switch (alert.type) {
            case AlertType.Success:
                return 'alert alert-success';
            case AlertType.Error:
                return 'alert alert-danger';
            case AlertType.Info:
                return 'alert alert-info';
        }
    }
}