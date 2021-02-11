import { Injectable } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Injectable()
export class FormHelper {

    constructor() { }

    static setValue(form: FormGroup, value: any) {
        // for (let prop in value) {
        //     if (form.contains(prop)) {
        //         form.controls[prop].setValue(value[prop]);
        //     }
        // }
        form.patchValue(value);
        // console.log(value);
    }
}