import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { EditionService } from './services/edition.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpService } from './services/http.service';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, MatCardModule, HttpClientModule],
  styleUrl: './app.component.css',
  templateUrl: './app.component.html',
  providers: [EditionService, HttpService, MatSnackBarModule],
})
export class AppComponent {
  companyName = 'FlowForma';
}
