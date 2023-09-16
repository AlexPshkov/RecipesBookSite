import {Component} from '@angular/core';
import {NavigationService} from "../../services/navigation-service.service";

@Component({
  selector: 'back-button',
  templateUrl: './back-button.component.html',
  styleUrls: ['./back-button.component.scss']
})
export class BackButtonComponent {

  constructor(public navigationService: NavigationService) {
  }
}
