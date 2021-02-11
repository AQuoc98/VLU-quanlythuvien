import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SortableModule } from 'ngx-bootstrap/sortable';

import { MenuRoutingModule } from './menu-routing.module';
import { MenuComponent } from './menu.component';
import { MenuAddComponent } from './menu-add.component';
import { GridViewModule, ToolBarModule, ConfirmDialogModule } from '@shared/components';
import { CoreViewApiService, CoreModuleApiService, CoreMenuApiService } from '@infrastructure/api-services';

@NgModule({
    imports: [
        MenuRoutingModule,
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
        MenuComponent,
        MenuAddComponent
    ]
})
export class MenuModule { }
