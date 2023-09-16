import {Component} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from "@angular/forms";
import {Step} from "../../../shared/objects/secondary/Step";

@Component({
  selector: 'steps-input',
  templateUrl: './steps-input.component.html',
  styleUrls: ['./steps-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: StepsInputComponent
    }]
})
export class StepsInputComponent implements ControlValueAccessor {
  steps: Step[] = [];

  onChange = (steps: Step[]) => {
  };
  onTouched = () => {
  };
  touched = false;
  disabled = false;

  add(): void {
    this.steps.push({id: 0, description: "", recipeId: 0});
    this.onChange(this.steps);
  }

  remove(number: number): void {
    this.steps.splice(number, 1)
    this.onChange(this.steps);
  }

  onAnyChange() {
    this.onChange(this.steps);
  }

  writeValue(steps: Step[]) {
    this.steps = steps;
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
