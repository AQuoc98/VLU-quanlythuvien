import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { Subject, forkJoin } from 'rxjs';
import * as _ from 'lodash';
import { CorePolicyApiServices, CoreMemberGroupApiService } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { FormHelper } from '@infrastructure/helpers';
import { Toast } from '@core/services';
import { Constants } from '@core/constants';
import { Guid } from '@infrastructure/common';
import { ApiResult } from '@core/models';

@Component({
    selector: 'app-policy-add',
    templateUrl: 'policy-add.component.html'
})
export class PolicyAddComponent implements OnInit {
    dataForm: FormGroup;
    dataModel: any = {};
    isSubmited: boolean = false;
    membergroups: any = [];

    private isFormValid: boolean = false;

    private destroyed$ = new Subject();
    isEdit: boolean = false

    constructor(private formBuilder: FormBuilder,
        private corePolicyApiServices: CorePolicyApiServices,
        private coreMemberGroupApiService: CoreMemberGroupApiService,
        private confirmDialogService: ConfirmDialogService,
        private router: ActivatedRoute,
        private toast: Toast
    ) {
        this.createForm();
    }
    ngOnInit() {
        this.readParams();
        this.loadRefData();
    }
    ngOnDestroy() {
        this.destroyed$.next();
        this.destroyed$.complete();
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

    get fControls() {
        return this.dataForm.controls;
    }
    private readParams() {
        this.router.params
          .pipe(
            takeUntil(this.destroyed$)
          )
          .subscribe(params => {
            const id: string = params['id'];
            this.dataForm.addControl('id', new FormControl(id));
            if (id != Guid.empty) {
              this.loadDataEdit(id);
            } else {
              this.loadData()
            }
          });
      }

    private loadRefData() {
        const membergroup = this.coreMemberGroupApiService.getAll();
        forkJoin([membergroup]).subscribe(res => {
            this.membergroups = res[0];
        });
    }

    createForm() {
        this.dataForm = this.formBuilder.group({
            memberGroupId: ['',Validators.required],
            bookNumber: [Number],
            numberOfDueDate: [Number]
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
    private saveData() {
        // Submited
        this.isSubmited = true;
        // Validate form
        if (!this.isFormValid) return;
        Object.assign(this.dataModel, this.dataForm.value);
        // Check add or update event
        if (this.fControls.id.value == Guid.empty) {
            // Add
            this.corePolicyApiServices.Insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
        } else {
            // Update
            this.corePolicyApiServices.Update(this.dataModel).subscribe((res) => this.saveDataCompletedEdit(res));
        }
    }

    private saveDataCompleted(res: ApiResult) {
        this.toast.apiResult(res);
        res.recordId = "00000000-0000-0000-0000-000000000000";
        if (res.status == Constants.ResultStatus.Success) {
            this.loadData();

        }
    }
    private saveDataCompletedEdit(res: ApiResult) {
        this.toast.apiResult(res)
        if (res.status == Constants.ResultStatus.Success) this.loadDataEdit(res.recordId)
    }

    private loadData() {
        Object.assign(this.dataForm, this.dataForm.value);
    }
    private loadDataEdit(id: string) {
        this.corePolicyApiServices.getById(id).subscribe((res) => {
            this.isEdit = true;
            FormHelper.setValue(this.dataForm, res);

        });
    }
}
