import { Injectable, Type, EventEmitter } from '@angular/core';
import { BsModalService, ModalOptions } from 'ngx-bootstrap';

import { ModalResult } from '@core/models';

@Injectable()
export class ModalService {

    constructor(private modalService: BsModalService) { }

    createModalInstance<TComponent>(container: Type<TComponent>, data: any): ModalResult<TComponent> {
        let modalResult = new ModalResult<TComponent>();

        const config: ModalOptions = {
            initialState: {
                initData: data
            },
            backdrop: true,
            ignoreBackdropClick: true,
            class: 'modal-xl'
        }
        const bsModalRef = this.modalService.show(container, config);
        modalResult.onShow.emit();
        modalResult.containerComponent = bsModalRef.content;
        const containerOnCloseSubcriber = bsModalRef.content.onClose.subscribe((data: any) => {
            bsModalRef.hide();
            modalResult.onClose.emit(data);
            containerOnCloseSubcriber.unsubscribe();
        });

        return modalResult;
    }

    createModalMdInstance<TComponent>(container: Type<TComponent>, data: any): ModalResult<TComponent> {
        let modalResult = new ModalResult<TComponent>();

        const config: ModalOptions = {
            initialState: {
                initData: data
            },
            backdrop: true,
            ignoreBackdropClick: true,
            class: 'modal-md modal-dialog-centered'
        }
        const bsModalRef = this.modalService.show(container, config);
        modalResult.onShow.emit();
        modalResult.containerComponent = bsModalRef.content;
        const containerOnCloseSubcriber = bsModalRef.content.onClose.subscribe((data: any) => {
            bsModalRef.hide();
            modalResult.onClose.emit(data);
            containerOnCloseSubcriber.unsubscribe();
        });

        return modalResult;
    }
}