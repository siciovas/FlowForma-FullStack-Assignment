import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatDialogModule, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from '../../../environments/environment';
import { ReactiveFormsModule } from '@angular/forms';
import { IngredientElement } from '../../ingredients/ingredients.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-form-food',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatDialogModule,
    CommonModule,
    MatSelectModule,
    MatRadioModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './form-food.component.html',
  styleUrl: './form-food.component.css',
})
export class FormFoodComponent {
  @Output() triggerEvent = new EventEmitter<void>();
  foodForm: FormGroup;
  ingredientsList: IngredientElement[] = [];
  isEdit: boolean = false;
  selectedValues: string[] = [];

  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar
  ) {
    var body = data.body;
    this.isEdit = body !== undefined;
    this.foodForm = this.fb.group({
      name: [(body && body.name) ?? '', Validators.required],
      description: [(body && body.description) ?? '', Validators.required],
      price: [
        (body && body.price) ?? 0,
        [Validators.required, Validators.min(1)],
      ],
      size: [(body && body.size) ?? '', Validators.required],
      ingredients: [(body && body.ingredients) ?? [], Validators.required],
      calories: [
        (body && body.calories) ?? 0,
        [Validators.required, Validators.min(1)],
      ],
      isVegetarian: [
        (body && body.isVegetarian.toString()) ?? '',
        Validators.required,
      ],
      isVegan: [(body && body.isVegan.toString()) ?? '', Validators.required],
    });
  }

  ngOnInit(): void {
    this.ingredientsList = this.data.ingredients;
    if (this.data.additionalData) {
      this.ingredientsList = this.data.additionalData;
      this.selectedValues = this.data.additionalData;
    }
  }

  private showSnackBar(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }

  onSubmit() {
    if (this.foodForm.valid) {
      const formData = this.foodForm.value;

      if (!this.isEdit) {
        this.data.httpService.add(environment.FOODS, formData).subscribe(
          () => {
            this.showSnackBar('Added food');
            this.triggerEvent.emit();
          },
          () => {
            this.showSnackBar('Failed to add food');
          }
        );
      } else {
        this.data.httpService
          .update(environment.FOODS, formData, this.data.body.id)
          .subscribe(
            () => {
              this.showSnackBar('Updated food');
              this.triggerEvent.emit();
            },
            () => {
              this.showSnackBar('Failed to update food');
            }
          );
      }
    }
  }
}
