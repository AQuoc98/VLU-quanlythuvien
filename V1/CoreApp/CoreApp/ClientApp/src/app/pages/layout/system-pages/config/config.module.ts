import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SortableModule } from 'ngx-bootstrap/sortable';

import { ConfigRoutingModule } from './config-routing.module';
import { ConfigComponent } from './config.component';
import { ConfirmDialogModule, DynamicFormModule } from '@shared/components';
import { CoreViewApiService, CoreModuleApiService, CoreConfigApiService } from '@infrastructure/api-services';

@NgModule({
    imports: [
        ConfigRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        ConfirmDialogModule,
        TranslateModule,
        CommonModule,
        SortableModule,
        DynamicFormModule
    ],
    declarations: [
        ConfigComponent
    ]
})
export class ConfigModule { }
