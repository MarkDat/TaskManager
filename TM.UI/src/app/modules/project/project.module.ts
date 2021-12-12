import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { SharedModule } from '@app/modules/shared/shared.module';
import { ProjectsComponent } from '@app/modules/project/projects/projects.component';
import {RouterModule, Routes} from '@angular/router';
import { ProjectComponent } from '@app/modules/project/project.component';
import { KanbanComponent } from '@app/modules/project/kanban/kanban.component';
import { ModalDetailsComponent } from '@app/modules/project/kanban/modal/modal-details.component';

export const routes: Routes = [
	{
		path: '',
		component: ProjectsComponent,
	},
  {
    path: 'projects/:id/kanban',
    component: KanbanComponent
  }
];

@NgModule({
  declarations: [
    ProjectComponent,
    ProjectsComponent,
    KanbanComponent,
    ModalDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
  ],
  providers: [DatePipe]
})
export class ProjectModule { }
