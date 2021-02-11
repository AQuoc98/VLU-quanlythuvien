import {NgModule} from '@angular/core';
import { Routes , RouterModule} from '@angular/router';
import {StatusComponent} from './status.component';
import {StatusAddComponent} from './status-add.component';

const routes = [
    { path : '',component :  StatusComponent},
    { path: 'add/:id', component: StatusAddComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

export class StatusRoutingModule {

}
