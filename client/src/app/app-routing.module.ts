import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';

const routes: Routes = [
  { path: '', component: HomeComponent}, /** when someone browses to baseAddress/ homeComponent is loaded  */
  { path: 'members', component: MemberListComponent},
  { path: 'members/:id', component: MemberDetailComponent},
  { path: 'lists', component: ListsComponent},
  { path: 'messages', component: MessagesComponent},
  { path: '**', component: HomeComponent, pathMatch: 'full'}, /** if users type rubbish, go to home component, pathMatch to prevent loop */
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
