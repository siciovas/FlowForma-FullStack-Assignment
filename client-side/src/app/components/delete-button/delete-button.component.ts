import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-button',
  standalone: true,
  imports: [MatButtonModule, MatIconModule,DeleteDialogComponent],
  templateUrl: './delete-button.component.html',
  styleUrl: './delete-button.component.css',
})
export class DeleteButtonComponent {
  @Input() url: string = '';

  constructor(public dialog: MatDialog) {}
  openDeleteDialog() {
    this.dialog.open(DeleteDialogComponent, {
      data: {
        url: this.url
      }
    });
  }
}
