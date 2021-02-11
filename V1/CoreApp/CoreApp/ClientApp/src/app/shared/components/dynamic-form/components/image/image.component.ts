import { Component, OnInit, HostBinding } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { FieldConfig } from '@infrastructure/models/field-config.model';

@Component({
    selector: 'app-image',
    templateUrl: './image.component.html'
})
export class ImageComponent implements OnInit {
    field: FieldConfig;
    group: FormGroup;
    isSubmited: boolean;

    @HostBinding('class.col-3')
    enableAddClass: boolean = false;

    constructor() { }

    ngOnInit() {
        this.enableAddClass = true;
    }

}