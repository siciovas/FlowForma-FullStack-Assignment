import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-delete-dialog',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule],
  templateUrl: './delete-dialog.component.html',
})
export class DeleteDialogComponent {
  @Output() triggerEvent = new EventEmitter<void>();

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar
  ) {}

  private showSnackBar(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }

  deleteData(): void {
    this.data.httpService.delete(this.data.apiName, this.data.id).subscribe(
      () => {
        this.showSnackBar('Deletion successful');
        this.triggerEvent.emit();
      },
      () => {
        this.showSnackBar('Deletion failed');
      }
    );
  }
}
