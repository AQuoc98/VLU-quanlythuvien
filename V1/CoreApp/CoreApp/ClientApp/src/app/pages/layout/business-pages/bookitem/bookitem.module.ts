import { ImportExcelComponent } from './../../../../shared/components/import-excel/import-excel.component';
import { ImportExcelModule } from './../../../../shared/components/import-excel/import-excel.module';
import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SortableModule } from 'ngx-bootstrap/sortable';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { CurrencyPipe } from '@angular/common';
import { BookItemComponent } from './bookitem.component';
import { BookItemAddComponent } from './bookitem-add.component';
import { BookItemRoutingModule } from './bookitem-routing.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components'
import { NgxPaginationModule } from 'ngx-pagination';




@NgModule({
  declarations: [
    BookItemComponent,
    BookItemAddComponent,
    ImportExcelComponent
  ],
  imports: [
    BsDatepickerModule.forRoot(),
    BookItemRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    SortableModule,
    NgSelectModule,
    GridViewModule,
    ToolBarModule,
    ConfirmDialogModule,
    NgxPaginationModule,
    ImportExcelModule
  ],
  providers: [CurrencyPipe],
})

export class BookItemModule { }
