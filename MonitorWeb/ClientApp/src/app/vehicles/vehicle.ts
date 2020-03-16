export interface MonitorViewModel {
  ownsershipId: string;
  vehicleId: string;
  customerId: number;
  customerName: string;
  customerAddress: string;
  vehicleStatus: string;
}

export enum VehicleStatus {
  Detached,
  Linked
}

export enum DataUsageType {
  Filter,
  Server
}
