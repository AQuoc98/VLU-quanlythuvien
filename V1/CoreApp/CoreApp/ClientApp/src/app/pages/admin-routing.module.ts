import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { extract } from '../core';
import { AuthGuard } from '../core/guards/auth.guard';

const routes: Routes = [
    {
        path: '',
        component: AdminComponent,
        children: [
            { path: '', loadChildren: './layout/layout.module#LayoutModule', canActivate: [AuthGuard] },
            { path: 'login', loadChildren: './login/login.module#LoginModule' },
            { path: '**', pathMatch: 'prefix', redirectTo: '' }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
