import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ComponentType } from '@angular/cdk/portal';
import { ActivatedRoute } from '@angular/router';
import { AddClothesComponent } from '../forms/add-forms/add-clothes/add-clothes.component';
import { AddDeviceComponent } from '../forms/add-forms/add-device/add-device.component';
import { AddFoodComponent } from '../forms/add-forms/add-food/add-food.component';
import { AddIngredientComponent } from '../forms/add-forms/add-ingredient/add-ingredient.component';
import { AddMaterialComponent } from '../forms/add-forms/add-material/add-material.component';

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
    console.log('aaa')
    if (this.editDialogComponent) {
      this.dialog.open(this.editDialogComponent);
    }
  }

  determineComponentBasedOnUrl(url: string): void {
    const currentUrl = this.route.snapshot.url.join('/');
    console.log(url)
    if (url == 'clothes') {
      this.setEditDialogComponent(AddClothesComponent);
    } else if (url == 'devices') {
      this.setEditDialogComponent(AddDeviceComponent);
    } else if (url == 'foods') {
      this.setEditDialogComponent(AddFoodComponent);
    } else if (url == 'ingredients') {
      this.setEditDialogComponent(AddIngredientComponent);
    } else if (url == 'materials') {
      this.setEditDialogComponent(AddMaterialComponent);
    }
  }
}
