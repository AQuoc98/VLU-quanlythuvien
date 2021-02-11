import { FormatAddComponent } from './format-add.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SortableModule } from 'ngx-bootstrap/sortable';
import { FormatComponent } from './format.component';
import { FormatRoutingModule } from './format-routing.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
@NgModule({
  declarations: [
    FormatComponent,
    FormatAddComponent
  ],
  imports: [
    FormatRoutingModule,
    CommonModule,
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
export class FormatModule { }
