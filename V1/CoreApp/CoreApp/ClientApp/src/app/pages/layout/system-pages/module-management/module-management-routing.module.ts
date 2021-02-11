import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModuleManagementComponent } from './module-management.component';
import { ModuleAddComponent } from './module-add.component';

const routes: Routes = [
    { path: '', component: ModuleManagementComponent },
    { path: 'add/:id', component: ModuleAddComponent, data: { title: 'MODULE_ADD.TITLE', subtitle: 'MODULE_ADD.SUBTITLE' } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ModuleManagementRoutingModule {
}
