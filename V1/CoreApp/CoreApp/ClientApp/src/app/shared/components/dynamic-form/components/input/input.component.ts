import { Component, OnInit, HostBinding } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { FieldConfig } from '@infrastructure/models/field-config.model';

@Component({
    selector: 'app-input',
    templateUrl: './input.component.html'
})
export class InputComponent implements OnInit {
    field: FieldConfig;
    group: FormGroup;
    isSubmited: boolean;

    @HostBinding('class.col-6')
    enableAddClass: boolean = false;

    constructor() { }

    ngOnInit() {
        this.enableAddClass = true;
    }

}