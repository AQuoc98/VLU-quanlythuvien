import { Injectable, OnInit } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Subject } from 'rxjs';

import { ToastrConfig, ApiResult } from '../models';
import { Constants } from '../constants/constants';
import { TranslateService } from '@ngx-translate/core';
import { takeUntil } from 'rxjs/operators';

@Injectable()
export class Toast implements OnInit {

    private destroyed$ = new Subject();
    private toastrConfig: ToastrConfig = {
        toastTimeout: 10000,
        dismiss: 'auto',
        newestOnTop: true,
        showCloseButton: true,
        maxShown: 5,
        position: 'bottom-right',
        messageClass: '',
        titleClass: '',
        animate: 'slideFromBottom',
        enableHTML: true
    }
    private successMessage: string;
    private errorMessage: string;
    private warningMessage: string;
    private infoMessage: string;
   
    constructor(private toastrManager: ToastrManager, private translateService: TranslateService) {

        // Get translations
        this.translateService.get(['COMMON.TOAST_TITLE_SUCCESS',
            'COMMON.TOAST_TITLE_ERROR',
            'COMMON.TOAST_TITLE_INFO',
            'COMMON.TOAST_TITLE_WARNING']).pipe(
                takeUntil(this.destroyed$)
            ).subscribe(res => {
                this.successMessage = res['COMMON.TOAST_TITLE_SUCCESS'];
                this.errorMessage = res['COMMON.TOAST_TITLE_ERROR'];
                this.infoMessage = res['COMMON.TOAST_TITLE_INFO'];
                this.warningMessage = res['COMMON.TOAST_TITLE_WARNING'];
            });
    }

    ngOnInit() {

    }

    ngOnDestroy() {
        this.destroyed$.next();
        this.destroyed$.complete();
    }

    success(msg: string) {
        this.translateService.get([msg]).pipe(
            takeUntil(this.destroyed$)
        ).subscribe(res => {
            this.toastrManager.successToastr(res[msg], this.successMessage, this.toastrConfig);
        });
    }

    error(msg: string){ 
        this.translateService.get([msg]).pipe(
            takeUntil(this.destroyed$)
        ).subscribe(res => {
            this.toastrManager.errorToastr(res[msg], this.errorMessage, this.toastrConfig);
        });
    }

    warning(msg: string) {
        this.translateService.get([msg]).pipe(
            takeUntil(this.destroyed$)
        ).subscribe(res => {
            this.toastrManager.warningToastr(res[msg], this.warningMessage, this.toastrConfig);
        });
    }

    info(msg: string) {
        this.translateService.get([msg]).pipe(
            takeUntil(this.destroyed$)
        ).subscribe(res => {
            this.toastrManager.infoToastr(res[msg], this.infoMessage, this.toastrConfig);
        });
    }

    apiResult(res: ApiResult) {
        if (res.status == Constants.ResultStatus.Success) {
            res.messages.forEach(msg => {
                this.success(msg);
            })
        }
        if (res.status == Constants.ResultStatus.Error) {
            res.messages.forEach(msg => {
                this.error(msg);
            })
        }
        if (res.status == Constants.ResultStatus.ValidateFail) {
            res.messages.forEach(msg => {
                this.error(msg);
            })
        }
    }
}