import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ValidationErrors } from '@angular/forms';
import { Subject, forkJoin } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
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
  selector: 'app-profile-edit',
  templateUrl: 'profile-edit.component.html'
})
export class ProfileEditComponent implements OnInit {

  //#region Fields
  dataForm: FormGroup;
  dataModel: any = {};
  loginInfoModel: any = {};
  isSubmited: boolean = false;
  loginInfoForm: FormGroup;
  roles: any = [];
  userRoles: any = [];
  private isFormValid: boolean = false;
  private isLoginInfoFormValid: boolean = false;
  private destroyed$ = new Subject();
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
    var roles = this.corePermissionApiService.getRoleByCurrentUser();

    forkJoin([roles]).subscribe(res => {
      this.roles = res[0];

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
      // secret: [''],
      newSecret: [''],
      cnewSecret: [''],
      credentialTypeId: [Constants.LoginType.Email],
      userId: ['']
    },
      // { validator: [this.checkConfirmPassword, this.checkNewPassword] }
      { validator: [this.checkConfirmPassword] }
    );

    this.loginInfoForm.valueChanges
      .pipe(
        // To manage unsubscribe
        takeUntil(this.destroyed$)
      )
      .subscribe((v) => {
        this.isLoginInfoFormValid = this.loginInfoForm.valid;
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
    const roleSelecteds = _.filter(this.userRoles, (item) => item.isSelected === true);
    this.dataModel.coreUserRoles = _.map(roleSelecteds, (item) => {
      return { roleId: item.roleId, userId: this.fControls.id.value };
    });
  }

  private saveData() {
    // Submited
    this.isSubmited = true;
    // Validate form
    if (!this.isFormValid || !this.isLoginInfoFormValid) return;

    this.loginInfoForm.controls.userId.setValue(this.fControls.id.value);
    Object.assign(this.loginInfoModel, this.loginInfoForm.value);

    Object.assign(this.dataModel, this.dataForm.value);
    this.dataModel.coreCredentials = [this.loginInfoModel];
    this.beforeSaveData();
    // Update
    this.coreUserApiService.EditProfile(this.dataModel).subscribe((res) => this.saveDataCompleted(res));

  }

  private saveDataCompleted(res: ApiResult) {
    this.toast.apiResult(res);
    if (res.status === Constants.ResultStatus.Success) {
      this.loadData(res.recordId);
    } else {
      return;
    }
  }

  private loadData(id: string) {
    this.coreUserApiService.getById(id).subscribe((res) => {
      Object.assign(this.dataModel, res);
      FormHelper.setValue(this.dataForm, res);
      Object.assign(this.loginInfoModel, res.coreCredentials[0]);
      this.loginInfoModel.secret = '';
      this.loginInfoModel.newSecret = '';
      FormHelper.setValue(this.loginInfoForm, this.loginInfoModel);
      this.loginInfoForm.controls.cnewSecret.setValue('');
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
    console.log("userRoles",this.userRoles)
  }

  private checkConfirmPassword(group: FormGroup): ValidationErrors {
    let pass = group.controls.newSecret.value;
    let cpass = group.controls.cnewSecret.value;
    if (pass === cpass) {
      return null;
    }
    group.controls.cnewSecret.setErrors({ cnewSecret: true });
    return { cnewSecret: true };
  }
  // private checkNewPassword(group: FormGroup): ValidationErrors {
  //   let pass = group.controls.secret.value;
  //   let npass = group.controls.newSecret.value;
  //   if (pass !== '' && npass === '') {
  //     group.controls.newSecret.setErrors({ newSecret: true });
  //     return { newSecret: true };
  //   }
  //   return null;
  // }
  // #endregion

}
