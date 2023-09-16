import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Recipe} from "../../../shared/objects/Recipe";
import {MatDialog} from "@angular/material/dialog";
import {RecipesService} from "../../../services/recipes.service";
import {Router} from "@angular/router";
import {HttpErrorResponse} from "@angular/common/http";
import {AuthRequiredComponent} from "../../../modals/auth-required/auth-required.component";

@Component({
  selector: 'recipe-block[recipeObject]',
  templateUrl: './recipe-block.component.html',
  styleUrls: ['./recipe-block.component.scss']
})
export class RecipeBlockComponent implements OnInit {
  @Input()
  public recipeObject!: Recipe;

  @Output()
  public recipeChange = new EventEmitter<Recipe>();

  constructor(
    public recipesService: RecipesService,
    public router: Router,
    public dialog: MatDialog) {
  }

  ngOnInit(): void {
  }

  openRecipePage() {
    this.router.navigate(['/recipe', {recipeId: this.recipeObject.id}])
  }

  likeButton() {
    this.recipesService.putLike(this.recipeObject.id).subscribe({
        next: recipe => {
          this.recipeObject = recipe;
          this.recipeChange.emit(recipe);
        },
        error: err => this.handleError(err, "Ставить лайки могут только авторизованны пользователи")
      }
    );
  }

  favoriteButton() {
    this.recipesService.makeFavorite(this.recipeObject.id).subscribe({
      next: recipe => {
        this.recipeObject = recipe;
        this.recipeChange.emit(recipe);
      },
      error: err => this.handleError(err, "Добавлять в избранное могут только авторизованны пользователи")
    });
  }

  handleError(error: HttpErrorResponse, message: string) {
    if (error.status != 401) return;
    this.dialog.open(AuthRequiredComponent, {data: message})
  }

  makeSearch(value: string) {
    this.router.navigate(['recipes', {search: value}])
  }
}
