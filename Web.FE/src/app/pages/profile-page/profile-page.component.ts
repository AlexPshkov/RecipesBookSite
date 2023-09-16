import {Component, OnInit} from '@angular/core';
import {UserService} from "../../services/user.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Recipe} from "../../shared/objects/Recipe";
import {UserStatistic} from "../../shared/objects/UserStatistic";
import {loadRecipesAmount} from "../../app.component";

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent implements OnInit {

  public form: FormGroup = new FormGroup({
    id: new FormControl<string>(""),
    role: new FormControl<string>(""),
    userName: new FormControl<string>({value: "", disabled: true}, [
      Validators.required,
      Validators.minLength(3)]),
    login: new FormControl<string>({value: "", disabled: true}, [
      Validators.required,
      Validators.minLength(3)]),
    password: new FormControl<string>({value: "", disabled: true}, Validators.minLength(8)),
    description: new FormControl<string>({value: "", disabled: true}),
  });

  public editMode: boolean = false;
  public hidePassword: boolean = true;

  public isMore: boolean = false;
  public startIndex: number = 1;
  public endIndex: number = loadRecipesAmount;

  public recipesList: Recipe[] = [];
  public statistic: UserStatistic = {
    createdRecipesAmount: 0,
    likedRecipesAmount: 0,
    favoritesRecipesAmount: 0
  };

  constructor(public userService: UserService) {
  }

  ngOnInit(): void {
    this.loadUser();
    this.loadUserStatistic();
    this.loadCreatedRecipes();
  }

  loadUser() {
    this.userService.getUserData().subscribe(userData => {
      this.form.patchValue(userData);
    });
  }

  loadUserStatistic() {
    this.userService.getUserStatistic().subscribe(statistic => {
      this.statistic = statistic;
    });
  }

  onSubmit() {
    if (this.form.invalid) return;
    this.userService.saveUserData(this.form.value).subscribe({
      next: token => {
        this.userService.authService.saveToken(token);
        this.userService.updateProfile();
        this.loadCreatedRecipes();
        this.toggleEditMode();
      }
    });
  }

  toggleEditMode() {
    this.editMode = !this.editMode;
    if (!this.editMode) {
      this.loadUser();
      this.form.disable();
    } else {
      this.form.enable();
    }
  }

  loadCreatedRecipes() {
    this.startIndex = 1;
    this.endIndex = loadRecipesAmount;

    this.userService.getCreatedRecipes(this.startIndex, this.endIndex).subscribe(createdRecipes => {
      this.recipesList = createdRecipes;
      this.isMore = createdRecipes.length == loadRecipesAmount;
    });
  }

  loadMore() {
    this.startIndex += loadRecipesAmount;
    this.endIndex += loadRecipesAmount;

    this.userService.getCreatedRecipes(this.startIndex, this.endIndex).subscribe(createdRecipes => {
      createdRecipes.forEach(recipe => this.recipesList.push(recipe));
      this.isMore = createdRecipes.length == loadRecipesAmount;
    });
  }
}
