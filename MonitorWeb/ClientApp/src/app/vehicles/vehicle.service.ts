import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';

import { MonitorViewModel } from './vehicle';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'my-auth-token'
  })
};

//@Injectable({
//  providedIn: 'root'
//})
export class MonitorService {
  //private _http: HttpClient;
  //private _baseUrl: string;

  //constructor(http: HttpClient, @Inject('BASE_URL')  baseUrl: string) {
  //  this._http = http;
  //  this._baseUrl = baseUrl;
  //}

  getVehicles(http: HttpClient, baseUrl: string): Observable<MonitorViewModel[]> {
    return http.get<MonitorViewModel[]>(baseUrl + 'api/MonitorVehicle/GetVehicles');
  }

  pingVehicles(http: HttpClient, baseUrl: string, data: MonitorViewModel[]): Observable<MonitorViewModel[]> {
    //return http.get<MonitorViewModel[]>(baseUrl + 'api/MonitorVehicle/PingVehicles');
    return http.put<MonitorViewModel[]>(baseUrl + 'api/MonitorVehicle/PingVehicles', data, httpOptions);
  }

}
