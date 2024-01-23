import { Component, Input } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { EditButtonComponent } from '../buttons/edit-button/edit-button.component';
import { DeleteButtonComponent } from '../buttons/delete-button/delete-button.component';

export interface TableColumns {
  name: string;
  displayName: string;
}

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [
    MatTableModule,
    MatIconModule,
    MatFormFieldModule,
    CommonModule,
    MatButtonModule,
    EditButtonComponent,
    DeleteButtonComponent,
  ],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css',
})
export class TableComponent {
  @Input() displayedColumns: TableColumns[] = [];
  @Input() dataSource: any[] = [];
  @Input() url: string = '';
  @Input() triggerEvent: () => void = () => {};

  getDisplayedColumns(): string[] {
    return this.displayedColumns.map((column) => column.displayName);
  }
}
