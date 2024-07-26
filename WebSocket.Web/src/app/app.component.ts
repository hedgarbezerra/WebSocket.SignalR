import { Component } from '@angular/core';
import { slideInAnimation } from './common/animations/basic-animations';
import { LoadingSpinnerService } from './common/services/loading-spinner.service';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  animations: [slideInAnimation]
})
export class AppComponent {
  constructor(protected spinnerService: LoadingSpinnerService) {}

  prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
  }

  title = 'Movies';
}
