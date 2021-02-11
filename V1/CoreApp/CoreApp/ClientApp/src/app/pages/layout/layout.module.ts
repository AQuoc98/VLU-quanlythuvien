import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { LayoutRoutingModule } from './layout-routing.module';
import { LayoutComponent } from './layout.component';
import { FooterComponent, NavbarComponent, PageHeaderComponent, SidebarComponent } from './components';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    TranslateModule,
    LayoutRoutingModule,
    CommonModule,
    NgSelectModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [LayoutComponent, FooterComponent, NavbarComponent, PageHeaderComponent, SidebarComponent]
})
export class LayoutModule { }
