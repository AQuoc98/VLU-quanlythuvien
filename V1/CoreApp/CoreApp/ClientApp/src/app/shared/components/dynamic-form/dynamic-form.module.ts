import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { DynamicFormComponent } from './dynamic-form.component';
import { DynamicFieldDirective } from './components/dynamic-field/dynamic-field.directive';
import { InputComponent } from './components/input/input.component';
import { SelectComponent } from './components/select/select.component';
import { RTEModule } from '../rte/rte.module';
import { DiRteComponent } from './components/di-rte/di-rte.component';
import { TextAreaComponent } from './components/text-area/text-area.component';
import { AvatarChooserModule } from '../avatar-chooser';
import { ImageComponent } from './components/image/image.component';

@NgModule({
    imports: [
        TranslateModule,
        BsDatepickerModule.forRoot(),
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        RTEModule,
        AvatarChooserModule
    ],
    declarations: [
        DynamicFormComponent,
        DynamicFieldDirective,
        InputComponent,
        SelectComponent,
        DiRteComponent,
        TextAreaComponent,
        ImageComponent
    ],
    providers: [],
    exports: [DynamicFormComponent],
    entryComponents: [
        InputComponent,
        SelectComponent,
        DiRteComponent,
        TextAreaComponent,
        ImageComponent
    ],
})
export class DynamicFormModule { }
