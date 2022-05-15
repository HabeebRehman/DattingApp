import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MembersComponent } from './members/members/members.component';
import { MemberlistComponent } from './members/memberlist/memberlist.component';
import { MessagesComponent } from './members/messages/messages.component';
import { ListComponent } from './members/list/list.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorsComponent } from './Error/test-errors/test-errors.component';
import { ErrorInterceptor } from './Interceptors/error.interceptor';
import { NotFoundComponent } from './error/not-found/not-found.component';
import { ServerErrorComponent } from './error/server-error/server-error.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MembersComponent,
    MemberlistComponent,
    MessagesComponent,
    ListComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule
  ],
  providers: [
    {
      provide:HTTP_INTERCEPTORS,
      useClass:ErrorInterceptor,
      multi:true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
