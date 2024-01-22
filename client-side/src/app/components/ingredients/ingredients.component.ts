import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { AddButtonComponent } from '../add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { AddIngredientComponent } from '../../forms/add-ingredient/add-ingredient.component';
import { EditionService } from '../../services/edition.service';
import { environment } from '../../environments/environment';
import { HttpService } from '../../services/http.service';

export interface IngredientElement {
  id: number;
  name: string;
}

const ELEMENT_DATA: IngredientElement[] = [];

@Component({
  selector: 'app-ingredients',
  standalone: true,
  imports: [TableComponent, AddButtonComponent],
  templateUrl: './ingredients.component.html',
  styleUrl: './ingredients.component.css',
})
export class IngredientsComponent implements OnInit {
  displayedColumns = [
    { name: '', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: '', displayName: 'Actions' },
  ];

  dataSource: IngredientElement[] = ELEMENT_DATA;
  apiName = environment.INGREDIENTS;
  buttonLabel = 'Add ingredient';

  constructor(
    public dialog: MatDialog,
    private editionService: EditionService,
    private httpService: HttpService
  ) {}
  openAdditionDialog() {
    this.dialog.open(AddIngredientComponent, {
      data: {
        httpService: this.httpService,
      },
    });
  }

  loadIngredients() {
    this.httpService
      .getAll(environment.INGREDIENTS)
      .subscribe((ingredients) => {
        this.dataSource = ingredients;
      });
  }

  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl(environment.INGREDIENTS);
    this.loadIngredients();
  }

  triggerEvent = () => {
    this.loadIngredients();
  };
}
