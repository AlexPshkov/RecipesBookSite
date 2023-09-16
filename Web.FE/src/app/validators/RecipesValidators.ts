import {AbstractControl, ValidationErrors, ValidatorFn} from "@angular/forms";
import {Ingredient} from "../shared/objects/secondary/Ingredient";
import {Step} from "../shared/objects/secondary/Step";


export function checkIngredients(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const ingredientsArray: Ingredient[] = control.value;
    let counter: number = 0;

    for (let ingredient of ingredientsArray) {
      if (ingredient.title.length == 0 || ingredient.description.length == 0) continue;
      counter++
    }
    if (counter != ingredientsArray.length) return {emptyIngredients: true};

    return null;
  };
}

export function checkSteps(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const stepsArray: Step[] = control.value;
    let counter: number = 0;

    for (let step of stepsArray) {
      if (step.description.length == 0) continue;
      counter++
    }
    if (counter == 0) return {emptySteps: true};

    return null;
  };
}



