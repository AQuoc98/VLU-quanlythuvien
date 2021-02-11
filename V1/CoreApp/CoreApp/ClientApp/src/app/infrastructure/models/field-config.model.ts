export interface FieldConfig {
    label?: string;
    name?: string;
    inputType?: string;
    option?: SelectOption;
    collections?: any;
    type: string;
    value?: any;
    validations?: Validator[];
}

export class FieldConfig {
    constructor() {

    }
}

export interface Validator {
    name: string;
    validator: any;
    message: string;
}

export interface SelectOption {
    valueField: string,
    displayField: string,
    data: any
}