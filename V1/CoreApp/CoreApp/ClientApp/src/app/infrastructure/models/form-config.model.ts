import { FieldGroupConfig } from './field-group-config.model';

export interface FormConfig {
    fGroups: FieldGroupConfig[]
}

export class FormConfig {
    constructor() {
        this.fGroups = [];
    }
}