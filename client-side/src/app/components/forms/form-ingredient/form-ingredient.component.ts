import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from '../../../environments/environment';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-form-ingredient',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    CommonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './form-ingredient.component.html',
  styleUrl: './form-ingredient.component.css',
})
export class FormIngredientComponent {
  @Output() triggerEvent = new EventEmitter<void>();
  ingredientForm: FormGroup;
  isEdit: boolean = false;

  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar
  ) {
    var body = data.body;
    this.isEdit = body !== undefined;
    this.ingredientForm = this.fb.group({
      name: [(body && body.name) ?? '', Validators.required],
    });
  }

  private showSnackBar(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }

  onSubmit() {
    if (this.ingredientForm.valid) {
      const formData = this.ingredientForm.value;

      if (!this.isEdit) {
        this.data.httpService.add(environment.INGREDIENTS, formData).subscribe(
          () => {
            this.showSnackBar('Added ingredient');
            this.triggerEvent.emit();
          },
          () => {
            this.showSnackBar('Failed to add ingredient');
          }
        );
      } else {
        this.data.httpService
          .update(environment.INGREDIENTS, formData, this.data.body.id)
          .subscribe(
            () => {
              this.showSnackBar('Updated ingredient');
              this.triggerEvent.emit();
            },
            () => {
              this.showSnackBar('Failed to update ingredient');
            }
          );
      }
    }
  }
}
