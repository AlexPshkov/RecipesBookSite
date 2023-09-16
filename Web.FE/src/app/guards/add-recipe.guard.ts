import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {MatDialog} from "@angular/material/dialog";
import {AuthService} from "../services/auth.service";
import {AuthRequiredComponent} from "../modals/auth-required/auth-required.component";

@Injectable({
  providedIn: 'root'
})
export class AddRecipeGuard implements CanActivate {

  constructor(
    public dialog: MatDialog,
    public authService: AuthService) {

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (!this.authService.isAuthenticated()) {
      this.dialog.open(AuthRequiredComponent, {data: "Добавлять рецепты могут только зарегистрированные пользователи."});
      return false;
    }
    return true;
  }

}
