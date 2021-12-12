import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '@app/modules/header/header.component';
import { SharedModule } from '@app/modules/shared/shared.module';
import {RouterModule, Routes} from '@angular/router';

const COMPONENT = [
  HeaderComponent
]

@NgModule({
  declarations: [...COMPONENT],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ],
  exports: [...COMPONENT]
})
export class HeaderModule { }
