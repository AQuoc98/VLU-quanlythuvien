import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileEditComponent } from './profile-edit.component';
import { DashboardComponent } from '@page/layout/dashboard/dashboard.component';

const routes: Routes = [
  { path: ':id', component: ProfileEditComponent, data: { title: 'PROFILE.TITLE', subtitle: 'PROFILE.SUBTITLE' } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProfileRoutingModule {
}
