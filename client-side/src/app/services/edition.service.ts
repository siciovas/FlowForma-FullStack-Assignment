import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ComponentType } from '@angular/cdk/portal';
import { FormClothesComponent } from '../components/forms/form-clothes/form-clothes.component';
import { FormDeviceComponent } from '../components/forms/form-device/form-device.component';
import { FormFoodComponent } from '../components/forms/form-food/form-food.component';
import { FormIngredientComponent } from '../components/forms/form-ingredient/form-ingredient.component';
import { FormMaterialComponent } from '../components/forms/form-material/form-material.component';
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

  openEditDialog(id: number | null, triggerEvent: () => void): void {
    if (this.editDialogComponent && id) {
      this.httpService.getById(this.url, id).subscribe((response: any) => {
        var additionalData;
        var dialogRef;
        if (this.url == environment.CLOTHES || this.url == environment.FOODS) {
          this.httpService
            .getAll(
              this.url == environment.CLOTHES
                ? environment.MATERIALS
                : environment.INGREDIENTS
            )
            .subscribe((data) => {
              additionalData = data;
              console.log(additionalData);
              dialogRef = this.dialog.open(
                this.editDialogComponent as ComponentType<any>,
                {
                  data: {
                    body: response,
                    httpService: this.httpService,
                    additionalData,
                  },
                }
              );
              dialogRef.componentInstance.triggerEvent.subscribe(() => {
                triggerEvent();
              });
            });
        } else {
          dialogRef = this.dialog.open(
            this.editDialogComponent as ComponentType<any>,
            {
              data: {
                body: response,
                httpService: this.httpService,
              },
            }
          );
          dialogRef.componentInstance.triggerEvent.subscribe(() => {
            triggerEvent();
          });
        }
      });
    }
  }

  determineComponentBasedOnUrl(url: string): void {
    this.url = url;

    if (url == environment.CLOTHES) {
      this.setEditDialogComponent(FormClothesComponent);
    } else if (url == environment.DEVICES) {
      this.setEditDialogComponent(FormDeviceComponent);
    } else if (url == environment.FOODS) {
      this.setEditDialogComponent(FormFoodComponent);
    } else if (url == environment.INGREDIENTS) {
      this.setEditDialogComponent(FormIngredientComponent);
    } else if (url == environment.MATERIALS) {
      this.setEditDialogComponent(FormMaterialComponent);
    }
  }
}
