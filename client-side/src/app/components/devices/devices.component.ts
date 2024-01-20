import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { AddButtonComponent } from '../add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { AddDeviceComponent } from '../../forms/add-forms/add-device/add-device.component';
import { EditionService } from '../../services/edition.service';
import { environment } from '../../environments/environment';
import { HttpService } from '../../services/http.service';

export interface DeviceElement {
  position: number;
  name: string;
  description: string;
  price: number;
  iselectronical: boolean;
}

const ELEMENT_DATA: DeviceElement[] = [];

@Component({
  selector: 'app-devices',
  standalone: true,
  imports: [TableComponent, AddButtonComponent],
  templateUrl: './devices.component.html',
  styleUrl: './devices.component.css',
})
export class DevicesComponent implements OnInit {
  displayedColumns = [
    { name: 'id', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: 'description', displayName: 'Description' },
    { name: 'price', displayName: 'Price' },
    { name: 'isElectronical', displayName: 'Is Electronical?' },
    { name: '', displayName: 'Actions' },
  ];

  dataSource: DeviceElement[] = ELEMENT_DATA;
  buttonLabel = 'Add device';
  apiName = environment.DEVICES;

  constructor(
    public dialog: MatDialog,
    private editionService: EditionService,
    private httpService: HttpService
  ) {}

  openAdditionDialog() {
    this.dialog.open(AddDeviceComponent);
  }

  loadDevices() {
    this.httpService.getAll(environment.DEVICES).subscribe((device) => {
      this.dataSource = device;
    });
  }

  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl(environment.DEVICES);
    this.loadDevices();
  }
}
