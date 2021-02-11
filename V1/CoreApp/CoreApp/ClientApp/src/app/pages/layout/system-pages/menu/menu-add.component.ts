import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Subject, forkJoin } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import * as _ from 'lodash';

import { CoreModuleApiService, CoreViewApiService, CoreMenuApiService } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { FormHelper } from '@infrastructure/helpers';
import { Guid } from '@infrastructure/common';
import { Toast } from '@core/services';
import { ApiResult } from '@core/models';
import { Constants } from '@core/constants';
import { DataModel } from '@infrastructure/models';

@Component({
    selector: 'app-menu-add',
    templateUrl: 'menu-add.component.html'
})
export class MenuAddComponent implements OnInit {

    //#region Fields
    dataForm: FormGroup;
    dataModel: DataModel = new DataModel();
    modules: any = [];
    parentMenus: any = [];
    views: any = [];
    isSubmited: boolean = false;
    private isFormValid: boolean = false;
    private destroyed$ = new Subject();
    //#endregion

    constructor(private formBuilder: FormBuilder,
        private coreModuleApiService: CoreModuleApiService,
        private coreViewApiService: CoreViewApiService,
        private coreMenuApiService: CoreMenuApiService,
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

    moduleChanged() {
        this.loadViews(this.fControls.moduleId.value);
    }

    get fControls() {
        return this.dataForm.controls;
    }
    //#endregion

    //#region Private Functions
    private loadRefData() {
        const module = this.coreModuleApiService.getAll();
        const parentMenu = this.coreMenuApiService.getParentMenus();

        forkJoin([module, parentMenu]).subscribe(res => {
            this.modules = res[0];
            this.parentMenus = res[1];
        });
    }

    private createForm() {
        this.dataForm = this.formBuilder.group({
            moduleId: [''],
            viewId: [''],
            name: ['', Validators.required],
            titleDict: ['', Validators.required],
            href: [''],
            icon: [''],
            position: [0, Validators.required],
            parentId: [''],
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
            this.coreMenuApiService.insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        } else {
            // Update
            this.coreMenuApiService.update(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        }
    }

    private saveDataCompleted(res: ApiResult) {
        this.toast.apiResult(res);
        if (res.status == Constants.ResultStatus.Success)
            this.loadData(res.recordId);
    }

    private loadData(id: string) {
        this.coreMenuApiService.getById(id).subscribe((res) => {
            Object.assign(this.dataModel, res);
            FormHelper.setValue(this.dataForm, res);
            this.loadViews(res.moduleId);
        });
    }

    private loadViews(moduleId: string) {
        this.coreViewApiService.getViewsByModule(moduleId).subscribe(res => {
            this.views = res;
        });
    }
    //#endregion

}