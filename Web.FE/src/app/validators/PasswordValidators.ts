import {AbstractControl, ValidationErrors, ValidatorFn} from "@angular/forms";

/**
 * Checks if current filed value matches with 'password' field value
 */
export function checkPasswordMatch(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const controls = control?.parent?.controls as { [key: string]: AbstractControl; };
    if (!controls) return null;

    const forbidden = (controls['password'].value != control.value);
    return forbidden ? {checkPasswordMatch: true} : null;
  };
}
