import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { Subject, forkJoin } from 'rxjs';
import { FileUploader } from 'ng2-file-upload';
import * as _ from 'lodash';
import { CoreMemberApiServices, CoreMemberGroupApiService } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { FormHelper } from '@infrastructure/helpers';
import { Toast } from '@core/services';
import { Constants } from '@core/constants';
import { Guid } from '@infrastructure/common';
import { ApiResult } from '@core/models';
import { AuthService } from '@infrastructure/auth';

@Component({
  selector: 'app-member-add',
  templateUrl: 'member-add.component.html',
  styleUrls: ['./member-management.component.scss']
})

export class MemberAddComponent implements OnInit {
  dataForm: FormGroup;
  dataModel: any = {};
  isSubmited: boolean = false;
  isEdit: boolean = false;
  membergroups: any = [];
  uploader: FileUploader;
  imageUrl: string

  private uploaderOptions = {
    url: this.coreMemberApiServices.UploadImageMember,
    itemAlias: 'file',
    autoUpload: true,
    authToken: `Bearer ${this.authService.token}`,
    removeAfterUpload: true,
    maxFileSize: 2000000, // 2mb
    allowedMimeType: ['image/png', 'image/jpg', 'image/jpeg', 'image/svg+xml']
  };
  private isFormValid: boolean = false;
  private destroyed$ = new Subject();

  constructor(private formBuilder: FormBuilder,
    private coreMemberApiServices: CoreMemberApiServices,
    private coreMemberGroupApiService: CoreMemberGroupApiService,
    private confirmDialogService: ConfirmDialogService,
    private router: ActivatedRoute,
    private toast: Toast,
    private authService: AuthService,
  ) {
    this.uploader = new FileUploader(this.uploaderOptions);
    this.createForm();
  }

  ngOnInit() {
    this.readParams();
    this.loadRefData();
    this.uploadFileEvents();
  }

  ngOnDestroy() {
    this.destroyed$.next();
    this.destroyed$.complete();
  }

  saveEvent() {
    this.confirmDialogService.confirm('CONFIRM_DIALOG.CONFIRM_SAVE_MESSAGE').then(
      () => {
        // Accept
        this.saveData();
      },
      () => {
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
      memberCode: [''],
      name: ['', Validators.required],
      phone: ['', Validators.required],
      gender: [''],
      memberGroupId: [''],
      image: [''],
      address: [''],
    });

    this.dataForm.valueChanges
      .pipe(
        // To manage unsubscribe
        takeUntil(this.destroyed$)
      )
      .subscribe(() => {
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
      this.coreMemberApiServices.Insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
    } else {
      // Update
      this.coreMemberApiServices.Update(this.dataModel).subscribe((res) =>
        this.saveDataCompletedEdit(res))
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
    Object.assign(this.dataForm, this.dataForm.value)
  }
  private loadDataEdit(id: string) {
    this.coreMemberApiServices.getById(id).subscribe((res) => {
      this.isEdit = true;
      FormHelper.setValue(this.dataForm, res);
      this.imageUrl = res.image
    });
  }


  private uploadFileEvents() {
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };
    this.uploader.onCompleteItem = (item: any, response: any, status: any, headers: any) => {
      const fileUrl: string = response || "";
      this.imageUrl = fileUrl
      this.dataForm.controls.image.setValue(this.imageUrl);
    };
    this.uploader.onWhenAddingFileFailed = (file: any, filter: any) => {
      switch (filter.name) {
        case 'fileSize':
          // Error file size
          break;
        case 'fileType' || 'mimeType':
          // Error file type
          break;
        default:
          // Error
          break;
      }

    };
    this.uploader.onProgressItem = (fileItem, progress) => {
      console.log(progress)
    };
    this.uploader.onCompleteAll = () => {
      // On complete all
    }
  }

}
