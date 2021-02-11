import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Subject, forkJoin } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import * as _ from 'lodash';

import { CoreModuleApiService, CorePermissionApiService } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { FormHelper } from '@infrastructure/helpers';
import { Toast } from '@core/services';
import { Guid } from '@infrastructure/common';
import { ApiResult } from '@core/models';
import { Constants } from '@core/constants';

@Component({
    selector: 'app-permission-add',
    templateUrl: 'permission-add.component.html'
})
export class PermissionAddComponent implements OnInit {

    //#region Fields
    dataForm: FormGroup;
    dataModel: any = {};
    permissions: any = [];
    modules: any = [];
    modulePermissions: any = [];
    isSubmited: boolean = false;
    types: any = ["BUS", "SCHOOL"];
    private isFormValid: boolean = false;
    private destroyed$ = new Subject();
    //#endregion

    constructor(private formBuilder: FormBuilder,
        private coreModuleApiService: CoreModuleApiService,
        private corePermissionApiService: CorePermissionApiService,
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

    get fControls() {
        return this.dataForm.controls;
    }

    trackByFn(item) {
        return item.moduleId;
    }

    fullControlChanged(item) {
        this.permissions.forEach(permission => {
            item[permission.code] = item.fullControl;
        });
    }
    //#endregion

    //#region Private Functions
    private loadRefData() {
        const getPermissions = this.corePermissionApiService.getPermissions();
        const getModules = this.coreModuleApiService.getModulesByCurrentUser();

        forkJoin([getPermissions, getModules]).subscribe(res => {
            this.permissions = _.orderBy(res[0], 'position', 'asc');
            this.modules = res[1];

            this.buildModulePermissionData();
        });
    }

    private createForm() {
        this.dataForm = this.formBuilder.group({
            code: ['', Validators.required],
            name: ['', Validators.required],
            // type: ['', Validators.required],
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

    private beforeSaveData() {
        let permissionDetails = [];
        this.modulePermissions.forEach(item => {
            this.permissions.forEach(permission => {
                if (item[permission.code] === true) {
                    permissionDetails.push({
                        roleId: this.fControls.id.value,
                        moduleId: item.moduleId,
                        permissionId: permission.id
                    });
                }
            });
        });
        this.dataModel.coreRoleModulePermissions = permissionDetails;
    }

    private saveData() {
        // Submited
        this.isSubmited = true;

        // Validate form
        if (!this.isFormValid) return;
        this.beforeSaveData();
        Object.assign(this.dataModel, this.dataForm.value);
        // Check add or update event
        if (this.fControls.id.value == Guid.empty) {
            // Add
            this.corePermissionApiService.insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        } else {
            // Update
            this.corePermissionApiService.update(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        }
    }

    private saveDataCompleted(res: ApiResult) {
        this.toast.apiResult(res);
        if (res.status == Constants.ResultStatus.Success)
            this.loadData(res.recordId);
    }

    private loadData(id: string) {
        this.corePermissionApiService.getById(id).subscribe((res) => {
            Object.assign(this.dataModel, res);
            FormHelper.setValue(this.dataForm, res);

            this.buildModulePermissionData();
        });
    }

    private buildModulePermissionData() {
        let modulePermissions = [];
        this.modules.forEach(module => {
            let mpItem = {
                moduleId: module.id,
                moduleName: module.name,
                fullControl: false
            };

            this.permissions.forEach(permission => {
                // Check permission is assigned for this role
                const permissionExisted = _.find(this.dataModel.coreRoleModulePermissions,
                    (item) => item.moduleId == module.id && item.permissionId == permission.id);
                mpItem[permission.code] = permissionExisted !== undefined;
            });

            modulePermissions.push(mpItem);
        });

        this.modulePermissions = modulePermissions;
    }
    //#endregion

}
