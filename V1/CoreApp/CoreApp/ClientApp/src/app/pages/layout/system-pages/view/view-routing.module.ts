import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ViewComponent } from './view.component';
import { ViewAddComponent } from './view-add.component';

const routes: Routes = [
    { path: '', component: ViewComponent },
    { path: 'add/:id', component: ViewAddComponent, data: { title: 'VIEW_ADD.TITLE', subtitle: 'VIEW_ADD.SUBTITLE' } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ViewRoutingModule {
}
