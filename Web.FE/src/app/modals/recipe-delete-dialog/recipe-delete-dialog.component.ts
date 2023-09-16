import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Recipe} from "../../shared/objects/Recipe";
import {RecipesService} from "../../services/recipes.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-recipe-delete-dialog',
  templateUrl: './recipe-delete-dialog.component.html',
  styleUrls: ['./recipe-delete-dialog.component.scss']
})
export class RecipeDeleteDialogComponent {

  constructor(public dialogRef: MatDialogRef<RecipeDeleteDialogComponent>,
              public router: Router,
              public recipesService: RecipesService,
              @Inject(MAT_DIALOG_DATA) public data: Recipe) {
  }


  removeRecipe() {
    this.recipesService.remove(this.data.id).subscribe(res => {
      this.router.navigate(['recipes']).then(() => this.dialogRef.close());
    });
  }


}
