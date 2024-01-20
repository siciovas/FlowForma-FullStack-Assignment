import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { EditionService } from './services/edition.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpService } from './services/http.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, MatCardModule, HttpClientModule],
  styleUrl: './app.component.css',
  templateUrl: './app.component.html',
  providers: [EditionService, HttpService],
})
export class AppComponent {
  companyName = 'FlowForma';
}
