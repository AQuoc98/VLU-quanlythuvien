import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, ValidatorFn, Validators } from '@angular/forms';

import { FieldConfig, Validator, FormConfig } from '@infrastructure/models';

@Component({
    selector: 'app-dynamic-form',
    templateUrl: 'dynamic-form.component.html',
    exportAs: "dynamicForm"
})
export class DynamicFormComponent implements OnInit, OnChanges {

    @Input() formConfig: FormConfig;

    @Output() formAfterInit: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    dyForm: FormGroup;

    constructor(private formBuilder: FormBuilder) { }

    ngOnInit() {

    }

    ngOnChanges() {
        if (this.formConfig) {
            this.dyForm = this.createControls();
        }

        this.formAfterInit.emit(this.dyForm);
    }

    get value() {
        return this.dyForm.value;
    }

    private createControls() {
        const fGroup = this.formBuilder.group({});
        this.formConfig.fGroups.forEach(fg => {
            fg.fields.forEach(field => {
                const control = this.formBuilder.control(
                    field.value,
                    this.bindValidations(field.validations || [])
                );
                fGroup.addControl(field.name, control);
            });
        });
        return fGroup;
    }

    private bindValidations(validations: Validator[]): ValidatorFn {
        if (validations.length == 0)
            return null;
        const validList = [];
        validations.forEach((valid: Validator) => {
            validList.push(valid.validator)
        });
        return Validators.compose(validList);
    }
}