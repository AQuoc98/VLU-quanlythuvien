import { Component, OnInit } from '@angular/core';
import * as _ from 'lodash';
import { FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';

import { Logger } from '@core/logger';
import { CoreConfigApiService } from '@infrastructure/api-services';
import { Toast } from '@core/services';
import { ApiResult } from '@core/models';
import { Constants } from '@core/constants';
import { FieldConfig, FormConfig, FieldGroupConfig } from '@infrastructure/models';
import { withLatestFrom } from 'rxjs/operators';

export const logger = new Logger('Config');

@Component({
    selector: 'app-config',
    templateUrl: './config.component.html',
    styleUrls: ['./config.component.scss']
})
export class ConfigComponent implements OnInit {
    //#region Fields
    dataForm: FormGroup;
    dataModel: any = {};
    isSubmited: boolean = false;
    configs: any[] = [];
    configGroups: any[] = [];
    formConfig: FormConfig = new FormConfig();
    private destroyed$ = new Subject();
    //#endregion

    constructor(private coreConfigApiService: CoreConfigApiService,
        private toast: Toast) {

    }

    //#region Page Cycles Functions
    ngOnInit() {
        this.loadData();
    }

    ngOnDestroy() {
        this.destroyed$.next();
        this.destroyed$.complete();
    }
    //#endregion

    //#region Public Functions

    save() {
        this.saveData();
    }

    formAfterInit(form: FormGroup) {
        this.dataForm = form;
    }

    get fControls() {
        return this.dataForm.controls;
    }

    //#endregion

    private createForm() {
        let form: FormConfig = new FormConfig();

        this.configGroups.forEach(cGroup => {
            const fGroup = this.gFieldGroup(cGroup);
            form.fGroups.push(fGroup);
        });

        this.formConfig = form;
    }

    private gFieldGroup(cGroup) {
        let fGroup = new FieldGroupConfig();
        fGroup.name = cGroup.name;
        fGroup.label = cGroup.label;
        fGroup.sort = cGroup.sort;
        fGroup.fields = this.gFields(cGroup.id);

        return fGroup;
    }

    private gFields(gId) {
        let fields: FieldConfig[] = [];

        const configsByGroup = this.configs.filter(f => f.groupId === gId);
        configsByGroup.forEach(conf => {
            let fConfig = new FieldConfig();
            fConfig.inputType = conf.inputType;
            fConfig.type = conf.dataType;
            fConfig.label = conf.label;
            fConfig.value = conf.value;
            fConfig.name = conf.name;
            fields.push(fConfig);
        });

        return fields;
    }

    private saveData() {
        // Validate form
        if (!this.dataForm.valid) return;

        Object.assign(this.dataModel, this.dataForm.value);
        this.configs.forEach((config) => {
            config.value = this.dataModel[config.name];
        });
        // Update
        this.coreConfigApiService.updates(this.configs).subscribe((res) => this.saveDataCompleted(res));
    }

    private saveDataCompleted(res: ApiResult) {
        this.toast.apiResult(res);
        if (res.status == Constants.ResultStatus.Success)
            this.loadData();
    }

    private loadData() {
        this.coreConfigApiService.getAll().pipe(
            withLatestFrom(
                this.coreConfigApiService.getConfigGroup()
            )
        ).subscribe(([configs, configGroups]) => {
            this.configs = _.sortBy(configs, ['sort'], ['asc']);
            this.configGroups = _.sortBy(configGroups, ['sort'], ['asc']);
            this.createForm();
        });
    }
}
