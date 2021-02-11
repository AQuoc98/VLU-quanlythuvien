import { Component, OnInit, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-confirm-dialog',
    templateUrl: 'confirm-dialog.component.html'
})
export class ConfirmDialogComponent implements OnInit {

    title: string;
    message: string;
    result = new EventEmitter();

    constructor() { }

    ngOnInit() {

    }

    accept() {
        this.result.emit('Accept');
    }

    decline() {
        this.result.error('Decline');
    }
}