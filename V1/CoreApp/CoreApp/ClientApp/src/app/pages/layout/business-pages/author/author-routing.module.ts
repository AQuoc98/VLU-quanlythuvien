import {NgModule} from '@angular/core';
import { Routes , RouterModule} from '@angular/router';
import {AuthorComponent} from './author.component';
import {AuthorAddComponent} from './author-add.component';

const routes = [
    { path : '',component :  AuthorComponent},
    { path: 'add/:id', component: AuthorAddComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

export class AuthorRoutingModule {

}
