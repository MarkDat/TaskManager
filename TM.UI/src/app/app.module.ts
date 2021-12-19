import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AppComponent} from './app.component';
import {TestService, BaseService, AuthService} from '@app/modules/shared/services';
import { AuthModule } from '@app/modules/auth/auth.module';
import { SharedModule } from '@app/modules/shared/shared.module';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ProjectModule } from '@app/modules/project/project.module';
import { HeaderModule } from '@app/modules/header/header.module';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [  
        BrowserModule,
        HttpClientModule,
        HeaderModule,
        SharedModule,
        AppRoutingModule,
    ],
    providers: [
        TestService, AuthService, BaseService
    ],
    bootstrap: [AppComponent]
})
export class AppModule {}
