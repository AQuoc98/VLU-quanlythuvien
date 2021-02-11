import { Injectable, EventEmitter } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { ConfirmDialogComponent } from './confirm-dialog.component';

@Injectable()
export class ConfirmDialogService {

    private bsModalRef: BsModalRef;

    constructor(private modalService: BsModalService) { }

    /**
     * Show confirm dialog
     * @param messageDict: Translate Key
     */
    confirm(messageDict: string): Promise<any> {
        const config = {
            initialState: {
                message: messageDict
            },
            backdrop: true,
            ignoreBackdropClick: true
        }
        this.bsModalRef = this.modalService.show(ConfirmDialogComponent, config);

        const dialogResult: EventEmitter<any> = this.bsModalRef.content.result;

        const promise = new Promise<any>((resolve, reject) => {
            dialogResult.subscribe(
                (success) => {
                    resolve(success);
                    this.bsModalRef.hide();
                    dialogResult.unsubscribe();
                },
                (error) => {
                    reject(error);
                    this.bsModalRef.hide();
                }
            )
        });

        return promise;
    }
}