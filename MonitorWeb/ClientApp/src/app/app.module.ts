import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {  MatFormFieldModule, MatTableModule, MatInputModule } from '@angular/material';
import { MatTooltipModule } from '@angular/material/tooltip';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VehicleListComponent } from './vehicles/vehicle-list.component';

import { MonitorService } from './vehicles/vehicle.service';
import { VehicleStatusPipe } from './vehicles/vehicle-status.pipe';
import { VehicleStatusComponent } from './vehicles/vehicle-status.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleListComponent,
    VehicleStatusPipe,
    VehicleStatusComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      //{ path: '', component: HomeComponent, pathMatch: 'full' },
      //{ path: 'counter', component: CounterComponent },
      //{ path: 'fetch-data', component: FetchDataComponent },
      //{path:'vehicles', component:VehicleListComponent}
      {path:'', component: VehicleListComponent, pathMatch: 'full'}
    ]), BrowserAnimationsModule, 
    MatFormFieldModule,
    MatTableModule,
    MatInputModule,
    MatTooltipModule
  ],
  providers: [MonitorService],
  bootstrap: [AppComponent]
})
export class AppModule { }
