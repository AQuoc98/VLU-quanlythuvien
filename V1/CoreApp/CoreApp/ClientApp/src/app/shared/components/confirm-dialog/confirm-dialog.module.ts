import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { ModalModule } from 'ngx-bootstrap/modal';

import { ConfirmDialogComponent } from './confirm-dialog.component';
import { ConfirmDialogService } from './confirm-dialog.service';

@NgModule({
    imports: [
        TranslateModule,
        CommonModule,
        ModalModule.forRoot()
    ],
    declarations: [
        ConfirmDialogComponent
    ],
    providers: [ConfirmDialogService],
    entryComponents: [ConfirmDialogComponent],
    exports: []
})
export class ConfirmDialogModule { }
