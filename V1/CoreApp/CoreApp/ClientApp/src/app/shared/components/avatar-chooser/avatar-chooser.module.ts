import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { EditorModule } from '@tinymce/tinymce-angular';
import { FileUploadModule } from 'ng2-file-upload';

import { AvatarChooserComponent } from './avatar-chooser.component';
import { FileExplorerModule } from '../file-explorer/file-explorer.module';

@NgModule({
    imports: [
        TranslateModule,
        FormsModule,
        CommonModule,
        EditorModule,
        FileExplorerModule,
        FileUploadModule
    ],
    declarations: [
        AvatarChooserComponent
    ],
    providers: [],
    exports: [AvatarChooserComponent]
})
export class AvatarChooserModule { }
