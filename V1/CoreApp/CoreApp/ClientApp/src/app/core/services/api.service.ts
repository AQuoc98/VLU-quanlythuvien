import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import * as moment from 'moment';

@Injectable()
export class ApiService {
  constructor(private http: HttpClient) { }

  private formatErrors(error: any) {
    return throwError(error.error);
  }

  get(path: string, params: HttpParams = new HttpParams()): Observable<any> {
    return this.http.get(`${path}`, { params })
      .pipe(
        map((response) => {
          return this.convertDateStringsToDates(response);
        }),
        catchError(this.formatErrors)
      );
  }

  put(path: string, body: Object = {}): Observable<any> {
    return this.http.put(
      `${path}`,
      body
    ).pipe(catchError(this.formatErrors));
  }

  post(path: string, body: Object = {}): Observable<any> {
    return this.http.post(`${path}`, body, { responseType: 'json' })
      .pipe(catchError(this.formatErrors));
  }



  delete(path): Observable<any> {
    return this.http.delete(
      `${path}`
    ).pipe(catchError(this.formatErrors));
  }

  private convertDateStringsToDates(response: any) {
    // Ignore things that aren't objects.
    if (typeof response !== "object") return response;

    for (let key in response) {
      if (!response.hasOwnProperty(key)) continue;

      var value = response[key];
      // Check for string properties which look like dates.
      if (typeof value === "string" && (moment(value, moment.ISO_8601).isValid()) && !/^\d+$/.test(value)) {
        response[key] = moment(value, moment.ISO_8601).toDate();
      } else if (typeof value === "object") {
        // Recurse into object
        this.convertDateStringsToDates(value);
      }
    }
    return response;
  }
}
