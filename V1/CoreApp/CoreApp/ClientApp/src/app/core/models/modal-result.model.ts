import { EventEmitter, Type } from '@angular/core';

export interface ModalResult<TComponent> {
    onShow: EventEmitter<any>,
    onClose: EventEmitter<any>,
    containerComponent: Type<TComponent>
}

export class ModalResult<TComponent>{
    constructor() {
        this.onShow = new EventEmitter<any>();
        this.onClose = new EventEmitter<any>();
    }
}