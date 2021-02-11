import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';

import { ModuleManagementRoutingModule } from './module-management-routing.module';
import { ModuleManagementComponent } from './module-management.component';
import { ModuleAddComponent } from './module-add.component';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
import { CoreModuleApiService } from '@infrastructure/api-services';

@NgModule({
    imports: [
        ModuleManagementRoutingModule,
        FormsModule,
        TranslateModule,
        ReactiveFormsModule,
        CommonModule,
        GridViewModule,
        ToolBarModule,
        ConfirmDialogModule
    ],
    declarations: [
        ModuleManagementComponent,
        ModuleAddComponent
    ]
})
export class ModuleManagementModule { }
