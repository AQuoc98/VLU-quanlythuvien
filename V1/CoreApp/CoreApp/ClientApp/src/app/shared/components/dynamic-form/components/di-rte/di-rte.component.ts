import { Component, OnInit, HostBinding } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { FieldConfig } from '@infrastructure/models/field-config.model';

@Component({
    selector: 'app-di-rte',
    templateUrl: './di-rte.component.html'
})
export class DiRteComponent implements OnInit {
    field: FieldConfig;
    group: FormGroup;
    isSubmited: boolean;

    @HostBinding('class.col-12')
    enableAddClass: boolean = false;

    constructor() { }

    ngOnInit() {
        this.enableAddClass = true;
    }

}