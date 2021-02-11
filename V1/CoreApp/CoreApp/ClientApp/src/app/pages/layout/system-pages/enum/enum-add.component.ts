import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Subject, forkJoin } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import * as _ from 'lodash';

import { CoreEnumApiService } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { FormHelper } from '@infrastructure/helpers';
import { Toast } from '@core/services';
import { Guid } from '@infrastructure/common';
import { ApiResult } from '@core/models';
import { Constants } from '@core/constants';

@Component({
    selector: 'app-enum-add',
    templateUrl: 'enum-add.component.html'
})
export class EnumAddComponent implements OnInit {

    //#region Fields
    dataForm: FormGroup;
    dataModel: any = {};
    modules: any = [];
    isSubmited: boolean = false;
    enumValues: any = [];
    private isFormValid: boolean = false;
    private destroyed$ = new Subject();
    //#endregion

    constructor(private formBuilder: FormBuilder,
        private coreEnumApiService: CoreEnumApiService,
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

    addEnumValue() {
        this.enumValues.push({ enumId: this.fControls.id.value });
    }

    removeEnumValue(index) {
        this.enumValues.splice(index, 1);
    }

    get fControls() {
        return this.dataForm.controls;
    }
    //#endregion

    //#region Private Functions
    private loadRefData() {

        // forkJoin([]).subscribe(res => {

        // });
    }

    private createForm() {
        this.dataForm = this.formBuilder.group({
            code: ['', Validators.required],
            name: ['', Validators.required]
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
        this.dataModel.coreEnumValues = this.enumValues;
        // Check add or update event
        if (this.fControls.id.value == Guid.empty) {
            // Add
            this.coreEnumApiService.insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        } else {
            // Update
            this.coreEnumApiService.update(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        }
    }

    private saveDataCompleted(res: ApiResult) {
        this.toast.apiResult(res);
        if (res.status == Constants.ResultStatus.Success)
            this.loadData(res.recordId);
    }

    private loadData(id: string) {
        this.coreEnumApiService.getById(id).subscribe((res) => {
            Object.assign(this.dataModel, res);
            FormHelper.setValue(this.dataForm, res);

            this.enumValues = this.dataModel.coreEnumValues;
        });
    }
    //#endregion

}