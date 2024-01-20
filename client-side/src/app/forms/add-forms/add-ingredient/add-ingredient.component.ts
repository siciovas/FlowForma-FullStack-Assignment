import { Component } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-ingredient',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    CommonModule,
    FormsModule
  ],
  templateUrl: './add-ingredient.component.html',
  styleUrl: './add-ingredient.component.css',
})
export class AddIngredientComponent {}
