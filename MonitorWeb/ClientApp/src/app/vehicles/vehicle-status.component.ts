import { Component, OnChanges, Input, Output, EventEmitter } from "@angular/core";
import { VehicleStatus } from './vehicle';

@Component({
  selector: 'app-vehicle-status',
  templateUrl: './vehicle-status.component.html',
  styleUrls: ['./vehicle-status.component.css']
})
export class VehicleStatusComponent implements OnChanges {
  @Input() vStatus: string = VehicleStatus[VehicleStatus.Detached];
  //vColor: string = "";
  vPath: string="";

  ngOnChanges(): void {
    //this.vColor = this.vStatus == "Disconnected" ? "#FF0000" : "#008000";
    this.vPath = this.vStatus == VehicleStatus[VehicleStatus.Detached] ? "../../assets/icon_car5_32X32.png" : "../../assets/icon_car4_32X32.png"
  }
}
