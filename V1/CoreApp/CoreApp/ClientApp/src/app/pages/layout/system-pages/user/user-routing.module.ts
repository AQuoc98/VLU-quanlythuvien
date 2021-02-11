import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user.component';
import { UserAddComponent } from './user-add.component';

const routes: Routes = [
  { path: '', component: UserComponent },
  { path: 'add/:id', component: UserAddComponent, data: { title: 'USER_ADD.TITLEADD', subtitle: 'USER_ADD.SUBTITLE' } },
  // { path: 'add/:id', component: UserAddComponent, data: { title: 'USER_ADD.TITLEEDIT', subtitle: 'USER_ADD.SUBTITLE' } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule {

}
