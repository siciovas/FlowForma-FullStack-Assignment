import { environment } from '../../environments/environment';
import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-device',
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
  templateUrl: './add-device.component.html',
  styleUrl: './add-device.component.css',
})
export class AddDeviceComponent {
  @Output() triggerEvent = new EventEmitter<void>();
  deviceForm: FormGroup;
  isEdit: boolean = false;

  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar
  ) {
    var body = data.body;
    this.isEdit = body !== undefined;
    this.deviceForm = this.fb.group({
      name: [(body && body.name) ?? '', Validators.required],
      description: [(body && body.description) ?? '', Validators.required],
      price: [
        (body && body.price) ?? 0,
        [Validators.required, Validators.min(1)],
      ],
      isElectronical: [
        (body && body.isElectronical.toString()) ?? '',
        Validators.required,
      ],
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
    if (this.deviceForm.valid) {
      const formData = this.deviceForm.value;

      if (!this.isEdit) {
        this.data.httpService.add(environment.DEVICES, formData).subscribe(
          () => {
            this.showSnackBar('Added device');
            this.triggerEvent.emit();
          },
          () => {
            this.showSnackBar('Failed to add device');
          }
        );
      } else {
        this.data.httpService
          .update(environment.DEVICES, formData, this.data.body.id)
          .subscribe(
            () => {
              this.showSnackBar('Updated device');
              this.triggerEvent.emit();
            },
            () => {
              this.showSnackBar('Failed to update device');
            }
          );
      }
    }
  }
}
