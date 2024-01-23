import { AddButtonComponent } from '../buttons/add-button/add-button.component';
import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { MatDialog } from '@angular/material/dialog';
import { FormMaterialComponent } from '../forms/form-material/form-material.component';
import { EditionService } from '../../services/edition.service';
import { environment } from '../../environments/environment';
import { HttpService } from '../../services/http.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';

export interface MaterialElement {
  id: number;
  name: string;
}

const ELEMENT_DATA: MaterialElement[] = [];

@Component({
  selector: 'app-materials',
  standalone: true,
  imports: [
    TableComponent,
    AddButtonComponent,
    MatProgressSpinnerModule,
    CommonModule,
  ],
  templateUrl: './materials.component.html',
  styleUrl: './materials.component.css',
})
export class MaterialsComponent implements OnInit {
  displayedColumns = [
    { name: '', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: '', displayName: 'Actions' },
  ];

  isLoading: boolean = true;
  dataSource: MaterialElement[] = ELEMENT_DATA;
  buttonLabel = 'Add material';
  apiName = environment.MATERIALS;

  constructor(
    public dialog: MatDialog,
    private editionService: EditionService,
    private httpService: HttpService
  ) {}

  openAdditionDialog() {
    const dialogRef = this.dialog.open(FormMaterialComponent, {
      data: {
        httpService: this.httpService,
      },
    });
    dialogRef.componentInstance.triggerEvent.subscribe(() => {
      this.triggerEvent();
    });
  }

  loadMaterials() {
    this.httpService.getAll(environment.MATERIALS).subscribe((materials) => {
      this.dataSource = materials;
      this.isLoading = false;
    });
  }

  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl(environment.MATERIALS);
    this.loadMaterials();
  }

  triggerEvent = () => {
    this.isLoading = true;
    this.loadMaterials();
  };
}
