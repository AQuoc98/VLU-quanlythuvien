import { CatalogAddComponent } from './catalog-add.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule , ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {SortableModule} from 'ngx-bootstrap/sortable';
import {CatalogComponent} from './catalog.component';
import {CatalogRoutingModule} from './catalog-routing.module';
import {NgSelectModule} from '@ng-select/ng-select';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
@NgModule({
  declarations: [
    CatalogComponent,
    CatalogAddComponent
  ],
  imports: [
    CatalogRoutingModule,
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
export class CatalogModule { }
