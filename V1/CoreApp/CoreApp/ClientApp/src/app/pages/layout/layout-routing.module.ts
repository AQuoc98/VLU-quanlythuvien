import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { extract } from '../../core';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      // { path: '', redirectTo: 'dashboard', pathMatch: 'prefix' },
      { path: '', redirectTo: 'book', pathMatch: 'prefix' },
      { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule', data: { title: 'Dashboard', subtitle: 'Admin' } },
      { path: 'menu', loadChildren: './system-pages/menu/menu.module#MenuModule', data: { title: 'MENU.TITLE', subtitle: 'MENU.SUBTITLE' } },
      { path: 'module-management', loadChildren: './system-pages/module-management/module-management.module#ModuleManagementModule', data: { title: 'MODULE.TITLE', subtitle: 'MODULE.SUBTITLE' } },
      { path: 'table', loadChildren: './system-pages/table/table.module#TableModule', data: { title: 'TABLE.TITLE', subtitle: 'TABLE.SUBTITLE' } },
      { path: 'view', loadChildren: './system-pages/view/view.module#ViewModule', data: { title: 'VIEW.TITLE', subtitle: 'VIEW.SUBTITLE' } },
      { path: 'permission', loadChildren: './system-pages/permission/permission.module#PermissionModule', data: { title: 'PERMISSION.TITLE', subtitle: 'PERMISSION.SUBTITLE' } },
      { path: 'user', loadChildren: './system-pages/user/user.module#UserModule', data: { title: 'USER.TITLE', subtitle: 'USER.SUBTITLE' } },
      { path: 'enum', loadChildren: './system-pages/enum/enum.module#EnumModule', data: { title: 'ENUM.TITLE', subtitle: 'ENUM.SUBTITLE' } },
      { path: 'config', loadChildren: './system-pages/config/config.module#ConfigModule', data: { title: 'CONFIG.TITLE', subtitle: 'CONFIG.SUBTITLE' } },
      { path: 'profile', loadChildren: './system-pages/profile/profile.module#ProfileModule', data: { title: 'PROFILE.TITLE', subtitle: 'PROFILE.SUBTITLE' } },
      { path: 'author', loadChildren: './business-pages/author/author.module#AuthorModule', data: { title: 'AUTHOR.TITLE', subtitle: 'AUTHOR.SUBTITLE' } },
      { path: 'publisher', loadChildren: './business-pages/publisher/publisher.module#PublisherModule', data: { title: 'PUBLISHER.TITLE', subtitle: 'PUBLISHER.SUBTITLE' } },
      { path: 'membergroup', loadChildren: './business-pages/membergroup/membergroup.module#MemberGroupModule', data: { title: 'MEMBERGROUP.TITLE', subtitle: 'MEMBERGROUP.SUBTITLE' } },
      { path: 'catalog', loadChildren: './business-pages/catalog/catalog.module#CatalogModule', data: { title: 'CATALOG.TITLE', subtitle: 'CATALOG.SUBTITLE' } },
      { path: 'rack', loadChildren: './business-pages/rack/rack.module#RackModule', data: { title: 'RACK.TITLE', subtitle: 'RACK.SUBTITLE' } },
      { path: 'format', loadChildren: './business-pages/format/format.module#FormatModule', data: { title: 'FORMAT.TITLE', subtitle: 'FORMAT.SUBTITLE' } },
      { path: 'book', loadChildren: './business-pages/book/book.module#BookModule', data: { title: 'BOOK.TITLE', subtitle: 'BOOK.SUBTITLE' } },
      { path: 'language', loadChildren: './business-pages/language/language.module#LanguageModule', data: { title: 'LANGUAGE.TITLE', subtitle: 'LANGUAGE.SUBTITLE' } } ,
      { path: 'bookitem', loadChildren: './business-pages/bookitem/bookitem.module#BookItemModule', data: { title: 'BOOKITEM.TITLE', subtitle: 'BOOKITEM.SUBTITLE' } },
      { path: 'status', loadChildren: './business-pages/status/status.module#StatusModule', data: { title: 'STATUS.TITLE', subtitle: 'STATUS.SUBTITLE' } },
      { path: 'member', loadChildren: './business-pages/member-management/member-management.module#MemberManagementModule', data: { title: 'MEMBER.TITLE', subtitle: 'MEMBER.SUBTITLE' } },

      { path: 'policy', loadChildren: './business-pages/policy/policy.module#PolicyModule', data: { title: 'POLICY.TITLE', subtitle: 'POLICY.SUBTITLE' } }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
