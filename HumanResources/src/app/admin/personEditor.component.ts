import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { MeetingService } from "../meetings/meeting.service";
import { Person } from "../persons/person";
import { PersonService } from "../persons/person.service";

@Component({
  templateUrl: "personEditor.component.html",
  styles: [".separator { flex: 1 1 auto; }"]
})
export class PersonEditorComponent {
  editing: boolean = false;
  person: Person = new Person();
  errorMessage?: string;
  constructor(private personService: PersonService,
    private meetingService: MeetingService,
    private router: Router,
    private activeRoute: ActivatedRoute) {

    this.loadData();

  }

  ngOnInit() {
    console.log("reloaded");
    this.loadData();
  }
  loadData() {
    this.editing = this.activeRoute.snapshot.params["mode"] == "edit";
    if (this.editing) {
      this.personService.get(this.activeRoute.snapshot.params["id"])
        .subscribe(result => {
          this.person = result;
        });
      this.personService.getMeetings(this.activeRoute.snapshot.params["id"]).subscribe(result => {
        this.person.meetings = result;
      });
    }
  }
  save(form: NgForm) {
    if (form.valid) {
      if (this.editing) {
        this.personService.put(this.person, this.activeRoute.snapshot.params["id"])
          .subscribe(result => { }, error => console.error(error));
      }
      else {
        this.person.meetings = [];
        this.personService.post(this.person)
          .subscribe(result => { }, error => console.error(error));
      }
      this.router.navigateByUrl("admin/main/persons");
    } else {
      this.errorMessage = "Form Data Invalid";
    }
  }

  deleteMeeting(id: number | undefined) {
    this.meetingService.delete(id ?? 0).subscribe(result => {
      console.log("Meeting is deleted.");
      }, error => console.log(error));
    this.loadData();
  }
}
