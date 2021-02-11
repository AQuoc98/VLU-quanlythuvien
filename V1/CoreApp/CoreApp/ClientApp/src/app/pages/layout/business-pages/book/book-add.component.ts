import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { Subject, forkJoin } from 'rxjs';
import { FileUploader } from 'ng2-file-upload';
import * as _ from 'lodash';
import { CoreBookApiServices, CoreCatalogApiServices, CorePublisherApiService, CoreAuthorApiServices, CoreLanguageApiServices } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { FormHelper } from '@infrastructure/helpers';
import { Toast } from '@core/services';
import { Constants } from '@core/constants';
import { Guid } from '@infrastructure/common';
import { ApiResult } from '@core/models';
import { AuthService } from '@infrastructure/auth';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-book-add',
  templateUrl: 'book-add.component.html',
  styleUrls: ['./book.component.scss']
})

export class BookAddComponent implements OnInit {
  dataForm: FormGroup;
  dataModel: any = {};
  isSubmited: boolean = false;
  isEdit: boolean = false;
  catalogs: any = [];
  publishers: any = [];
  authors: any = [];
  languages: any = [];

  disabled: boolean;
  uploader: FileUploader;

  imageUrl: string


  private uploaderOptions = {
    url: this.coreBookApiServices.uploadFileApi,
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
    private coreBookApiServices: CoreBookApiServices,
    private confirmDialogService: ConfirmDialogService,
    private catalogApiServices: CoreCatalogApiServices,
    private publisherApiServices: CorePublisherApiService,
    private authorApiServices: CoreAuthorApiServices,
    private languageApiServices: CoreLanguageApiServices,
    private router: ActivatedRoute,
    private toast: Toast,
    private authService: AuthService,
    private sanitizer: DomSanitizer
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
    const catalog = this.catalogApiServices.getAll();
    const publisher = this.publisherApiServices.getAll()
    const author = this.authorApiServices.getAll()
    const language = this.languageApiServices.getAll()
    forkJoin([catalog, publisher, author, language]).subscribe(res => {
      this.catalogs = res[0];
      this.publishers = res[1];
      this.authors = res[2];
      this.languages = res[3]
    });
  }

  createForm() {
    this.dataForm = this.formBuilder.group({
      isbn: ['', [Validators.pattern, Validators.required]],
      title: ['', [Validators.required, Validators.maxLength(500)]],
      subject: ['', Validators.maxLength(1500)],
      numberOfPages: [Number, Validators.max(99999999)],
      catalogId: [''],
      publishierId: [''],
      authorId: [''],
      languageId: [''],
      image: ['']
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
      this.coreBookApiServices.insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
    } else {
      // Update
      this.coreBookApiServices.UpdateBook(this.dataModel).subscribe((res) =>
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
    this.coreBookApiServices.getById(id).subscribe((res) => {
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
