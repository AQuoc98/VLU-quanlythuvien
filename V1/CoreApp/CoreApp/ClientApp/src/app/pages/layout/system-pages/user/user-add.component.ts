import { UserRoles } from './userRoles';
import { Component, OnInit, NgModule } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ValidationErrors } from '@angular/forms';
import { Subject, forkJoin } from 'rxjs';
import { takeUntil, filter } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import * as _ from 'lodash';

import { CoreUserApiService, CorePermissionApiService } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { FormHelper } from '@infrastructure/helpers';
import { Toast } from '@core/services';
import { Constants } from '@core/constants';
import { Guid } from '@infrastructure/common';
import { ApiResult } from '@core/models';

@Component({
  selector: 'app-user-add',
  templateUrl: 'user-add.component.html'
})
export class UserAddComponent implements OnInit {
  radioValue: string;
  //#region Fields
  dataForm: FormGroup;
  dataModel: any = {};
  loginInfoModel: any = {};
  isSubmited: boolean = false;
  loginInfoForm: FormGroup;
  roles: any = [];
  schools: any = [];
  userRoles: any = [];
  private isFormValid: boolean = false;
  private isLoginInfoFormValid: boolean = false;
  private destroyed$ = new Subject();
  isEdit: boolean = false
  selectedUsserroles: UserRoles[] = [];
  //#endregion

  constructor(private formBuilder: FormBuilder,
    private coreUserApiService: CoreUserApiService,
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

  get fLoginInfoControls() {
    return this.loginInfoForm.controls;
  }
  //#endregion

  //#region Private Functions
  private loadRefData() {
    const roles = this.corePermissionApiService.getRoleByCurrentUser();
    forkJoin([roles]).subscribe(([roles]) => {
      this.roles = roles;
      this.buildUserRoleData();
    });
  }

  private createForm() {
    this.dataForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      gender: [''],
    });

    this.dataForm.valueChanges
      .pipe(
        // To manage unsubscribe
        takeUntil(this.destroyed$)
      )
      .subscribe((v) => {
        this.isFormValid = this.dataForm.valid;
      });

    this.loginInfoForm = this.formBuilder.group({
      identifier: ['', Validators.required],
      secret: [''],
      credentialTypeId: [Constants.LoginType.Email],
      userId: [''],
      newSecret: [''],
      cnewSecret: [''],
    },
      { validator: [this.checkConfirmPassword] }
    );

    this.loginInfoForm.valueChanges
      .pipe(
        // To manage unsubscribe
        takeUntil(this.destroyed$)
      )
      .subscribe((v) => {
        if (!this.isEdit) {
          this.isLoginInfoFormValid = this.loginInfoForm.valid;
        }
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
        if (id != Guid.empty) {
          this.loadDataEdit(id);
        } else {
          this.loadData()
        }
      });
  }

  private beforeSaveData() {
    const roleSelected = this.selectedUsserroles
    this.dataModel.coreUserRoles = _.map(roleSelected, (item) => {
      return { roleId: item.roleId, userId: this.fControls.id.value };
    });
  }

  private saveData() {
    // Submited
    this.isSubmited = true;
    // Validate form
    if (this.isEdit) {
      let pass = this.loginInfoForm.controls.newSecret.value;
      let cpass = this.loginInfoForm.controls.cnewSecret.value;
      if (pass !== cpass) {
        this.loginInfoForm.controls.cnewSecret.setErrors({ cnewSecret: true });
        return { cnewSecret: true };
      }
      this.isLoginInfoFormValid = true;

    }
    if (!this.isFormValid || !this.isLoginInfoFormValid) return;
    this.loginInfoForm.controls.userId.setValue(this.fControls.id.value);
    Object.assign(this.loginInfoModel, this.loginInfoForm.value);
    Object.assign(this.dataModel, this.dataForm.value);
    this.dataModel.coreCredentials = [this.loginInfoModel];
    this.beforeSaveData();
    // Check add or update event
    if (this.fControls.id.value == Guid.empty) {
      // Add
      this.coreUserApiService.insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
    } else {
      // Update
      this.coreUserApiService.EditProfile(this.dataModel).subscribe((res) => this.saveDataCompletedEdit(res));
    }
  }

  private saveDataCompleted(res: ApiResult) {
    this.toast.apiResult(res);
    res.recordId = "00000000-0000-0000-0000-000000000000"
    if (res.status == Constants.ResultStatus.Success) {
      this.loadData()

    }
    // if (res.status == Constants.ResultStatus.Success)
    // this.loadData(res.recordId);
  }

  private saveDataCompletedEdit(res: ApiResult) {
    this.toast.apiResult(res);
    if (res.status == Constants.ResultStatus.Success)
      this.loadDataEdit(res.recordId);
  }


  private loadData() {
    // this.dataForm.controls.address.setValue("");
    // this.dataForm.controls.email.setValue("");
    // this.dataForm.controls.gender.setValue("");
    // this.dataForm.controls.name.setValue("");
    // this.dataForm.controls.phone.setValue("");
    Object.assign(this.dataForm, this.dataForm.value)
    // this.loginInfoForm.controls.identifier.setValue("")
    this.loginInfoForm.controls.secret.setValue("")
    Object.assign(this.loginInfoForm, this.loginInfoModel.value)

  }

  private loadDataEdit(id: string) {
    this.coreUserApiService.getById(id).subscribe((res) => {
      Object.assign(this.dataModel, res);
      FormHelper.setValue(this.dataForm, res);
      Object.assign(this.loginInfoModel, res.coreCredentials[0]);
      this.loginInfoModel.secret = '';
      this.loginInfoModel.newSecret = '';
      this.loginInfoModel.cnewSecret = '';
      FormHelper.setValue(this.loginInfoForm, this.loginInfoModel);
      this.isEdit = true;
      this.buildUserRoleData();
    });
  }

  private buildUserRoleData() {
    let userRoles = [];
    this.roles.forEach(item => {
      const isRoleExisted = _.find(this.dataModel.coreUserRoles, (ur) => ur.roleId === item.id);
      let userRoleItem = {
        roleId: item.id,
        isSelected: isRoleExisted !== undefined,
        name: item.name
      }
      userRoles.push(userRoleItem);
    });
    this.userRoles = userRoles;

    if (!this.isEdit) {
      this.radioValue = this.userRoles[0].name;
      this.selectedUsserroles.splice(0, 1, this.userRoles[0]);
    }
    if (this.isEdit) {
      const userRolesArr = this.userRoles.filter((item) => item.isSelected == true)
      userRolesArr ? this.selectedUsserroles = userRolesArr : ""
      this.radioValue = userRolesArr[0].name
    }
  }

  getRadio(value) {
    value.isSelected = true;
    this.selectedUsserroles.splice(0, 1, value);
  }

  private checkConfirmPassword(group: FormGroup): ValidationErrors {
    let pass = group.controls.newSecret.value;
    let cpass = group.controls.cnewSecret.value;
    if (pass === cpass) {
      return null;
    }
    // group.controls.cnewSecret.setErrors({ cnewSecret: true });

    return { cnewSecret: true };
  }

}
