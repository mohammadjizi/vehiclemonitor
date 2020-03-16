import { Pipe, PipeTransform } from '@angular/core';
import { VehicleStatus, DataUsageType } from './vehicle';

@Pipe({ name: 'vehicleStatus' })
export class VehicleStatusPipe implements PipeTransform {
  transform(data: any[], usageType: DataUsageType): any[] {
    //if (value == 0) {
    //  return "Disconnected";
    //} else if (value == 1) {
    //  return "Connected";
    //}

    if (usageType == DataUsageType.Filter) {
      for (var monitor of data) {
        if (monitor.vehicleStatus == "0") {
          monitor.vehicleStatus = VehicleStatus[VehicleStatus.Detached];
        } else {
          monitor.vehicleStatus = VehicleStatus[VehicleStatus.Linked];
        }
      }
      return data;

    } else if (usageType == DataUsageType.Server) {
      let d: any[] = JSON.parse(JSON.stringify(data));

      for (var monitor of d) {
        if (monitor.vehicleStatus == VehicleStatus[VehicleStatus.Detached]) {
          monitor.vehicleStatus = "0";
        } else {
          monitor.vehicleStatus = "1";
        }
      }
      return d;
    }


  }
}
