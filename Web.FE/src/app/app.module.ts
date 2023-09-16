import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatIconModule} from "@angular/material/icon";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {RouterModule, Routes} from "@angular/router";
import {MainPageComponent} from './pages/main-page/main-page.component';
import {RecipesPageComponent} from './pages/recipes-page/recipes-page.component';
import {MatRippleModule} from "@angular/material/core";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {FavoritesPageComponent} from './pages/favorites-page/favorites-page.component';
import {RecipeBlockComponent} from './elements/blocks/recipe-block/recipe-block.component';
import {JwtModule} from "@auth0/angular-jwt";
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {RegisterDialogComponent} from './modals/register-dialog/register-dialog.component';
import {MatDialogModule} from "@angular/material/dialog";
import {LoginDialogComponent} from './modals/login-dialog/login-dialog.component';
import {TOKEN_KEY} from "./services/auth.service";
import {ProfilePageComponent} from './pages/profile-page/profile-page.component';
import {AuthGuard} from "./guards/auth.guard";
import {ErrorSnackbarComponent} from './modals/error-snackbar/error-snackbar.component';
import {MAT_SNACK_BAR_DEFAULT_OPTIONS, MatSnackBarModule} from "@angular/material/snack-bar";
import {OnlyLowerCaseDirective} from './directives/only-lower-case.directive';
import {AuthRequiredComponent} from './modals/auth-required/auth-required.component';
import {AddRecipeGuard} from "./guards/add-recipe.guard";
import {AddRecipePageComponent} from './pages/add-recipe-page/add-recipe-page.component';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatSelectModule} from "@angular/material/select";
import {CustomSvgComponent} from './elements/custom-svg/custom-svg.component';
import {ImageUploadComponent} from './elements/inputs/image-upload/image-upload.component';
import {StepEditBlockComponent} from './elements/blocks/step-edit-block/step-edit-block.component';
import {IngredientEditBlockComponent} from './elements/blocks/ingredient-edit-block/ingredient-edit-block.component';
import {RecipePageComponent} from './pages/recipe-page/recipe-page.component';
import {NlToBrPipe} from './pipes/nl-to-br.pipe';
import {RecipeDeleteDialogComponent} from './modals/recipe-delete-dialog/recipe-delete-dialog.component';
import {TagsInputComponent} from './elements/inputs/tags-input/tags-input.component';
import {MatChipsModule} from "@angular/material/chips";
import {IngredientsInputComponent} from './elements/inputs/ingredients-input/ingredients-input.component';
import {StepsInputComponent} from './elements/inputs/steps-input/steps-input.component';
import {ImageUrlPipe} from './pipes/image-url.pipe';
import {BestRecipeBlockComponent} from './elements/blocks/best-recipe-block/best-recipe-block.component';
import {MatInputModule} from "@angular/material/input";
import {BackButtonComponent} from './elements/back-button/back-button.component';
import {HttpErrorsInterceptor} from "./intercepters/http-errors.interceptor";
import {UserLogoutComponent} from './modals/user-logout/user-logout.component';
import {SearchBlockComponent} from './elements/blocks/search-block/search-block.component';
import {AutosizeModule} from "ngx-autosize";
import { NoSpacesDirective } from './directives/no-spaces.directive';
import { TrimContentDirective } from './directives/trim-content.directive';
import {MatSidenavModule} from "@angular/material/sidenav";

export const routes: Routes = [
  {path: "main-page", component: MainPageComponent, title: "Главная"},
  {path: "recipes", component: RecipesPageComponent, title: "Рецепты"},
  {path: "favorite", component: FavoritesPageComponent, title: "Избранное", canActivate: [AuthGuard]},
  {path: "profile", component: ProfilePageComponent, title: "Профиль", canActivate: [AuthGuard]},
  {path: "add-recipe", component: AddRecipePageComponent, title: "Добавить рецепт", canActivate: [AddRecipeGuard]},
  {path: "recipe", component: RecipePageComponent, title: "Рецепт"},
  {path: '**', redirectTo: 'main-page'}
];

/**
 * Token getter implementation
 */
export function tokenGetter() {
  return localStorage.getItem(TOKEN_KEY)
}

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    RecipesPageComponent,
    FavoritesPageComponent,
    RecipeBlockComponent,
    RegisterDialogComponent,
    LoginDialogComponent,
    ProfilePageComponent,
    ErrorSnackbarComponent,
    OnlyLowerCaseDirective,
    AuthRequiredComponent,
    AddRecipePageComponent,
    CustomSvgComponent,
    ImageUploadComponent,
    StepEditBlockComponent,
    IngredientEditBlockComponent,
    RecipePageComponent,
    NlToBrPipe,
    RecipeDeleteDialogComponent,
    TagsInputComponent,
    IngredientsInputComponent,
    StepsInputComponent,
    ImageUrlPipe,
    BestRecipeBlockComponent,
    BackButtonComponent,
    UserLogoutComponent,
    SearchBlockComponent,
    NoSpacesDirective,
    TrimContentDirective
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatSnackBarModule,
    MatToolbarModule,
    MatButtonModule,
    RouterModule.forRoot(
      routes,
      {scrollPositionRestoration: 'enabled'}),
    JwtModule.forRoot({
      config: {
        allowedDomains: [ new RegExp( "\\S+") ],
        tokenGetter: tokenGetter
      }
    }),
    MatRippleModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    HttpClientModule,
    MatDialogModule,
    FormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatChipsModule,
    MatInputModule,
    AutosizeModule,
    MatSidenavModule,
  ],
  providers: [
    {
      provide: MAT_SNACK_BAR_DEFAULT_OPTIONS,
      useValue: {
        duration: 5000
      }
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorsInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
