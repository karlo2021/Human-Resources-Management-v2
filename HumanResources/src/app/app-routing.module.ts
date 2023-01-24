import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PersonsComponent } from './persons/persons.component';
import { MeetingsComponent } from './meetings/meetings.component';
import { PersonEditComponent } from './persons/person-edit.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'persons', component: PersonsComponent },
  { path: 'meetings', component: MeetingsComponent },
  { path: 'test', component: PersonEditComponent },
  {
    path: 'admin', loadChildren: () => import('./admin/admin.module')
      .then(m => m.AdminModule),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
