import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { FieldConfig } from '@infrastructure/models/field-config.model';

@Component({
    selector: 'app-select',
    template: `
    <div class="form-group" [formGroup]="group">
        <label translate>{{field.label}}</label>
        <select class="form-control form-control-sm" [formControlName]="field.name" [ngClass]="{ 'is-invalid': isSubmited && group.get(field.name).errors }">
            <option [ngValue]="item[field.option.valueField]" *ngFor="let item of field.option.data">{{item[field.option.displayField]}}</option>
        </select>
        <ng-container *ngFor="let validation of field.validations;">
            <small class="form-text text-muted invalid-tooltip" *ngIf="group.get(field.name).hasError(validation.name)" translate>{{validation.message}}</small>
        </ng-container>
    </div>
    `
})
export class SelectComponent implements OnInit {
    field: FieldConfig;
    group: FormGroup;
    isSubmited: boolean;
    constructor() { }

    ngOnInit() {

    }

}