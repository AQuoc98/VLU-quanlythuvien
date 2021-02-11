import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { AuthGuard } from '../core/guards/auth.guard';

@NgModule({
    imports: [
        TranslateModule,
        AdminRoutingModule
    ],
    declarations: [AdminComponent],
    providers: [AuthGuard]
})
export class AdminModule { }
