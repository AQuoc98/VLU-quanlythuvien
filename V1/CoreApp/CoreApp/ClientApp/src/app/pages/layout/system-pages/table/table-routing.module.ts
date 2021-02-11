import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TableComponent } from './table.component';
import { TableAddComponent } from './table-add.component';

const routes: Routes = [
    { path: '', component: TableComponent },
    { path: 'add/:id', component: TableAddComponent, data: { title: 'TABLE_ADD.TITLE', subtitle: 'TABLE_ADD.SUBTITLE' } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TableRoutingModule {
}
