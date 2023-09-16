import {Component, OnInit} from '@angular/core';
import {RecipesService} from "../../services/recipes.service";
import {Recipe} from "../../shared/objects/Recipe";
import {ActivatedRoute, Router} from "@angular/router";
import {FormControl} from "@angular/forms";
import {loadRecipesAmount} from "../../app.component";

@Component({
  selector: 'app-recipes-page',
  templateUrl: './recipes-page.component.html',
  styleUrls: ['./recipes-page.component.scss']
})
export class RecipesPageComponent implements OnInit {

  public recipesList: Recipe[] = []
  public searchControl = new FormControl<string>("");
  public isMore: boolean = false;
  public startIndex: number = 1;
  public endIndex: number = loadRecipesAmount;


  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public recipesService: RecipesService) {

  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      const searchQuery: string = params['search'];
      if (searchQuery == null) {
        this.makeSearch("");
        return;
      }
      this.makeSearch(searchQuery);
    });
  }

  addRecipe() {
    this.router.navigate(['add-recipe']);
  }

  setValueToSearchField(value: string) {
    this.searchControl.setValue(value, { emitEvent: false });
  }

  makeSearch(value: string) {
    this.startIndex = 1;
    this.endIndex = loadRecipesAmount;

    this.recipesService.makeSearch(value, this.startIndex, this.endIndex).subscribe(recipes => {
      this.setValueToSearchField(value);
      this.recipesList = recipes;
      this.isMore = recipes.length == loadRecipesAmount;
    });
  }


  loadMore() {
    this.startIndex += loadRecipesAmount;
    this.endIndex += loadRecipesAmount;
    let searchQuery = this.searchControl.value;
    if (searchQuery == null) return;

    this.recipesService.makeSearch(searchQuery, this.startIndex, this.endIndex).subscribe(recipes => {
      recipes.forEach(recipe => this.recipesList.push(recipe));
      this.isMore = recipes.length == loadRecipesAmount;
    });
  }

}
