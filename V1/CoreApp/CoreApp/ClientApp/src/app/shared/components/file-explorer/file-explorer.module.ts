import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { FileUploadModule } from 'ng2-file-upload';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { FileExplorerComponent } from './file-explorer.component';
import { FileExplorerService } from './file-explorer.service';
import { FileManagerApiService } from '../../../infrastructure/api-services';

@NgModule({
    imports: [
        TranslateModule,
        FormsModule,
        CommonModule,
        ModalModule.forRoot(),
        TabsModule.forRoot(),
        ProgressbarModule.forRoot(),
        FileUploadModule,
        PaginationModule.forRoot()
    ],
    declarations: [
        FileExplorerComponent
    ],
    providers: [FileExplorerService],
    entryComponents: [FileExplorerComponent],
    exports: []
})
export class FileExplorerModule { }
