import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { NgModule, LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localeVN from '@angular/common/locales/vi';
registerLocaleData(localeVN);
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SortableModule } from 'ngx-bootstrap/sortable';
import { TagInputModule } from 'ngx-chips';
import { NgSelectModule } from '@ng-select/ng-select';
import { TimepickerModule, ProgressbarModule } from 'ngx-bootstrap';
import { FileUploadModule } from 'ng2-file-upload';

import { GridViewModule, ToolBarModule, ConfirmDialogModule, RTEModule, AvatarChooserModule } from '@shared/components';
import { SharedModule } from '@shared/shared.module';
import { ModalService } from '@core/services';

@NgModule({
    imports: [
        DashboardRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        GridViewModule,
        ToolBarModule,
        ConfirmDialogModule,
        TranslateModule,
        CommonModule,
        SortableModule,
        TagInputModule,
        NgSelectModule,
        RTEModule,
        AvatarChooserModule,
        SharedModule,
        TimepickerModule.forRoot(),
        FileUploadModule,
        ProgressbarModule.forRoot(),
    ],
    declarations: [
        DashboardComponent
    ], 
    exports: [

    ], 
    entryComponents: [

    ],
    providers: [ModalService]
})
export class DashboardModule { }
