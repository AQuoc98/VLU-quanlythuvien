import {NgModule} from '@angular/core';
import { Routes , RouterModule} from '@angular/router';
import {MembergroupComponent} from './membergroup.component';
import {MemberGroupAddComponent} from './membergroup-add.component';

const routes = [
    { path : '',component :  MembergroupComponent},
    { path: 'add/:id', component: MemberGroupAddComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

export class MemberGroupRoutingModule {

}
