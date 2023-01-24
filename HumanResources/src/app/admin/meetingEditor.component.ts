import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { MeetingService } from "../meetings/meeting.service";
import { Meeting } from "../meetings/meeting";

@Component({
  selector: "meetingEditor",
  templateUrl: "meetingEditor.component.html"
})
export class MeetingEditorComponent {

  meeting: Meeting = new Meeting();
  personId?: number;
  meetingId?: number;

  constructor(
    private meetingService: MeetingService,
    private router: Router,
    private activeRoute: ActivatedRoute) {

    this.personId = this.activeRoute.snapshot.params["id"];
    this.meetingId = this.activeRoute.snapshot.params["mid"];

    this.loadData();

  }

  loadData() {
    if (this.meetingId) {
      this.meetingService.get(this.meetingId)
        .subscribe(result => {
          this.meeting = result;
        }, error => console.error(error));
    }
  }

  save() {
    console.log("Saved meeting");
    if (this.meetingId) {
      this.meeting.employed = Boolean(this.meeting.employed);
      this.meetingService.put(this.meeting, this.meetingId)
        .subscribe(result => { }, error => console.error(error));
    } else {
      this.meeting.personId = Number(this.personId);
      this.meeting.employed = Boolean(this.meeting.employed);
      this.meetingService.post(this.meeting)
        .subscribe(result => { }, error => console.error(error));
    }
    this.router.navigateByUrl(`admin/main/persons/edit/${this.personId}`);
  }
}
