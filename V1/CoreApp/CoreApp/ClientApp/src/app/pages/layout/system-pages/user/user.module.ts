import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SortableModule } from 'ngx-bootstrap/sortable';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { UserAddComponent } from './user-add.component';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
    imports: [
        UserRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        GridViewModule,
        ToolBarModule,
        ConfirmDialogModule,
        TranslateModule,
        CommonModule,
        SortableModule,
        NgSelectModule
    ],
    declarations: [
        UserComponent,
        UserAddComponent
    ]
})
export class UserModule { }
