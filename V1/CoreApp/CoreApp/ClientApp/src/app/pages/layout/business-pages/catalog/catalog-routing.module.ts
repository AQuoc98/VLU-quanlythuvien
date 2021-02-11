import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CatalogComponent } from './catalog.component';
import { CatalogAddComponent } from './catalog-add.component';

const routes = [
  { path: '', component: CatalogComponent },
  { path: 'add/:id', component: CatalogAddComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class CatalogRoutingModule {

}
