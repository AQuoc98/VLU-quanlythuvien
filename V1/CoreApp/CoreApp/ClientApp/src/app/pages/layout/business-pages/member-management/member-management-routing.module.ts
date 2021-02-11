import {NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';
import { MemberManagementComponent} from './member-management.component';
import { MemberAddComponent } from './member-add.component';


const routes = [
    { path : '',component :  MemberManagementComponent},
    { path: 'add/:id', component: MemberAddComponent },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

export class MemberManagementRoutingModule {

}
