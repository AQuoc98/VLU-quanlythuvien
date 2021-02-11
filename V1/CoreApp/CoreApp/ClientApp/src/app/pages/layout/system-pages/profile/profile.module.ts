import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SortableModule } from 'ngx-bootstrap/sortable';


import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
import { CorePermissionApiService, CoreUserApiService } from '@infrastructure/api-services';
import { ProfileEditComponent } from './profile-edit.component';
import { ProfileRoutingModule } from './profile-routing.module';

@NgModule({
  imports: [
    ProfileRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    GridViewModule,
    ToolBarModule,
    ConfirmDialogModule,
    TranslateModule,
    CommonModule,
    SortableModule
  ],
  declarations: [
    ProfileEditComponent
  ]
})
export class ProfileModule { }
