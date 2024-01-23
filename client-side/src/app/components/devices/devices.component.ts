import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { AddButtonComponent } from '../buttons/add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { FormDeviceComponent } from '../forms/form-device/form-device.component';
import { EditionService } from '../../services/edition.service';
import { environment } from '../../environments/environment';
import { HttpService } from '../../services/http.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';

export interface DeviceElement {
  id: number;
  name: string;
  description: string;
  price: number;
  iselectronical: boolean;
}

const ELEMENT_DATA: DeviceElement[] = [];

@Component({
  selector: 'app-devices',
  standalone: true,
  imports: [
    TableComponent,
    AddButtonComponent,
    MatProgressSpinnerModule,
    CommonModule,
  ],
  templateUrl: './devices.component.html',
  styleUrl: './devices.component.css',
})
export class DevicesComponent implements OnInit {
  displayedColumns = [
    { name: '', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: 'description', displayName: 'Description' },
    { name: 'price', displayName: 'Price' },
    { name: 'isElectronical', displayName: 'Is Electronical?' },
    { name: '', displayName: 'Actions' },
  ];

  isLoading: boolean = true;
  dataSource: DeviceElement[] = ELEMENT_DATA;
  buttonLabel = 'Add device';
  apiName = environment.DEVICES;

  constructor(
    public dialog: MatDialog,
    private editionService: EditionService,
    private httpService: HttpService
  ) {}

  openAdditionDialog() {
    const dialogRef = this.dialog.open(FormDeviceComponent, {
      data: {
        httpService: this.httpService,
      },
    });
    dialogRef.componentInstance.triggerEvent.subscribe(() => {
      this.triggerEvent();
    });
  }

  loadDevices() {
    this.httpService.getAll(environment.DEVICES).subscribe((device) => {
      this.dataSource = device;
      this.isLoading = false;
    });
  }

  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl(environment.DEVICES);
    this.loadDevices();
  }

  triggerEvent = () => {
    this.isLoading = true;
    this.loadDevices();
  };
}
