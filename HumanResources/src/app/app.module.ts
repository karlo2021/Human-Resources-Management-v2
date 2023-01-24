import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { PersonsComponent } from './persons/persons.component';

import { AppRoutingModule } from './app-routing.module';
import { AngularMaterialModule } from './angular-material.module';
import { MeetingsComponent } from './meetings/meetings.component';

import { PersonEditComponent } from './persons/person-edit.component';
import { FormsModule } from '@angular/forms';
import { LOCALE_ID } from "@angular/core";
import localeHr from '@angular/common/locales/hr';
import { registerLocaleData } from '@angular/common';

registerLocaleData(localeHr);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    PersonsComponent,
    MeetingsComponent,
    PersonEditComponent,
  ],
  imports: [
    BrowserModule, HttpClientModule, BrowserAnimationsModule,
    AppRoutingModule, AngularMaterialModule, FormsModule
  ],
  providers: [{ provide: LOCALE_ID, useValue: "hr-HR" }],
  bootstrap: [AppComponent]
})
export class AppModule { }
