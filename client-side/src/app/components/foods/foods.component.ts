import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { AddButtonComponent } from '../add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { AddFoodComponent } from '../../forms/add-food/add-food.component';
import { EditionService } from '../../services/edition.service';
import { environment } from '../../environments/environment';
import { HttpService } from '../../services/http.service';
import { IngredientElement } from '../ingredients/ingredients.component';

export interface FoodElement {
  id: number;
  name: string;
  description: string;
  size: number;
  calories: number;
  price: number;
  isvegetarian: boolean;
  isvegan: boolean;
  ingredients: IngredientElement[];
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
    { name: '', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: 'description', displayName: 'Description' },
    { name: 'ingredients', displayName: 'Ingredients' },
    { name: 'size', displayName: 'Size' },
    { name: 'calories', displayName: 'Calories' },
    { name: 'price', displayName: 'Price' },
    { name: 'isVegetarian', displayName: 'Is vegetarian?' },
    { name: 'isVegan', displayName: 'Is vegan?' },
    { name: '', displayName: 'Actions' },
  ];

  dataSource: FoodElement[] = ELEMENT_DATA;
  buttonLabel = 'Add food';
  apiName = environment.FOODS;

  constructor(
    public dialog: MatDialog,
    private editionService: EditionService,
    private httpService: HttpService
  ) {}

  openAdditionDialog() {
    this.httpService
      .getAll(environment.INGREDIENTS)
      .subscribe((ingredients: IngredientElement[]) => {
        this.dialog.open(AddFoodComponent, {
          data: {
            ingredients,
            httpService: this.httpService,
          },
        });
      });
  }

  loadFoods() {
    this.httpService.getAll(environment.FOODS).subscribe((foods) => {
      this.dataSource = foods;
    });
  }

  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl(environment.FOODS);
    this.loadFoods();
  }

  triggerEvent = () => {
    this.loadFoods();
  };
}
