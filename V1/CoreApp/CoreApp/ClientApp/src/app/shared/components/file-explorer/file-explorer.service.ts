import { Injectable, EventEmitter } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';

import { FileExplorerComponent } from './file-explorer.component';

@Injectable()
export class FileExplorerService {

    private bsModalRef: BsModalRef;

    constructor(private modalService: BsModalService) { }

    /**
     * Choose file
     */
    chooseFile(): Promise<any> {
        const config: ModalOptions = {
            initialState: {

            },
            backdrop: true,
            ignoreBackdropClick: true,
            class: 'modal-lg'
        }
        this.bsModalRef = this.modalService.show(FileExplorerComponent, config);
    
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

    /**
    * Choose multiple file
    */
    chooseFiles() {
        const config: ModalOptions = {
            initialState: {
                isAllowSelectMultiple: true
            },
            backdrop: true,
            ignoreBackdropClick: true,
            class: 'modal-lg'
        }
        this.bsModalRef = this.modalService.show(FileExplorerComponent, config);

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