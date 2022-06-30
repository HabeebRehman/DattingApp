import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './error/not-found/not-found.component';
import { ServerErrorComponent } from './error/server-error/server-error.component';
import { TestErrorsComponent } from './Error/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { ListComponent } from './members/list/list.component';
import { MemberlistComponent } from './members/memberlist/memberlist.component';
import { MembersComponent } from './members/members/members.component';
import { MessagesComponent } from './members/messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {
    path:'',runGuardsAndResolvers:'always',
    canActivate:[AuthGuard],
    children:[
      {path:'members',component:MemberlistComponent},
      {path:'members/:username',component:MembersComponent},
      {path:'lists',component:ListComponent},
      {path:'messages',component:MessagesComponent},
     
    ]
  },
   // {path:'messages',component:MessagesComponent,canActivate:[AuthGuard]}, //for the single
  {path:'errors',component:TestErrorsComponent},
  {path:'not-found',component:NotFoundComponent},
  {path:'server-error',component:ServerErrorComponent},
  {path:'**',component:NotFoundComponent,pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
