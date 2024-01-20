import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { AddButtonComponent } from '../add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { AddClothesComponent } from '../../forms/add-forms/add-clothes/add-clothes.component';
import { EditionService } from '../../services/edition.service';
import { environment } from '../../environments/environment';
import { HttpService } from '../../services/http.service';

export interface ClothesElement {
  position: number;
  name: string;
  description: string;
  size: string;
  price: number;
  materials: string;
}

const ELEMENT_DATA: ClothesElement[] = [];

@Component({
  selector: 'app-clothes',
  standalone: true,
  imports: [TableComponent, AddButtonComponent],
  templateUrl: './clothes.component.html',
  styleUrl: './clothes.component.css',
})
export class ClothesComponent implements OnInit {
  displayedColumns = [
    { name: 'position', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: 'description', displayName: 'Description' },
    { name: 'size', displayName: 'Size' },
    { name: 'price', displayName: 'Price' },
    { name: 'materials', displayName: 'Materials' },
    { name: '', displayName: 'Actions' },
  ];

  dataSource: ClothesElement[] = ELEMENT_DATA;
  buttonLabel = 'Add clothes';
  apiName = environment.CLOTHES;

  constructor(
    public dialog: MatDialog,
    private editionService: EditionService,
    private httpService: HttpService
  ) {}

  openAdditionDialog() {
    this.dialog.open(AddClothesComponent);
  }

  loadClothes() {
    this.httpService.getAll(environment.CLOTHES).subscribe((clothes) => {
      this.dataSource = clothes;
    });
  }

  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl(environment.CLOTHES);
    this.loadClothes();
  }
}
