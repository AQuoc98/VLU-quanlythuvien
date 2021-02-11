import { BookAddComponent } from './book-add.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SortableModule } from 'ngx-bootstrap/sortable';
import { BookComponent } from './book.component';
import { BookRoutingModule } from './book-routing.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
import { AngularFileUploaderModule } from "angular-file-uploader";
import {FileUploadModule} from "ng2-file-upload"



@NgModule({
  declarations: [
    BookComponent,
    BookAddComponent,
  ],
  imports: [
    BookRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    SortableModule,
    NgSelectModule,
    GridViewModule,
    ToolBarModule,
    ConfirmDialogModule,
    AngularFileUploaderModule,
    FileUploadModule
  ]
})
export class BookModule { }
