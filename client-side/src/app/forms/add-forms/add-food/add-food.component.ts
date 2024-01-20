import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-food',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatDialogModule,
    CommonModule,
    MatSelectModule,
    MatRadioModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './add-food.component.html',
  styleUrl: './add-food.component.css',
})
export class AddFoodComponent {}
