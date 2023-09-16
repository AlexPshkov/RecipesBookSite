import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {LoginDialogComponent} from "../login-dialog/login-dialog.component";
import {RegisterDialogComponent} from "../register-dialog/register-dialog.component";

@Component({
  selector: 'app-auth-required',
  templateUrl: './auth-required.component.html',
  styleUrls: ['./auth-required.component.scss']
})
export class AuthRequiredComponent implements OnInit {

  constructor(public dialog: MatDialog, public dialogRef: MatDialogRef<LoginDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: string) {
  }

  ngOnInit(): void {
  }

  openRegisterDialog() {
    this.dialogRef.close();
    this.dialog.open(RegisterDialogComponent);
  }

  openLoginDialog() {
    this.dialogRef.close();
    this.dialog.open(LoginDialogComponent);
  }

}
