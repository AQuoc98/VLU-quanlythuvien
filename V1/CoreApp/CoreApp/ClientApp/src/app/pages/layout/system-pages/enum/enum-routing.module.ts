import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EnumComponent } from './enum.component';
import { EnumAddComponent } from './enum-add.component';

const routes: Routes = [
    {path: '', component: EnumComponent},
    { path: 'add/:id', component: EnumAddComponent, data: { title: 'ENUM_ADD.TITLE', subtitle: 'ENUM_ADD.SUBTITLE' } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class EnumRoutingModule {
}
