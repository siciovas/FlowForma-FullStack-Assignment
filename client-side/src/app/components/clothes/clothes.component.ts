import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { AddButtonComponent } from '../buttons/add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { FormClothesComponent } from '../forms/form-clothes/form-clothes.component';
import { EditionService } from '../../services/edition.service';
import { environment } from '../../environments/environment';
import { HttpService } from '../../services/http.service';
import { MaterialElement } from '../materials/materials.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';

export interface ClothesElement {
  id: number;
  name: string;
  description: string;
  size: string;
  price: number;
  materials: MaterialElement[];
}

const ELEMENT_DATA: ClothesElement[] = [];

@Component({
  selector: 'app-clothes',
  standalone: true,
  imports: [
    TableComponent,
    AddButtonComponent,
    MatProgressSpinnerModule,
    CommonModule,
  ],
  templateUrl: './clothes.component.html',
  styleUrl: './clothes.component.css',
})
export class ClothesComponent implements OnInit {
  displayedColumns = [
    { name: '', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: 'description', displayName: 'Description' },
    { name: 'size', displayName: 'Size' },
    { name: 'price', displayName: 'Price' },
    { name: 'materials', displayName: 'Materials' },
    { name: '', displayName: 'Actions' },
  ];

  isLoading: boolean = true;
  dataSource: ClothesElement[] = ELEMENT_DATA;
  buttonLabel = 'Add clothes';
  apiName = environment.CLOTHES;

  constructor(
    public dialog: MatDialog,
    private editionService: EditionService,
    private httpService: HttpService
  ) {}

  openAdditionDialog() {
    this.httpService
      .getAll(environment.MATERIALS)
      .subscribe((materials: MaterialElement[]) => {
        const dialogRef = this.dialog.open(FormClothesComponent, {
          data: {
            materials,
            httpService: this.httpService,
          },
        });
        dialogRef.componentInstance.triggerEvent.subscribe(() => {
          this.triggerEvent();
        });
      });
  }

  loadClothes() {
    this.httpService.getAll(environment.CLOTHES).subscribe((clothes) => {
      this.dataSource = clothes;
      this.isLoading = false;
    });
  }

  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl(environment.CLOTHES);
    this.loadClothes();
  }

  triggerEvent = () => {
    this.isLoading = true;
    this.loadClothes();
  };
}
