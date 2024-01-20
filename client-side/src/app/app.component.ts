import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { EditionService } from './services/edition.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, MatCardModule],
  styleUrl: './app.component.css',
  templateUrl: './app.component.html',
  providers: [EditionService],
})
export class AppComponent {
  companyName = 'FlowForma';
}
