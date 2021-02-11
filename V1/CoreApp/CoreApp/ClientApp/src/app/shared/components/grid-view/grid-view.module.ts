import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgSelectModule } from '@ng-select/ng-select';
import { GridViewComponent } from './grid-view.component';
import { ValueByDataTypeComponent } from './components/value-by-data-type/value-by-data-type.component';
import { FilterControlComponent } from './components/filter-control/filter-control.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { PipeModule } from '@shared/pipes';
import { Ng2SearchPipeModule } from 'ng2-search-filter';


@NgModule({
    imports: [
        TranslateModule,
        BsDatepickerModule.forRoot(),
        FormsModule,
        CommonModule,
        PaginationModule.forRoot(),
        NgSelectModule,
        PipeModule,
        Ng2SearchPipeModule
    ],
    declarations: [
        GridViewComponent,
        ValueByDataTypeComponent,
        PaginationComponent,
        FilterControlComponent
    ],
    providers: [],
    exports: [GridViewComponent]
})
export class GridViewModule { }
