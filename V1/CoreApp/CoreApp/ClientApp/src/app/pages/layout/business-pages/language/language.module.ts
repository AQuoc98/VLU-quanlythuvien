import { LanguageAddComponent } from './language-add.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SortableModule } from 'ngx-bootstrap/sortable';

import { LanguageComponent } from './language.component';
import { LanguageRoutingModule } from './language-routing.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';

@NgModule({
    declarations: [
        LanguageComponent ,
        LanguageAddComponent
    ],
    imports: [
        LanguageRoutingModule,
        CommonModule,
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

export class LanguageModule { }