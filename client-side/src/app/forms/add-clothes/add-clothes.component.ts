import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MaterialElement } from '../../components/materials/materials.component';
import { environment } from '../../environments/environment';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-clothes',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatDialogModule,
    CommonModule,
    MatSelectModule,
    MatButtonModule,
    FormsModule,
    MatInputModule,
    ReactiveFormsModule,
  ],
  templateUrl: './add-clothes.component.html',
  styleUrl: './add-clothes.component.css',
})
export class AddClothesComponent {
  @Output() triggerEvent = new EventEmitter<void>();
  clothesForm: FormGroup;
  materialsList: MaterialElement[] = [];
  isEdit: boolean = false;
  selectedValues: string[] = [];

  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar
  ) {
    var body = data.body;
    this.isEdit = body !== undefined;
    this.clothesForm = this.fb.group({
      name: [(body && body.name) ?? '', Validators.required],
      description: [(body && body.description) ?? '', Validators.required],
      price: [
        (body && body.price) ?? 0,
        [Validators.required, Validators.min(1)],
      ],
      size: [(body && body.size) ?? '', Validators.required],
      materials: [(body && body.materials) ?? [], Validators.required],
    });
  }

  ngOnInit(): void {
    this.materialsList = this.data.materials;
    if (this.data.additionalData) {
      this.materialsList = this.data.additionalData;
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
    if (this.clothesForm.valid) {
      const formData = this.clothesForm.value;

      if (!this.isEdit) {
        this.data.httpService.add(environment.CLOTHES, formData).subscribe(
          () => {
            this.showSnackBar('Added clothes');
            this.triggerEvent.emit();
          },
          () => {
            this.showSnackBar('Failed to add clothes');
          }
        );
      } else {
        this.data.httpService
          .update(environment.CLOTHES, formData, this.data.body.id)
          .subscribe(
            () => {
              this.showSnackBar('Updated clothes');
              this.triggerEvent.emit();
            },
            () => {
              this.showSnackBar('Failed to update clothes');
            }
          );
      }
    }
  }
}
