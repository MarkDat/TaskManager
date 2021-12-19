import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '@app/modules/shared/shared.module';
import { ProjectsComponent } from '@app/modules/project/projects/projects.component';
import { ProjectComponent } from '@app/modules/project/project.component';
import { KanbanComponent } from '@app/modules/project/kanban/kanban.component';
import { ModalDetailsComponent } from '@app/modules/project/kanban/modal/modal-details.component';
import { AuthModule } from '@app/modules/auth/auth.module';
import { ProjectRoutingModule } from '@app/modules/project/project-routing.module';


@NgModule({
  declarations: [
    KanbanComponent,
    ProjectComponent,
    ProjectsComponent,
    ModalDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ProjectRoutingModule,
  ],
  providers: [DatePipe]
})
export class ProjectModule { }
