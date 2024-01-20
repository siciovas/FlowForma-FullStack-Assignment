import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ComponentType } from '@angular/cdk/portal';
import { ActivatedRoute } from '@angular/router';
import { AddClothesComponent } from '../forms/add-forms/add-clothes/add-clothes.component';
import { AddDeviceComponent } from '../forms/add-forms/add-device/add-device.component';
import { AddFoodComponent } from '../forms/add-forms/add-food/add-food.component';
import { AddIngredientComponent } from '../forms/add-forms/add-ingredient/add-ingredient.component';
import { AddMaterialComponent } from '../forms/add-forms/add-material/add-material.component';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EditionService {
  private editDialogComponent: ComponentType<any> | undefined;

  constructor(private dialog: MatDialog, private route: ActivatedRoute) {}

  setEditDialogComponent(component: ComponentType<any>): void {
    this.editDialogComponent = component;
  }

  openEditDialog(): void {
    console.log('aaa');
    if (this.editDialogComponent) {
      this.dialog.open(this.editDialogComponent);
    }
  }

  determineComponentBasedOnUrl(url: string): void {
    if (url == environment.CLOTHES) {
      this.setEditDialogComponent(AddClothesComponent);
    } else if (url == environment.DEVICES) {
      this.setEditDialogComponent(AddDeviceComponent);
    } else if (url == environment.FOODS) {
      this.setEditDialogComponent(AddFoodComponent);
    } else if (url == environment.INGREDIENTS) {
      this.setEditDialogComponent(AddIngredientComponent);
    } else if (url == environment.MATERIALS) {
      this.setEditDialogComponent(AddMaterialComponent);
    }
  }
}
