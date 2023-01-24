import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map, Observable } from "rxjs";
// import { environment } from '../environments/environment';
import { Person } from "./persons/person";

const PROTOCOL = "https";
const PORT = 4200;

@Injectable()
export class RestDataSource {
  auth_token?: string;
  baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = `${PROTOCOL}://${location.hostname}:${PORT}/`;
  }

  getPersons(): Observable<Person[]> {
    return this.http.get<Person[]>(this.baseUrl + "api/home");
  }

  savePerson(person: Person): Observable<Person> {
    return this.http.post<Person>(this.baseUrl + "api/home", person);
  }

  authenticate(item: LoginRequest): Observable<LoginResult> {
    return this.http.post<any>(this.baseUrl  + "api/Account/login", item)
  }

}




