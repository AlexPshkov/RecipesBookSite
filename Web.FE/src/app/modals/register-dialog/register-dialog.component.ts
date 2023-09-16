import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {LoginDialogComponent} from "../login-dialog/login-dialog.component";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {checkPasswordMatch} from "../../validators/PasswordValidators";


@Component({
  selector: 'app-register-dialog',
  templateUrl: './register-dialog.component.html',
  styleUrls: ['./register-dialog.component.scss']
})
export class RegisterDialogComponent implements OnInit {


  public form: FormGroup = new FormGroup({
    name: new FormControl("", Validators.required),
    login: new FormControl("", Validators.required),
    password: new FormControl("", [Validators.minLength(8), Validators.required]),
    password_repeat: new FormControl("", checkPasswordMatch()),
  });

  constructor(
    public userService: UserService,
    public router: Router,
    public dialog: MatDialog,
    public snack: MatSnackBar,
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  ngOnInit(): void {

  }

  isSuchErrors(controlName: string, errorNames: string[]): boolean {
    const control = this.form.controls[controlName];
    for (let errorName of errorNames)
      if (control.errors?.[errorName] != null) return true;
    return false;
  }


  onSubmit() {
    this.userService.authService.register(this.form.value).subscribe({
      next: token => {
        this.userService.authService.saveToken(token);
        this.userService.updateProfile();
        this.dialogRef.close();
        window.location.reload();
      }
    });
  }

  openLoginDialog() {
    this.dialogRef.close();
    this.dialog.open(LoginDialogComponent);
  }
}
