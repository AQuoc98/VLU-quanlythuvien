import { StatusAddComponent } from './status-add.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule , ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {SortableModule} from 'ngx-bootstrap/sortable';

import {StatusComponent} from './status.component';
import {StatusRoutingModule} from './status-routing.module';
import {NgSelectModule} from '@ng-select/ng-select';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
@NgModule({
  declarations: [
    StatusComponent,
    StatusAddComponent
  ],
  imports: [
    StatusRoutingModule,
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

export class StatusModule { }
