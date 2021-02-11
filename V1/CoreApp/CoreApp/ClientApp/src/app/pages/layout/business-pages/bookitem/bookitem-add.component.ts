import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { Subject, forkJoin } from 'rxjs';
import * as _ from 'lodash';
import { CoreBookItemApiServices, CoreRackApiServices, CoreFormatApiServices, CoreBookApiServices, CoreCatalogApiServices, CorePublisherApiService, CoreAuthorApiServices, CoreLanguageApiServices, CoreStatusApiServices } from '@infrastructure/api-services';
import { ConfirmDialogService } from '@shared/components';
import { Toast } from '@core/services';
import { Constants } from '@core/constants';
import { Guid } from '@infrastructure/common';
import { ApiResult } from '@core/models';
import { FormHelper } from '@infrastructure/helpers';

@Component({
  selector: 'app-bookitem-add',
  templateUrl: 'bookitem-add.component.html',
  styleUrls: ['./bookitem.component.scss']
})

export class BookItemAddComponent implements OnInit {
  dataForm: FormGroup;
  dataModel: any = {};
  isSubmited: boolean = false;
  isEdit: boolean = false;
  racks: any = [];
  formats: any = [];
  books: any = [];
  status: any = [];
  price: any;
  private isFormValid: boolean = false;
  private destroyed$ = new Subject();
  isShowBookInformation: boolean = false;

  bookIsbn: string = ""
  bookName: string = ""
  bookSubject: string = ""
  bookPublisher: string = ""
  bookAuthor: string = ""
  bookCatalog: string = ""
  bookLanguage: string = ""
  bookNumberPage: number;
  bookImage: string = "";


  constructor(private formBuilder: FormBuilder,
    private coreBookItemApiServices: CoreBookItemApiServices,
    private coreRackApiServices: CoreRackApiServices,
    private coreFormatApiServices: CoreFormatApiServices,
    private coreBookApiServices: CoreBookApiServices,
    private confirmDialogService: ConfirmDialogService,
    private router: ActivatedRoute,
    private catalogApiServices: CoreCatalogApiServices,
    private publisherApiServices: CorePublisherApiService,
    private authorApiServices: CoreAuthorApiServices,
    private languageApiServices: CoreLanguageApiServices,
    private coreStatusApiServices: CoreStatusApiServices,
    private toast: Toast) {
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
    const rack = this.coreRackApiServices.getAll();
    const format = this.coreFormatApiServices.getAll();
    const book = this.coreBookApiServices.getAll();
    const status = this.coreStatusApiServices.getAll();
    forkJoin([rack, format, book,status]).subscribe(res => {
      this.racks = res[0];
      this.formats = res[1];
      this.books = res[2];
      this.status = res[3]
    });
  }

  createForm() {
    this.dataForm = this.formBuilder.group({
      barcode: ['',Validators.maxLength(50)],
      isReferenceOnly: [false],
      isRareBook: [false],
      price: ['', Validators.max(999999999)],
      statusId: [''],
      publicationYear: [''],
      rackId: [''],
      formatId: [''],
      bookId: ['']


    });

    this.dataForm.valueChanges.pipe(takeUntil(this.destroyed$)).subscribe(() => { this.isFormValid = this.dataForm.valid })

  }

  private saveData() {
    this.isSubmited = true;
    // Validate form
    if (!this.isFormValid) return;
    Object.assign(this.dataModel, this.dataForm.value);
    // Check add or update event
    if (this.fControls.id.value == Guid.empty) {
      // Add
      this.coreBookItemApiServices.insert(this.dataModel).subscribe((res) => this.saveDataCompleted(res));
    } else {
      this.coreBookItemApiServices.update(this.dataModel).subscribe((res) =>
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
    this.coreBookItemApiServices.getById(id).subscribe((res) => {
      this.isEdit = true;
      this.isShowBookInformation = true;
      this.bookIsbn = res.book.isbn
      this.bookName = res.book.title
      this.bookSubject = res.book.subject;
      this.publisherApiServices.getById(res.book.publishierId).subscribe((res) => this.bookPublisher = res.name)
      this.authorApiServices.getById(res.book.authorId).subscribe((res) => this.bookAuthor = res.name)
      this.catalogApiServices.getById(res.book.catalogId).subscribe((res) => this.bookCatalog = res.name)
      this.languageApiServices.getById(res.book.languageId).subscribe((res) => this.bookLanguage = res.name)
      this.bookNumberPage = res.book.numberOfPages
      this.bookImage = res.book.image
      FormHelper.setValue(this.dataForm, res);
    });

  }

  handleFunction(event) {
    this.isShowBookInformation = true
    var bookArr = this.books.filter(item => item.id == event.target.value.slice(3))
    this.bookIsbn = bookArr[0].isbn
    this.bookName = bookArr[0].title
    this.bookNumberPage = bookArr[0].numberOfPages
    this.bookImage = bookArr[0].image
    this.bookSubject = bookArr[0].subject
    this.authorApiServices.getById(bookArr[0].authorId).subscribe((res) => this.bookAuthor = res.name)
    this.catalogApiServices.getById(bookArr[0].catalogId).subscribe((res) => this.bookCatalog = res.name)
    this.publisherApiServices.getById(bookArr[0].publishierId).subscribe((res) => this.bookPublisher = res.name)
    this.languageApiServices.getById(bookArr[0].languageId).subscribe((res) => this.bookLanguage = res.name)





  }






}
