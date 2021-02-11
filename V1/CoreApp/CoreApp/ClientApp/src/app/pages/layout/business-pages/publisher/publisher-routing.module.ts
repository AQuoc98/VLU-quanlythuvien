import {NgModule} from '@angular/core';
import { Routes , RouterModule} from '@angular/router';
import {PublisherComponent} from './publisher.component';
import {PublisherAddComponent} from './publisher-add.component';

const routes = [
    { path : '', component :  PublisherComponent},
    { path: 'add/:id', component: PublisherAddComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

export class PublisherRoutingModule {

}
