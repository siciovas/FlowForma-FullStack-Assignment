import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from '../../environments/environment';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-material',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatDialogModule,
    CommonModule,
    MatButtonModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './add-material.component.html',
  styleUrl: './add-material.component.css',
})
export class AddMaterialComponent {
  @Output() triggerEvent = new EventEmitter<void>();
  materialForm: FormGroup;
  isEdit: boolean = false;

  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar
  ) {
    var body = data.body;
    this.isEdit = body !== undefined;
    this.materialForm = this.fb.group({
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
    if (this.materialForm.valid) {
      const formData = this.materialForm.value;

      if (!this.isEdit) {
        this.data.httpService.add(environment.MATERIALS, formData).subscribe(
          () => {
            this.showSnackBar('Added material');
            this.triggerEvent.emit();
          },
          () => {
            this.showSnackBar('Failed to add material');
          }
        );
      } else {
        this.data.httpService
          .update(environment.MATERIALS, formData, this.data.body.id)
          .subscribe(
            () => {
              this.showSnackBar('Updated material');
              this.triggerEvent.emit();
            },
            () => {
              this.showSnackBar('Failed to update material');
            }
          );
      }
    }
  }
}
