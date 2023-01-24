import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Data, Router } from '@angular/router';
import { FormGroup, FormControl, NgForm } from '@angular/forms';
import { environment } from './../../environments/environment';

@Component({
  selector: 'app-person-edit',
  templateUrl: './person-edit.component.html',
  styleUrls: ['./person-edit.component.css']
})
export class PersonEditComponent {

  title?: string;
  person: Guy = {
    id: 1,
    name: "Karlo",
    birth: new Date(2000, 2, 15),
    birthS: "2000-2-15"
  };
  

  constructor() {
    console.log(this.person.id);
  }
  submit(form: NgForm) {
    console.log("Hello World")
    
    console.log(typeof(this.person.birth))
  }
}
class Guy {
   public id?: number;
   public name?: string;
  public birth?: Date;
  public birthS?: string;

}
