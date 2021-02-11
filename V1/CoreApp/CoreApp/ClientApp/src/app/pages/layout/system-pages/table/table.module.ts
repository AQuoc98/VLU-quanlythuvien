import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';

import { TableRoutingModule } from './table-routing.module';
import { TableComponent } from './table.component';
import { TableAddComponent } from './table-add.component';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
import { CoreModuleApiService, CoreTableApiServcice, CoreEnumApiService } from '@infrastructure/api-services';

@NgModule({
    imports: [
        TranslateModule,
        TableRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        GridViewModule,
        CommonModule,
        ToolBarModule,
        ConfirmDialogModule
    ],
    declarations: [
        TableComponent, TableAddComponent
    ]
})
export class TableModule { }
