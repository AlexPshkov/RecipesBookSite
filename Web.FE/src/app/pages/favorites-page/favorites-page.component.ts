import {Component, OnInit} from '@angular/core';
import {Recipe} from "../../shared/objects/Recipe";
import {UserService} from "../../services/user.service";
import {loadRecipesAmount} from "../../app.component";

@Component({
  selector: 'app-favorites-page',
  templateUrl: './favorites-page.component.html',
  styleUrls: ['./favorites-page.component.scss']
})
export class FavoritesPageComponent implements OnInit {

  public isMore: boolean = false;
  public startIndex: number = 1;
  public endIndex: number = loadRecipesAmount;
  public favorites: Recipe[] = [];

  constructor(
    public userService: UserService
  ) {
  }

  ngOnInit(): void {
    this.loadFavorites();
  }

  loadFavorites() {
    this.startIndex = 1;
    this.endIndex = loadRecipesAmount;
    this.userService.getFavorites(this.startIndex, this.endIndex).subscribe(recipes => {
      this.favorites = recipes;
      this.isMore = recipes.length == loadRecipesAmount;
    });
  }

  loadMore() {
    this.startIndex += loadRecipesAmount;
    this.endIndex += loadRecipesAmount;

    this.userService.getFavorites(this.startIndex, this.endIndex).subscribe(recipes => {
      recipes.forEach(recipe => this.favorites.push(recipe));
      this.isMore = recipes.length == loadRecipesAmount;
    });
  }

}
