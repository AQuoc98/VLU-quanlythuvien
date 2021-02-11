import { FieldConfig } from './field-config.model';

export interface FieldGroupConfig {
    name: string,
    label: string,
    sort: number,
    fields: FieldConfig[]
}

export class FieldGroupConfig {
    constructor() {

    }
}