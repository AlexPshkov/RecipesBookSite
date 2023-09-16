import {Component, OnInit} from '@angular/core';
import {Recipe} from "../../../shared/objects/Recipe";
import {RecipesService} from "../../../services/recipes.service";
import {Router} from "@angular/router";

@Component({
  selector: 'best-recipe-block',
  templateUrl: './best-recipe-block.component.html',
  styleUrls: ['./best-recipe-block.component.scss']
})
export class BestRecipeBlockComponent implements OnInit {
  public recipeObject: Recipe | null = null;

  constructor(
    public recipesService: RecipesService,
    public router: Router) {
  }

  ngOnInit(): void {
    this.recipesService.getBestRecipe().subscribe({
      next: recipe => this.recipeObject = recipe,
      error: () => {
      }
    });
  }

  openRecipePage() {
    if (this.recipeObject == null) return;
    this.router.navigate(['/recipe', {recipeId: this.recipeObject.id}])
  }
}
