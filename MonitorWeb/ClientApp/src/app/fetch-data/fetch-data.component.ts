import { Component, Inject, OnInit  } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material';

import { Observable } from 'rxjs';
import { flatMap, catchError, tap, map } from 'rxjs/operators';


export interface MonitorViewModel {
  ownsershipId: string;
  vehicleId: string;
  customerId: number;
  customerName: string;
  customerAddress: string;
  vehicleStatus: string;
}

const ELEMENT_DATA: MonitorViewModel[] = [
  //{ ownsershipId: "1", vehicleId: 'Hydrogen', customerId: 1, customerName: 'H', customerAddress: "s", vehicleStatus: "s" },
  //{ ownsershipId: "2", vehicleId: 'Hydrogen', customerId: 1, customerName: 'H', customerAddress: "s", vehicleStatus: "s" }
];


@Component({
  selector: 'app-fetch-data',
  styleUrls: ['./fetch-data.component.css'],
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  displayedColumns: string[] =
    ['ownsershipId', 'vehicleId', 'customerId', 'customerName', 'customerAddress', 'vehicleStatus'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);
  myHttp: HttpClient;
  myBaseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myHttp = http;
    this.myBaseUrl = baseUrl;

    http.get<MonitorViewModel[]>(baseUrl + 'api/MonitorVehicle/GetVehicles').subscribe(result => {
        this.dataSource = new MatTableDataSource(result);

      },
      error => console.error(error));
  }

  ngOnInit() {

    Observable.interval(60000)
      .pipe(
        flatMap(() => {
          return this.getCustomers()
        })
      )
      .subscribe(data => this.refreshPage(data)  )
  }

  refreshPage(data) {
    console.log("called web api");
    this.dataSource = new MatTableDataSource(data);
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  getCustomers(): Observable<MonitorViewModel[]> {
    return this.myHttp.get<MonitorViewModel[]>(this.myBaseUrl + 'api/MonitorVehicle/GetVehicles');
  }

  private handleError(err: HttpErrorResponse) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    //return throwError(errorMessage);
  }

}





