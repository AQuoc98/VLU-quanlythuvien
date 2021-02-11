import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BookComponent } from './book.component';
import { BookAddComponent } from './book-add.component';

const routes = [
  { path: '', component: BookComponent },
  { path: 'add/:id', component: BookAddComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class BookRoutingModule {

}
