import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ComponentType } from '@angular/cdk/portal';
import { AddClothesComponent } from '../forms/add-clothes/add-clothes.component';
import { AddDeviceComponent } from '../forms/add-device/add-device.component';
import { AddFoodComponent } from '../forms/add-food/add-food.component';
import { AddIngredientComponent } from '../forms/add-ingredient/add-ingredient.component';
import { AddMaterialComponent } from '../forms/add-material/add-material.component';
import { environment } from '../environments/environment';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root',
})
export class EditionService {
  private editDialogComponent: ComponentType<any> | undefined;
  url: string = '';

  constructor(private dialog: MatDialog, private httpService: HttpService) {}

  setEditDialogComponent(component: ComponentType<any>): void {
    this.editDialogComponent = component;
  }

  openEditDialog(id: number | null): void {
    if (this.editDialogComponent && id) {
      this.httpService.getById(this.url, id).subscribe((response: any) => {
        var additionalData;
        if (this.url == environment.CLOTHES || this.url == environment.FOODS) {
          this.httpService
            .getAll(
              this.url == environment.CLOTHES
                ? environment.MATERIALS
                : environment.INGREDIENTS
            )
            .subscribe((data) => {
              additionalData = data;
              this.dialog.open(this.editDialogComponent as ComponentType<any>, {
                data: {
                  body: response,
                  httpService: this.httpService,
                  additionalData,
                },
              });
            });
        } else {
          this.dialog.open(this.editDialogComponent as ComponentType<any>, {
            data: {
              body: response,
              httpService: this.httpService,
            },
          });
        }
      });
    }
  }

  determineComponentBasedOnUrl(url: string): void {
    this.url = url;

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
