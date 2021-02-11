import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MenuComponent } from './menu.component';
import { MenuAddComponent } from './menu-add.component';

const routes: Routes = [
    {path: '', component: MenuComponent},
    { path: 'add/:id', component: MenuAddComponent, data: { title: 'MENU_ADD.TITLE', subtitle: 'MENU_ADD.SUBTITLE' } }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MenuRoutingModule {
}
