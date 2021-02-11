import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { EditorModule } from '@tinymce/tinymce-angular';

import { RTEComponent } from './rte.component';
import { FileExplorerModule } from '../file-explorer/file-explorer.module';

@NgModule({
    imports: [
        TranslateModule,
        FormsModule,
        CommonModule,
        EditorModule,
        FileExplorerModule
    ],
    declarations: [
        RTEComponent
    ],
    providers: [],
    exports: [RTEComponent]
})
export class RTEModule { }
