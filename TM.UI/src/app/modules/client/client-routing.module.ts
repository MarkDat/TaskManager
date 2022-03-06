import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ClientComponent } from './client.component';
import { AuthGuard } from '@app/modules/shared/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: ClientComponent,
    children: [
      {path: '', redirectTo: 'projects', pathMatch: 'full'},
      {
				path: 'projects',
				canActivate: [AuthGuard],
        loadChildren: () => import('@app/modules/project/project.module').then(m => m.ProjectModule)
			}
    ]
  }
];

@NgModule({
imports: [RouterModule.forChild(routes)],
exports: [RouterModule]
})
export class ClientRoutingModule { }
