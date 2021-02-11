import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LanguageComponent } from './language.component';
import { LanguageAddComponent } from './language-add.component';

const routes = [
  { path: '', component: LanguageComponent },
  { path: 'add/:id', component: LanguageAddComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class LanguageRoutingModule {

}