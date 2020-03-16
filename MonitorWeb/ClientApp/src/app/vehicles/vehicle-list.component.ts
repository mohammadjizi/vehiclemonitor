import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material';

import { Observable } from 'rxjs';
import { flatMap, catchError, tap, map } from 'rxjs/operators';

import { MonitorService } from './vehicle.service';
//import { MonitorViewModel } from '../fetch-data/fetch-data.component';
import { VehicleStatusPipe } from './vehicle-status.pipe';
import { VehicleStatus, DataUsageType } from './vehicle';

@Component({
  selector: 'app-vehicle-list',
  styleUrls: ['./vehicle-list.component.css'],
  templateUrl: './vehicle-list.component.html'
})
export class VehicleListComponent implements OnInit {
  private _http: HttpClient;
  private _baseUrl: string;
  private _monitorService: MonitorService;

  displayedColumns: string[] = ['ownsershipId', 'vehicleId', 'customerName', 'customerAddress', 'vehicleStatus'];
  dataSource = new MatTableDataSource([]);
  vStatusPipe = new VehicleStatusPipe();

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, monitorService: MonitorService) {
    this._http = http;
    this._baseUrl = baseUrl;
    this._monitorService = monitorService;
  

    this._monitorService.getVehicles(http, baseUrl).subscribe(result => {

      //this.dataSource = new MatTableDataSource(this.manipulateDataForFilter(result));
      this.dataSource = new MatTableDataSource(this.vStatusPipe.transform(result, DataUsageType.Filter));

      this.dataSource.filterPredicate = (data: any, filtersJson: string) => {
        console.log("called filter predicte");

        const matchFilter = [];
        const filters = JSON.parse(filtersJson);

        filters.forEach(filter => {
          // check for null values!
          const val = data[filter.id] === null ? '' : data[filter.id];
          matchFilter.push(val.toLowerCase().includes(filter.value.toLowerCase()));
        });

        // Choose one
        //return matchFilter.every(Boolean); // AND condition
        return matchFilter.some(Boolean); // OR condition
      };

      Observable.interval(60000)
        .pipe(
          flatMap(() => {
            //return this._monitorService.pingVehicles(this._http, this._baseUrl, this.manipulateDataForServer(this.dataSource.data));
          return this._monitorService.pingVehicles(this._http, this._baseUrl, this.vStatusPipe.transform(this.dataSource.data,DataUsageType.Server));
        })
        )
        .subscribe(data => this.refreshPage(data))


    },
      error => console.error(error));
  }

  ngOnInit(): void {
    //Observable.interval(60000)
    //  .pipe(
    //    flatMap(() => {
    //    return this._monitorService.pingVehicles(this._http,this._baseUrl,this.dataSource.data);
    //    })
    //  )
    //  .subscribe(data => this.refreshPage(data))



  }

  applyFilter(filterValue: string) {
    //this.dataSource.filter = filterValue.trim().toLowerCase();
    const filters = [
      { "columnId": "customerName", "value": filterValue }, { "columnId": "vehicleStatus", "value": filterValue }
    ];

    const tableFilters = [];
    filters.forEach((filter) => {
      tableFilters.push({
        id: filter.columnId,
        value: filter.value
      });
    });
    this.dataSource.filter = JSON.stringify(tableFilters);
  }

  refreshPage(data: any[]) {
    console.log("called web api");

    //this.dataSource.data = this.manipulateDataForFilter(data);
    this.dataSource.data = this.vStatusPipe.transform(data, DataUsageType.Filter);
  }
 
}

