import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../base.service';
import { Observable } from 'rxjs';
import { Meeting } from './meeting';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})

export class MeetingService
  extends BaseService<Meeting> {

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
  ): Observable<ApiResult<Meeting>> {
    var url = this.getUrl("api/Meetings");
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
    return this.http.get<ApiResult<Meeting>>(url, { params });
  }

  get(id: number): Observable<Meeting> {
    var url = environment.baseUrl + 'api/Meetings/get/' + id;
    return this.http.get<Meeting>(url);
  }

  post(item: Meeting): Observable<Meeting> {
    var url = environment.baseUrl + 'api/Meetings';
    return this.http.post<Meeting>(url, item);
  }

  put(item: Meeting, id: number): Observable<Meeting> {
    var url = environment.baseUrl + 'api/Meetings/' + id;
    return this.http.put<Meeting>(url, item);
  }

  delete(id: number): Observable<Meeting> {
    var url = environment.baseUrl + 'api/Meetings/' + id;
    return this.http.delete<Meeting>(url);
  }
}
