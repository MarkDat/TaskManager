import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NgCircleProgressModule} from 'ng-circle-progress';
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
  DxListModule,
  DxPopoverModule
} from 'devextreme-angular';
import { ChecklistTodoComponent } from './components/checklist-todo/checklist-todo.component';
import { RouterModule } from '@angular/router';

export const COMMON_MODULES = [
  CommonModule,
  RouterModule
];

export const DEV_EXTREME_MODULES = [
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
  DxPopupModule,
  DxLoadPanelModule,
  DxListModule,
  DxPopoverModule
];

export const COMPONENTS = [
  ChecklistTodoComponent
];

export const EXTENSIONS = [
  NgCircleProgressModule.forRoot(
    {
        radius: 20,
        outerStrokeWidth: 1,
        innerStrokeWidth: 12,
        outerStrokeColor: '#78C000',
        innerStrokeColor: '#C7E596',
        showSubtitle: false,
        clockwise: true,
        showUnits: false,
        showImage: false
    }
  )
];

@NgModule({
  declarations: [
    ...COMPONENTS
  ],
  imports: [
    ...COMMON_MODULES,
    ...DEV_EXTREME_MODULES,
    ...EXTENSIONS
  ],
  exports: [...DEV_EXTREME_MODULES,...COMMON_MODULES, ...COMPONENTS, EXTENSIONS]
})
export class SharedModule { }
