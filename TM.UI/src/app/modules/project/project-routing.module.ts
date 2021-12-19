import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProjectsComponent } from '@app/modules/project/projects/projects.component';
import { KanbanComponent } from '@app/modules/project/kanban/kanban.component';
import { AuthGuard } from '@app/modules/shared/guards/auth.guard';


const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    component: ProjectsComponent,
  },
  {
    path: ':id/kanban',
    canActivate: [AuthGuard],
    component: KanbanComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectRoutingModule { }