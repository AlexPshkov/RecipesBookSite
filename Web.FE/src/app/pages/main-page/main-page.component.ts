import {Component, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {RegisterDialogComponent} from "../../modals/register-dialog/register-dialog.component";
import {Router} from "@angular/router";
import {UserService} from "../../services/user.service";

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit {

  constructor(
    public router: Router,
    public userService: UserService,
    public dialog: MatDialog) {
  }

  ngOnInit(): void {
  }

  openRegisterDialog() {
    this.dialog.open(RegisterDialogComponent, {
      data: {}
    });
  }

  addRecipe() {
    this.router.navigate(['add-recipe']);
  }

  makeSearch(value: string) {
    this.router.navigate(['recipes', {search: value}])
  }

}
