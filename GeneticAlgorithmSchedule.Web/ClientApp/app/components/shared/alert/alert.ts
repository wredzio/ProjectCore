export class Alert {
    content: string;
    type: AlertType;
    dissmissed: boolean = false;

    constructor(content: string, alertType: AlertType) {
        this.content = content;
        this.type = alertType;
    }
}

export enum AlertType {
    Error,
    Info,
    Success
}