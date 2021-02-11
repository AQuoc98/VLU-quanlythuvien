import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule , ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {SortableModule} from 'ngx-bootstrap/sortable';

import { PolicyComponent } from './policy.component';
import { PolicyAddComponent } from './policy-add.component';
import { PolicyRoutingModule } from './policy-routing.module';
import {NgSelectModule} from '@ng-select/ng-select';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
@NgModule({
  declarations: [
    PolicyComponent,
    PolicyAddComponent
  ],
  imports: [
    PolicyRoutingModule,
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

export class PolicyModule { }
