import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule , ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {SortableModule} from 'ngx-bootstrap/sortable';

import {RackComponent} from './rack.component';
import {RackAddComponent} from './rack-add.component';
import {RackRoutingModule} from './rack-routing.module';
import {NgSelectModule} from '@ng-select/ng-select';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
@NgModule({
  declarations: [
    RackComponent,
    RackAddComponent
  ],
  imports: [
    RackRoutingModule,
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
export class RackModule { }
