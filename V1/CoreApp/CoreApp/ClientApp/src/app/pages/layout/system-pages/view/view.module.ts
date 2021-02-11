import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SortableModule } from 'ngx-bootstrap/sortable';

import { ViewRoutingModule } from './view-routing.module';
import { ViewComponent } from './view.component';
import { ViewAddComponent } from './view-add.component';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
import { CoreViewApiService, CoreModuleApiService, CoreTableApiServcice } from '@infrastructure/api-services';

@NgModule({
    imports: [
        TranslateModule,
        ViewRoutingModule,
        SortableModule.forRoot(),
        FormsModule,
        ReactiveFormsModule,
        GridViewModule,
        CommonModule,
        ToolBarModule,
        ConfirmDialogModule
    ],
    declarations: [
        ViewComponent,
        ViewAddComponent
    ],
    providers: [CoreViewApiService, CoreModuleApiService, CoreTableApiServcice]
})
export class ViewModule { }
