import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { AddButtonComponent } from '../add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { AddDeviceComponent } from '../../forms/add-forms/add-device/add-device.component';
import { EditionService } from '../../services/edition.service';

export interface DeviceElement {
  position: number;
  name: string;
  description: string;
  price: number;
  iselectronical: boolean;
}

const ELEMENT_DATA: DeviceElement[] = [
  {
    position: 1,
    name: 'Hydrogen',
    description: 'asdasd',
    price: 48,
    iselectronical: true,
  },
];

@Component({
  selector: 'app-devices',
  standalone: true,
  imports: [TableComponent, AddButtonComponent],
  templateUrl: './devices.component.html',
  styleUrl: './devices.component.css',
})
export class DevicesComponent implements OnInit {
  displayedColumns = [
    { name: 'position', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: 'description', displayName: 'Description' },
    { name: 'price', displayName: 'Price' },
    { name: 'iselectronical', displayName: 'Is Electronical?' },
    { name: '', displayName: 'Actions' },
  ];
  dataSource: DeviceElement[] = ELEMENT_DATA;
  buttonLabel = 'Add device';
  constructor(
    public dialog: MatDialog,
    private editionService: EditionService
  ) {}
  openAdditionDialog() {
    this.dialog.open(AddDeviceComponent);
  }
  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl('devices');
  }
}
