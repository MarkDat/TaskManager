import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {
  DxButtonModule,
  DxScrollViewModule,
  DxSortableModule,
} from 'devextreme-angular';
import { NgCircleProgressModule } from 'ng-circle-progress';

import { AppComponent } from './app.component';
import { TestService } from './services/test.service';
import { KanbanComponent } from './components/kanban/kanban.component';

@NgModule({
  declarations: [AppComponent, KanbanComponent],
  imports: [
    BrowserModule,
    DxButtonModule,
    DxScrollViewModule,
    DxSortableModule,
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
  providers: [TestService],
  bootstrap: [AppComponent],
})
export class AppModule {}
