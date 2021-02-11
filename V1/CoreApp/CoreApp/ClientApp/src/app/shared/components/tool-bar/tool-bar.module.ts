import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { ToolBarComponent } from './tool-bar.component';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    TranslateModule,
    CommonModule
  ],
  declarations: [
    ToolBarComponent
  ],
  providers: [],
  exports: [ToolBarComponent]
})
export class ToolBarModule { }
