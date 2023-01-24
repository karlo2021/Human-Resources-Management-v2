import { NgModule } from "@angular/core";
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { AuthComponent } from "./auth.component";
import { AdminComponent } from "./admin.component";
import { AuthGuard } from "./auth.guard";
import { AuthInterceptor } from "./auth.interceptor";
import { PersonEditorComponent } from "./personEditor.component";
import { PersonTableComponent } from "./personTable.component";
import { MaterialFeatures } from "./material.module";
import { MeetingEditorComponent } from "./meetingEditor.component";


let routing = RouterModule.forChild([
  { path: 'auth', component: AuthComponent},
  {
    path: 'main', component: AdminComponent, canActivate: [AuthGuard],
    children: [
      { path: "persons/:mode/:id", component: PersonEditorComponent },
      { path: "persons/:mode", component: PersonEditorComponent },
      { path: "persons", component: PersonTableComponent },
      { path: "persons/edit/:id/meetings/:mid", component: MeetingEditorComponent },
      { path: "persons/edit/:id/meetings", component: MeetingEditorComponent },
      { path: "**", redirectTo: "persons" }
    ]
  },
  { path: '**', redirectTo: 'auth'}
]);

@NgModule({
  imports: [HttpClientModule, CommonModule, FormsModule, routing, MaterialFeatures],
  declarations: [AuthComponent, AdminComponent, PersonEditorComponent, PersonTableComponent,
    MeetingEditorComponent],
  providers: [AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ]
})
export class AdminModule { }
