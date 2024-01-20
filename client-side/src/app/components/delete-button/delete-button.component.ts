import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { HttpService } from '../../services/http.service';

@Component({
  selector: 'app-delete-button',
  standalone: true,
  imports: [MatButtonModule, MatIconModule,DeleteDialogComponent],
  templateUrl: './delete-button.component.html',
  styleUrl: './delete-button.component.css',
})
export class DeleteButtonComponent {
  @Input() url: string = '';
  @Input() id: number = 0;

  constructor(public dialog: MatDialog, private httpService: HttpService) {}
  openDeleteDialog() {
    this.dialog.open(DeleteDialogComponent, {
      data: {
        apiName: this.url,
        id: this.id,
        httpService: this.httpService
      }
    });
  }
}
