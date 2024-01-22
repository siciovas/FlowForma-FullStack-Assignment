import { AddButtonComponent } from '../add-button/add-button.component';
import { Component, Input, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { MatDialog } from '@angular/material/dialog';
import { AddMaterialComponent } from '../../forms/add-material/add-material.component';
import { EditionService } from '../../services/edition.service';
import { environment } from '../../environments/environment';
import { HttpService } from '../../services/http.service';

export interface MaterialElement {
  id: number;
  name: string;
}

const ELEMENT_DATA: MaterialElement[] = [];

@Component({
  selector: 'app-materials',
  standalone: true,
  imports: [TableComponent, AddButtonComponent],
  templateUrl: './materials.component.html',
  styleUrl: './materials.component.css',
})
export class MaterialsComponent implements OnInit {
  displayedColumns = [
    { name: '', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: '', displayName: 'Actions' },
  ];

  dataSource: MaterialElement[] = ELEMENT_DATA;
  buttonLabel = 'Add material';
  apiName = environment.MATERIALS;

  constructor(
    public dialog: MatDialog,
    private editionService: EditionService,
    private httpService: HttpService
  ) {}

  openAdditionDialog() {
    this.dialog.open(AddMaterialComponent, {
      data: {
        httpService: this.httpService,
      },
    });
  }

  loadMaterials() {
    this.httpService.getAll(environment.MATERIALS).subscribe((materials) => {
      this.dataSource = materials;
    });
  }

  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl(environment.MATERIALS);
    this.loadMaterials();
  }

  triggerEvent = () => {
    this.loadMaterials();
  };
}
