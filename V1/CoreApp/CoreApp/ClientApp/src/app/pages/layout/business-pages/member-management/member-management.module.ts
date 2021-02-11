import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SortableModule } from 'ngx-bootstrap/sortable';
import { MemberManagementComponent } from './member-management.component';
import { MemberAddComponent } from './member-add.component';
import { MemberManagementRoutingModule } from './member-management-routing.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
import { AngularFileUploaderModule } from "angular-file-uploader";
import {FileUploadModule} from "ng2-file-upload"

@NgModule({
  declarations: [
    MemberManagementComponent,
    MemberAddComponent
  ],
  imports: [
    MemberManagementRoutingModule,
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

export class MemberManagementModule { }
