import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { AddButtonComponent } from '../add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { AddFoodComponent } from '../../forms/add-forms/add-food/add-food.component';
import { EditionService } from '../../services/edition.service';

export interface FoodElement {
  position: number;
  name: string;
  description: string;
  size: number;
  calories: number;
  price: number;
  isvegeterian: boolean;
  isvegan: boolean;
}

const ELEMENT_DATA: FoodElement[] = [];

@Component({
  selector: 'app-foods',
  standalone: true,
  imports: [TableComponent, AddButtonComponent],
  templateUrl: './foods.component.html',
  styleUrl: './foods.component.css',
})
export class FoodsComponent implements OnInit {
  displayedColumns = [
    { name: 'position', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: 'description', displayName: 'Description' },
    { name: 'size', displayName: 'Size' },
    { name: 'calories', displayName: 'Calories' },
    { name: 'price', displayName: 'Price' },
    { name: 'isvegeterian', displayName: 'Is vegeterian?' },
    { name: 'isvegan', displayName: 'Is vegan?' },
    { name: '', displayName: 'Actions' },
  ];

  dataSource: FoodElement[] = ELEMENT_DATA;
  buttonLabel = 'Add food';

  constructor(
    public dialog: MatDialog,
    private editionService: EditionService
  ) {}
  openAdditionDialog() {
    this.dialog.open(AddFoodComponent);
  }
  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl('foods');
  }
}
