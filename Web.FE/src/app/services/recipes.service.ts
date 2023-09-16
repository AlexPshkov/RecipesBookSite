import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Recipe} from "../shared/objects/Recipe";
import {apiUrl} from "../app.component";
import {ImageLoaded} from "../shared/responses/ImageLoaded";
import {RecipeCreated} from "../shared/responses/RecipeCreated";
import {Tag} from "../shared/objects/secondary/Tag";

@Injectable({
  providedIn: 'root'
})
export class RecipesService {
  constructor(private http: HttpClient) {
  }

  get(recipeId: number): Observable<Recipe> {
    return this.http.get<Recipe>(`${apiUrl}/recipes/${recipeId}`);
  }

  getBestRecipe(): Observable<Recipe> {
    return this.http.get<Recipe>(`${apiUrl}/recipes/best-recipe/`);
  }

  getBestTags(amount: number = 5): Observable<Tag[]> {
    let params = new HttpParams();
    params = params.append('amount', amount);
    return this.http.get<Tag[]>(`${apiUrl}/recipes/best-tags/`, {params: params});
  }

  makeSearch(searchQuery: string, start: number, end: number): Observable<Recipe[]> {
    let params = new HttpParams();
    params = params.append('start', start);
    params = params.append('end', end);
    return this.http.get<Recipe[]>(`${apiUrl}/recipes/search/${searchQuery}`, {params: params});
  }

  remove(recipeId: number): Observable<any> {
    return this.http.delete<any>(`${apiUrl}/recipes/${recipeId}`);
  }

  uploadImage(image: File): Observable<ImageLoaded> {
    const data = new FormData();
    data.append("formFile", image);
    return this.http.post<ImageLoaded>(`${apiUrl}/images`, data);
  }

  uploadRecipe(recipe: Recipe): Observable<RecipeCreated> {
    return this.http.post<RecipeCreated>(`${apiUrl}/recipes`, recipe);
  }

  //====LIKES=====
  putLike(recipeId: number): Observable<Recipe> {
    return this.http.put<Recipe>(`${apiUrl}/recipes/like/${recipeId}`, {});
  }

  //====FAVORITES=====
  makeFavorite(recipeId: number): Observable<Recipe> {
    return this.http.put<Recipe>(`${apiUrl}/recipes/favorite/${recipeId}`, {});
  }
}
