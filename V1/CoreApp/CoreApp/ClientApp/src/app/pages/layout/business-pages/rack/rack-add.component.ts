import { Validator } from './../../../../infrastructure/models/field-config.model';
import { Component, OnInit, NgModule } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import * as _ from 'lodash';

import { CoreRackApiServices } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { FormHelper } from '@infrastructure/helpers';
import { Toast } from '@core/services';
import { Constants } from '@core/constants';
import { Guid } from '@infrastructure/common';
import { ApiResult } from '@core/models';


@Component({
  selector: 'app-rack-add',
  templateUrl: 'rack-add.component.html'
})

export class RackAddComponent implements OnInit {
  dataForm: FormGroup;
  dataModel: any = {};
  isSubmited: boolean = false;

  private isFormValid: boolean = false;

  private destroyed$ = new Subject();
  isEdit: boolean = false

  constructor(private formBuilder: FormBuilder,
    private coreRackApiService: CoreRackApiServices,
    private confirmDialogService: ConfirmDialogService,
    private router: ActivatedRoute,
    private toast: Toast) {
    this.createForm();
  }



  ngOnInit() {
    this.readParams();
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


  createForm() {
    this.dataForm = this.formBuilder.group({
      number: ['', [Validators.required, Validators.max(999999999)]],
      locationIndentifier: ['', Validators.maxLength(10)]
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
      this.coreRackApiService.insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
    } else {
      // Update
      this.coreRackApiService.update(this.dataModel).subscribe((res) => this.saveDataCompletedEdit(res))
    }
  }

  private saveDataCompleted(res: ApiResult) {
    this.toast.apiResult(res);
    res.recordId = "00000000-0000-0000-0000-000000000000"
    if (res.status == Constants.ResultStatus.Success) {
      this.loadData()

    }
  }

  private saveDataCompletedEdit(res: ApiResult) {
    this.toast.apiResult(res)
    if (res.status == Constants.ResultStatus.Success) this.loadDataEdit(res.recordId)
  }

  private loadData() {
    this.dataForm.controls.number.setValue("");
    this.dataForm.controls.locationIndentifier.setValue("");
    Object.assign(this.dataForm, this.dataForm.value)

  }

  private loadDataEdit(id: string) {
    this.isEdit = true;
    this.coreRackApiService.getById(id).subscribe((res) => {
      Object.assign(this.dataModel, res);
      FormHelper.setValue(this.dataForm, res);
    });
  }

}
