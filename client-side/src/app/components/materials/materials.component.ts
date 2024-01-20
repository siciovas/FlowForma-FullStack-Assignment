import { AddButtonComponent } from '../add-button/add-button.component';
import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { MatDialog } from '@angular/material/dialog';
import { AddMaterialComponent } from '../../forms/add-forms/add-material/add-material.component';
import { EditionService } from '../../services/edition.service';

export interface MaterialElement {
  position: number;
  name: string;
}

const ELEMENT_DATA: MaterialElement[] = [
  {
    position: 1,
    name: 'Hydrogen',
  },
];

@Component({
  selector: 'app-materials',
  standalone: true,
  imports: [TableComponent, AddButtonComponent],
  templateUrl: './materials.component.html',
  styleUrl: './materials.component.css',
})
export class MaterialsComponent implements OnInit {
  displayedColumns = [
    { name: 'position', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: '', displayName: 'Actions' },
  ];
  dataSource: MaterialElement[] = ELEMENT_DATA;
  buttonLabel = 'Add material';
  constructor(
    public dialog: MatDialog,
    private editionService: EditionService
  ) {}
  openAdditionDialog() {
    this.dialog.open(AddMaterialComponent);
  }
  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl('materials');
  }
}
