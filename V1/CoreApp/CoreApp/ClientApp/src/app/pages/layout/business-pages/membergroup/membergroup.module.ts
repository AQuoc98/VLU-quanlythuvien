    
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule , ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {SortableModule} from 'ngx-bootstrap/sortable';

import {MembergroupComponent} from './membergroup.component';
import { MemberGroupAddComponent } from './membergroup-add.component';
import {MemberGroupRoutingModule} from './membergroup-routing.module';
import {NgSelectModule} from '@ng-select/ng-select';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
@NgModule({
  declarations: [
    MembergroupComponent,
    MemberGroupAddComponent
  ],
  imports: [
    MemberGroupRoutingModule,
    CommonModule ,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    SortableModule,
    NgSelectModule,
    GridViewModule,
    ToolBarModule,
    ConfirmDialogModule,
  ]
})
export class MemberGroupModule { }
