import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "./auth.service";
import { LoginRequest } from "./login-request";
import { LoginResult } from "./login-result";

@Component({
  templateUrl: "auth.component.html"
})

export class AuthComponent {
  username?: string;
  password?: string;
  errorMessage?: string;
  loginResult?: LoginResult;

  constructor(private router: Router,
    private auth: AuthService) { }

  authenticate(form: NgForm) {
    if (form.valid) {
      this.auth.login(<LoginRequest>{ email: this.username ?? "", password: this.password ?? "" })
        .subscribe(response => {
          console.log(response);
          this.loginResult = response;
          if (response.success && response.token) {
            this.router.navigateByUrl("/admin/main");
          }
          
        }, error => {
          console.log(error);
          this.errorMessage = "Authentication Failed";
          if (error.status == 401) {
            this.loginResult = error.error;
          }
        });
    } else {
      this.errorMessage = "Form Data Invalid";
    }
  }
}
