import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SortableModule } from 'ngx-bootstrap/sortable';

import { EnumRoutingModule } from './enum-routing.module';
import { EnumComponent } from './enum.component';
import { EnumAddComponent } from './enum-add.component';
import { GridViewModule, ToolBarModule, ConfirmDialogModule, AvatarChooserModule } from '@shared/components';
import { CoreEnumApiService } from '@infrastructure/api-services';

@NgModule({
    imports: [
        EnumRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        GridViewModule,
        ToolBarModule,
        ConfirmDialogModule,
        TranslateModule,
        CommonModule,
        SortableModule,
        AvatarChooserModule
    ],
    declarations: [
        EnumComponent,
        EnumAddComponent
    ]
})
export class EnumModule { }
