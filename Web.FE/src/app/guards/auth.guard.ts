import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {AuthService} from "../services/auth.service";
import {MatDialog} from "@angular/material/dialog";
import {RegisterDialogComponent} from "../modals/register-dialog/register-dialog.component";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    public authService: AuthService,
    public dialog: MatDialog,
    public router: Router) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (!this.authService.isAuthenticated()) {
      this.dialog.open(RegisterDialogComponent);
      // this.router.navigate(['']).then(() => );
      return false;
    }
    return true;
  }

}
