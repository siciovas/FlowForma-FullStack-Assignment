import { Component } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { EditionService } from '../../../services/edition.service';

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
  ],
  templateUrl: './add-clothes.component.html',
  styleUrl: './add-clothes.component.css',
})
export class AddClothesComponent {
}
