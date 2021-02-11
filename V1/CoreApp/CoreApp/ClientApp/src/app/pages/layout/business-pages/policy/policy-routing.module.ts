import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PolicyComponent } from './policy.component';
import { PolicyAddComponent } from './policy-add.component';

const routes = [
  { path: '', component: PolicyComponent },
  { path: 'add/:id', component: PolicyAddComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class PolicyRoutingModule {

}
