import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PermissionComponent } from './permission.component';
import { PermissionAddComponent } from './permission-add.component';

const routes: Routes = [
    {path: '', component: PermissionComponent},
    { path: 'add/:id', component: PermissionAddComponent, data: { title: 'PERMISSION_ADD.TITLE', subtitle: 'PERMISSION_ADD.SUBTITLE' } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PermissionRoutingModule {
}
