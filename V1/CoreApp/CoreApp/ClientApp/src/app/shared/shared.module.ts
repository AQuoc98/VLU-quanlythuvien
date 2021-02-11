import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule, BsDatepickerModule, SortableModule } from 'ngx-bootstrap';
import { TagInputModule } from 'ngx-chips';
import { NgxCurrencyModule } from 'ngx-currency';
import { NgSelectModule } from '@ng-select/ng-select';
import * as comps from './components';
import { Constants } from '@core/constants';
import { PipeModule } from './pipes';


@NgModule({
  imports: [
    CommonModule,

    PipeModule,

    comps.GridViewModule,
    comps.ToolBarModule,
    comps.ConfirmDialogModule,
    comps.AvatarChooserModule,
    comps.RTEModule,
    comps.AvatarChooserModule,
    comps.DynamicFormModule,
    comps.FileExplorerModule,

    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    SortableModule,
    TagInputModule,
    NgxCurrencyModule.forRoot(Constants.CurrencyMaskConfig),
    NgSelectModule,
  ],
  declarations: [],
  exports: [
    PipeModule,

    comps.GridViewModule,
    comps.ToolBarModule,
    comps.ConfirmDialogModule,
    comps.AvatarChooserModule,
    comps.RTEModule,
    comps.AvatarChooserModule,
    comps.DynamicFormModule,
    comps.FileExplorerModule,

    BsDatepickerModule,
    BsDropdownModule,
    SortableModule,
    TagInputModule,
    NgxCurrencyModule,
    NgSelectModule
  ]
})
export class SharedModule { }
