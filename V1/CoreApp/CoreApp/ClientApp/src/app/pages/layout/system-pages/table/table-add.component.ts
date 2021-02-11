import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Subject, forkJoin } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import * as _ from 'lodash';

import { CoreModuleApiService, CoreTableApiServcice, CoreEnumApiService } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { FormHelper } from '@infrastructure/helpers';
import { DataModel } from '@infrastructure/models';
import { Toast } from '@core/services';
import { ApiResult } from '@core/models';
import { Constants } from '@core/constants';
import { Guid } from '@infrastructure/common';

@Component({
    selector: 'app-table-add',
    templateUrl: 'table-add.component.html'
})
export class TableAddComponent implements OnInit {

    //#region Fields
    modules: any = [];
    dataTypes: any = [];
    dataForm: FormGroup;
    columnForm: FormGroup;
    dataModel: DataModel = new DataModel();
    columns: DataModel[] = [];
    isSubmited: boolean = false;
    isColumnFormSubmited: boolean = false;
    enums: any = [];
    tables: any = [];
    foreignColumns: any = [];
    private isFormValid: boolean = false;
    private destroyed$ = new Subject();
    //#endregion

    constructor(private formBuilder: FormBuilder,
        private coreModuleApiService: CoreModuleApiService,
        private coreTableApiService: CoreTableApiServcice,
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

    saveColumn() {
        // Submited
        this.isColumnFormSubmited = true;

        // Validate form
        const sqlNameExisted = this.checkSqlNameExisted();
        if (!this.columnForm.valid) return;

        // Check add or update event
        if (!sqlNameExisted) {
            // Add
            let column = new DataModel();
            this.columnForm.controls.tableId.setValue(this.fControls.id.value);
            Object.assign(column, this.columnForm.value);
            this.columns.push(column);
        } else {
            // Update
            const updateIndex = _.findIndex(this.columns, (item: any) => {
                return item.sqlName == this.columnForm.controls.sqlName.value;
            })
            Object.assign(this.columns[updateIndex], this.columnForm.value);
        }

        // Reset form
        this.isColumnFormSubmited = false;
        this.columnForm.reset({
            id: Guid.empty,
            searchable: false,
            sortable: false,
            isPrimaryKey: false,
            isForeignKey: false
        });
    }

    editColumn(column: DataModel) {
        FormHelper.setValue(this.columnForm, column);
    }

    clearColumn(index: number) {
        this.columns.splice(index, 1);
    }

    dataTypeName(id: string): string {
        const dataType = _.find(this.dataTypes, (item: any) => {
            return item.id == id;
        });
        if (!dataType)
            return '';
        return dataType.name;
    }

    tableChanged() {
        this.loadTableDetail();
    }

    foreignColumnValueChanged() {
        this.buildSqlScript();
    }

    foreignColumnDisplayChanged() {
        this.buildSqlScript();
    }

    get fControls() {
        return this.dataForm.controls;
    }

    get fColumnControls() {
        return this.columnForm.controls;
    }
    //#endregion

    //#region Private Functions
    private loadRefData() {
        const module = this.coreModuleApiService.getAll();
        const dataType = this.coreTableApiService.getDataType();
        const enums = this.coreEnumApiService.getAll();
        const tables = this.coreTableApiService.getAll();

        forkJoin([module, dataType, enums, tables]).subscribe(res => {
            this.modules = res[0];
            this.dataTypes = res[1];
            this.enums = res[2];
            this.tables = res[3];
        });
    }

    private createForm() {
        this.dataForm = this.formBuilder.group({
            sqlName: ['', Validators.required],
            name: ['', Validators.required],
            alias: ['', Validators.required],
            moduleId: ['', Validators.required]
        });

        this.dataForm.valueChanges
            .pipe(
                // To manage unsubscribe
                takeUntil(this.destroyed$)
            )
            .subscribe((v) => {
                this.isFormValid = this.dataForm.valid;
            });

        this.columnForm = this.formBuilder.group({
            id: [Guid.empty],
            sqlName: ['', Validators.required],
            nameDict: ['', Validators.required],
            name: ['', Validators.required],
            tableAlias: ['', Validators.required],
            dataTypeId: ['', Validators.required],
            tableId: [''],
            searchable: [false],
            sortable: [false],
            isPrimaryKey: [false],
            isForeignKey: [false],
            sqlScript: [''],
            enumId: [''],
            foreignTableId: [''],
            foreignColumnValue: [''],
            foreignColumnDisplay: ['']
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
        this.dataModel['coreColumns'] = this.columns;
        if (this.fControls.id.value == Guid.empty) {
            // Add
            this.coreTableApiService.insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        } else {
            // Update
            this.coreTableApiService.update(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        }
    }

    private saveDataCompleted(res: ApiResult) {
        this.toast.apiResult(res);
        if (res.status == Constants.ResultStatus.Success)
            this.loadData(res.recordId);
    }

    private loadData(id: string) {
        this.coreTableApiService.getById(id).subscribe((res) => {
            Object.assign(this.dataModel, res);
            this.columns = res.coreColumns;
            FormHelper.setValue(this.dataForm, res);
        })
    }

    private checkSqlNameExisted(): boolean {
        const sqlName = this.columnForm.controls.sqlName.value;
        const alias = this.columnForm.controls.tableAlias.value;
        const checkExsited = _.filter(this.columns, (item: any) => {
            return `${item.tableAlias}.${item.sqlName}` == `${alias}.${sqlName}`;
        });
        if (checkExsited.length > 0) {
            return true;
        }
        return false;
    }

    private loadTableDetail() {
        this.coreTableApiService.getById(this.fColumnControls.foreignTableId.value).subscribe((res) => {
            this.foreignColumns = res.coreColumns;
        });
    }

    private buildSqlScript() {
        const tableSelected = _.find(this.tables, item => item.id == this.fColumnControls.foreignTableId.value);
        const tableAlias = tableSelected.alias;
        const columnValue = this.fColumnControls.foreignColumnValue.value;
        const columnDisplay = this.fColumnControls.foreignColumnDisplay.value;

        let sqlScript = `SELECT ${tableAlias}.${columnValue} AS Id, ${tableAlias}.${columnDisplay} as Name FROM ${tableSelected.sqlName} ${tableAlias}`;
        this.fColumnControls.sqlScript.setValue(sqlScript);
    }
    //#endregion

}
