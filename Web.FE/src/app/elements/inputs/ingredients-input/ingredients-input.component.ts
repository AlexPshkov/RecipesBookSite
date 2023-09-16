import {Component} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from "@angular/forms";
import {Ingredient} from "../../../shared/objects/secondary/Ingredient";

@Component({
  selector: 'ingredients-input',
  templateUrl: './ingredients-input.component.html',
  styleUrls: ['./ingredients-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: IngredientsInputComponent
    }]
})
export class IngredientsInputComponent implements ControlValueAccessor {
  public ingredients: Ingredient[] = [];

  onChange = (ingredients: Ingredient[]) => {};
  onTouched = () => {};
  touched = false;
  disabled = false;


  add(): void {
    this.ingredients.push({id: 0, title: "", description: "", recipeId: 0})
    this.onChange(this.ingredients);
  }

  remove(number: number): void {
    this.ingredients.splice(number, 1);
    this.onChange(this.ingredients);
  }

  onAnyChange() {
    this.onChange(this.ingredients);
  }

  writeValue(ingredients: Ingredient[]) {
    this.ingredients = ingredients;
  }

  registerOnChange(onChange: any) {
    this.onChange = onChange;
  }

  registerOnTouched(onTouched: any) {
    this.onTouched = onTouched;
  }

  markAsTouched() {
    if (!this.touched) {
      this.onTouched();
      this.touched = true;
    }
  }

  setDisabledState(disabled: boolean) {
    this.disabled = disabled;
  }
}
