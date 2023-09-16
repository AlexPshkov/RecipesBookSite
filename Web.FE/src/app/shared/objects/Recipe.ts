import {Tag} from "./secondary/Tag";
import {Ingredient} from "./secondary/Ingredient";
import {Step} from "./secondary/Step";

export interface Recipe {
  id: number;
  recipeName: string;
  recipeDescription: string;
  imagePath: string;
  requiredTime: string;
  servingsAmount: string;
  userLogin: string;
  favoritesAmount: number;
  likesAmount: number;
  isCreator: boolean;
  isLiked: boolean;
  isFavorite: boolean;
  tags: Array<Tag>;
  ingredients: Array<Ingredient>;
  steps: Array<Step>;
}
