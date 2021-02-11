import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Add for routing here
const routes: Routes = [
  { path: 'admin', loadChildren: './pages/admin.module#AdminModule' },
  { path: '**', redirectTo: 'admin', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
