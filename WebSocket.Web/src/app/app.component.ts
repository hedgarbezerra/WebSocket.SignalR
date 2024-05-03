import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { routesAnimations } from './common/animations/basic-animations';
import { LoadingSpinnerService } from './common/services/loading-spinner.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  animations: [routesAnimations]
})
export class AppComponent implements OnInit {

  constructor(protected spinnerService: LoadingSpinnerService) {}

  ngOnInit() {
  }

  prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
  }

  title = 'Movie tickets';
}
