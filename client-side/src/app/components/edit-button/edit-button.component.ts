import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { EditionService } from '../../services/edition.service';

@Component({
  selector: 'app-edit-button',
  standalone: true,
  imports: [MatButtonModule, MatIconModule],
  templateUrl: './edit-button.component.html',
  styleUrl: './edit-button.component.css',
})
export class EditButtonComponent {
  @Input() id: number | null = null;
  constructor(private editionService: EditionService) {}
  openEditDialog(): void {
    this.editionService.openEditDialog(this.id);
  }
}
