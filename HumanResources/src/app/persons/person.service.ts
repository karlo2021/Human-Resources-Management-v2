import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../base.service';
import { Observable } from 'rxjs';
import { Person } from './person';
import { environment } from '../../environments/environment';
import { Meeting } from '../meetings/meeting';

@Injectable({
  providedIn: 'root',
})

export class PersonService
  extends BaseService<Person> {

  constructor(
    http: HttpClient) {
    super(http);
  }

  getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Person>> {
    var url = this.getUrl("api/Home");
    var params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("sortColumn", sortColumn)
      .set("sortOrder", sortOrder);

    if (filterColumn && filterQuery) {
      params = params
        .set("filterColumn", filterColumn)
        .set("filterQuery", filterQuery);
    }
    return this.http.get<ApiResult<Person>>(url, { params });
  }

  get(id: number): Observable<Person> {
    var url = environment.baseUrl + 'api/Home/' + id;
    return this.http.get<Person>(url);
  }

  post(item: Person): Observable<Person> {
    var url = environment.baseUrl + 'api/Home';
    return this.http.post<Person>(url, item);
  }

  put(item: Person, id: number): Observable<Person> {
    var url = environment.baseUrl + 'api/Home/' + id;
    return this.http.put<Person>(url, item);
  }

  delete(id: number): Observable<Person> {
    var url = environment.baseUrl + 'api/Home/' + id;
    return this.http.delete<Person>(url);
  }

  getMeetings(id: number): Observable<Meeting[]> {

    var url = environment.baseUrl + 'api/meetings/' + id;
    return this.http.get<Meeting[]>(url);

  }
}
