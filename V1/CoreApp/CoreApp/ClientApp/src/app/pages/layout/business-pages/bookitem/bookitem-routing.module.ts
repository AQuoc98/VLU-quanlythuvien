import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BookItemComponent } from './bookitem.component';
import { BookItemAddComponent } from './bookitem-add.component';
import { ImportExcelComponent } from '../../../../shared/components/import-excel/import-excel.component';

const routes = [
  { path: '', component: BookItemComponent },
  { path: 'add/:id', component: BookItemAddComponent },
  { path: 'import-excel', component: ImportExcelComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class BookItemRoutingModule {

}
