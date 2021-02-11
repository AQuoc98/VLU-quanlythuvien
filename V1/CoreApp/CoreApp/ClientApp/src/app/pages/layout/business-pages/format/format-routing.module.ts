import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormatComponent } from './format.component';
import { FormatAddComponent } from './format-add.component';

const routes = [
  { path: '', component: FormatComponent },
  { path: 'add/:id', component: FormatAddComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class FormatRoutingModule {

}
