import {NgModule} from '@angular/core';
import { Routes , RouterModule} from '@angular/router';
import {RackComponent} from './rack.component';
import {RackAddComponent} from './rack-add.component';

const routes = [
    { path : '', component :  RackComponent},
    { path: 'add/:id', component: RackAddComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

export class RackRoutingModule {

}
