import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { AddButtonComponent } from '../add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { AddIngredientComponent } from '../../forms/add-forms/add-ingredient/add-ingredient.component';
import { EditionService } from '../../services/edition.service';

export interface IngredientElement {
  position: number;
  name: string;
}

const ELEMENT_DATA: IngredientElement[] = [
  {
    position: 1,
    name: 'Hydrogen',
  },
];

@Component({
  selector: 'app-ingredients',
  standalone: true,
  imports: [TableComponent, AddButtonComponent],
  templateUrl: './ingredients.component.html',
  styleUrl: './ingredients.component.css',
})
export class IngredientsComponent implements OnInit {
  displayedColumns = [
    { name: 'position', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: '', displayName: 'Actions' },
  ];
  dataSource: IngredientElement[] = ELEMENT_DATA;
  buttonLabel = 'Add ingredient';
  constructor(
    public dialog: MatDialog,
    private editionService: EditionService
  ) {}
  openAdditionDialog() {
    this.dialog.open(AddIngredientComponent);
  }
  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl('ingredients');
  }
}
