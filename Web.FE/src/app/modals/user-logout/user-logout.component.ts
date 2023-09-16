import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Recipe} from "../../shared/objects/Recipe";
import {UserService} from "../../services/user.service";

@Component({
  selector: 'app-user-logout',
  templateUrl: './user-logout.component.html',
  styleUrls: ['./user-logout.component.scss']
})
export class UserLogoutComponent {

  constructor(public dialogRef: MatDialogRef<UserLogoutComponent>,
              public userService: UserService,
              @Inject(MAT_DIALOG_DATA) public data: Recipe) {
  }


  exit() {
    this.dialogRef.close();
    this.userService.authService.logout();
    this.userService.updateProfile();
  }
}
