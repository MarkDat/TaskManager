import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import {
  DxButtonModule,
  DxScrollViewModule,
  DxSortableModule,
  DxSelectBoxModule,
  DxTextAreaModule,
  DxTextBoxModule,
  DxDateBoxModule,
  DxTreeViewModule,
  DxTemplateModule,
  DxCheckBoxModule,
  DxPopupModule,
  DxFormModule,
  DxDataGridModule,
  DxBulletModule,
  DxLoadPanelModule,
  DxListModule
} from 'devextreme-angular';
import DataSource from 'devextreme/data/data_source';
import { NgCircleProgressModule } from 'ng-circle-progress';

import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AppComponent } from './app.component';
import { TestService } from './services/test.service';
import { KanbanComponent } from './components/kanban/kanban.component';
import { ModalComponent } from './components/kanban/modal/modal.component';
import { LoginComponent } from './components/auth/login/login.component';
import { ProjectsComponent } from './components/projects/projects.component';
import { HeaderComponent } from './components/header/header.component';
import { BaseService } from './services/base.service';
import { AuthService } from './services/auth.service';


const appRoutes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'projects/:id/kanban',
    component: KanbanComponent
  },
  {
    path: 'projects',
    component: ProjectsComponent
  }
];

@NgModule({
  declarations: [AppComponent, KanbanComponent, ModalComponent, LoginComponent, ProjectsComponent, HeaderComponent],
  imports: [
    BrowserModule,
    DxButtonModule,
    DxScrollViewModule,
    DxSortableModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxTextAreaModule,
    DxDateBoxModule,
    DxTreeViewModule,
    DxTemplateModule,
    DxCheckBoxModule,
    DxDataGridModule,
    DxBulletModule,
    DxFormModule,
    HttpClientModule,
    BrowserModule,
    DxPopupModule,
    DxLoadPanelModule,
    DxListModule,
    RouterModule.forRoot(appRoutes),
    NgCircleProgressModule.forRoot({
      radius: 20,
      outerStrokeWidth: 1,
      innerStrokeWidth: 12,
      outerStrokeColor: '#78C000',
      innerStrokeColor: '#C7E596',
      showSubtitle: false,
      clockwise: true,
      showUnits: false,
      showImage: false,
    }),
  ],
  providers: [TestService,AuthService, BaseService],
  bootstrap: [AppComponent],
})
export class AppModule { }
