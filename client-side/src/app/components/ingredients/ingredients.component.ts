import { Component, Input, OnInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { AddButtonComponent } from '../buttons/add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { FormIngredientComponent } from '../forms/form-ingredient/form-ingredient.component';
import { EditionService } from '../../services/edition.service';
import { environment } from '../../environments/environment';
import { HttpService } from '../../services/http.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';

export interface IngredientElement {
  id: number;
  name: string;
}

const ELEMENT_DATA: IngredientElement[] = [];

@Component({
  selector: 'app-ingredients',
  standalone: true,
  imports: [
    TableComponent,
    AddButtonComponent,
    MatProgressSpinnerModule,
    CommonModule,
  ],
  templateUrl: './ingredients.component.html',
  styleUrl: './ingredients.component.css',
})
export class IngredientsComponent implements OnInit {
  displayedColumns = [
    { name: '', displayName: 'Pos.' },
    { name: 'name', displayName: 'Name' },
    { name: '', displayName: 'Actions' },
  ];

  isLoading: boolean = true;
  dataSource: IngredientElement[] = ELEMENT_DATA;
  apiName = environment.INGREDIENTS;
  buttonLabel = 'Add ingredient';

  constructor(
    public dialog: MatDialog,
    private editionService: EditionService,
    private httpService: HttpService
  ) {}
  openAdditionDialog() {
    const dialogRef = this.dialog.open(FormIngredientComponent, {
      data: {
        httpService: this.httpService,
      },
    });
    dialogRef.componentInstance.triggerEvent.subscribe(() => {
      this.triggerEvent();
    });
  }

  loadIngredients() {
    this.httpService
      .getAll(environment.INGREDIENTS)
      .subscribe((ingredients) => {
        this.dataSource = ingredients;
        this.isLoading = false;
      });
  }

  ngOnInit(): void {
    this.editionService.determineComponentBasedOnUrl(environment.INGREDIENTS);
    this.loadIngredients();
  }

  triggerEvent = () => {
    this.isLoading = true;
    this.loadIngredients();
  };
}
