import { EventEmitter, Input } from '@angular/core';

export class ModalContainerBaseComponent<T> {
    @Input() initData: T;

    protected onClose: EventEmitter<any> = new EventEmitter();

    constructor() { }

    public closeModal(){
        this.onClose.emit();
        this.onClose.complete();
    }
}