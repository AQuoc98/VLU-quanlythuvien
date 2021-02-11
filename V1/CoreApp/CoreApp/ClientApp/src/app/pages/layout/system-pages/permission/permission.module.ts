import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SortableModule } from 'ngx-bootstrap/sortable';

import { PermissionRoutingModule } from './permission-routing.module';
import { PermissionComponent } from './permission.component';
import { PermissionAddComponent } from './permission-add.component';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
import { CoreModuleApiService, CorePermissionApiService } from '@infrastructure/api-services';

@NgModule({
    imports: [
        PermissionRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        GridViewModule,
        ToolBarModule,
        ConfirmDialogModule,
        TranslateModule,
        CommonModule,
        SortableModule
    ],
    declarations: [
        PermissionComponent,
        PermissionAddComponent
    ]
})
export class PermissionModule { }
