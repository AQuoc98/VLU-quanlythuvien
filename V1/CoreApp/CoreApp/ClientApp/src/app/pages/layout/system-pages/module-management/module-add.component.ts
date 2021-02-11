import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Subject, forkJoin } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import * as _ from 'lodash';

import { CoreModuleApiService } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { FormHelper } from '@infrastructure/helpers';
import { Guid } from '@infrastructure/common';
import { Toast } from '@core/services';
import { ApiResult } from '@core/models';
import { Constants } from '@core/constants';
import { DataModel } from '@infrastructure/models';

@Component({
    selector: 'app-module-add',
    templateUrl: 'module-add.component.html'
})
export class ModuleAddComponent implements OnInit {

    //#region Fields
    dataForm: FormGroup;
    dataModel: DataModel = new DataModel();
    isSubmited: boolean = false;
    private isFormValid: boolean = false;
    private destroyed$ = new Subject();
    //#endregion

    constructor(private formBuilder: FormBuilder,
        private coreModuleApiService: CoreModuleApiService,
        private confirmDialogService: ConfirmDialogService,
        private router: ActivatedRoute,
        private toast: Toast) {
        this.createForm();
    }

    //#region Page Cycles Functions
    ngOnInit() {
        this.loadRefData();
        this.readParams();
    }

    ngOnDestroy() {
        this.destroyed$.next();
        this.destroyed$.complete();
    }
    //#endregion

    //#region Public Functions
    get fControls() {
        return this.dataForm.controls;
    }

    saveEvent() {
        this.confirmDialogService.confirm('CONFIRM_DIALOG.CONFIRM_SAVE_MESSAGE').then(
            (accept) => {
                // Accept
                this.saveData();
            },
            (decline) => {
                // Decline
                // Inogre
            });
    }
    //#endregion

    //#region Private Functions
    private loadRefData() {

    }

    private createForm() {
        this.dataForm = this.formBuilder.group({
            code: ['', Validators.required],
            name: ['', Validators.required],
            isActived: [false, Validators.required]
        });

        this.dataForm.valueChanges
            .pipe(
                // To manage unsubscribe
                takeUntil(this.destroyed$)
            )
            .subscribe((v) => {
                this.isFormValid = this.dataForm.valid;
            });
    }

    private readParams() {
        this.router.params
            .pipe(
                takeUntil(this.destroyed$)
            )
            .subscribe(params => {
                const id: string = params['id'];
                this.dataForm.addControl('id', new FormControl(id));
                if (id != Guid.empty)
                    this.loadData(id);
            });
    }

    private saveData() {
        // Submited
        this.isSubmited = true;

        // Validate form
        if (!this.isFormValid) return;
        Object.assign(this.dataModel, this.dataForm.value);
        // Check add or update event
        if (this.fControls.id.value == Guid.empty) {
            // Add
            this.coreModuleApiService.insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        } else {
            // Update
            this.coreModuleApiService.update(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        }
    }

    private saveDataCompleted(res: ApiResult) {
        this.toast.apiResult(res);
        if (res.status == Constants.ResultStatus.Success)
            this.loadData(res.recordId);
    }

    private loadData(id: string) {
        this.coreModuleApiService.getById(id).subscribe((res) => {
            Object.assign(this.dataModel, res);
            FormHelper.setValue(this.dataForm, res);
        })
    }
    //#endregion

}