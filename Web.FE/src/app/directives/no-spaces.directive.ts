import { Directive } from '@angular/core';
import {Subscription} from "rxjs";
import {NgControl} from "@angular/forms";

@Directive({
  selector: '[noSpaces]'
})
export class NoSpacesDirective {
  sub: Subscription | undefined = new Subscription();

  constructor(
    private ngControl: NgControl) {
  }

  ngOnInit() {
    this.sub = this.ngControl.valueChanges?.subscribe(value => {
      let newValue = value.replace(" ", "");
      this.ngControl.control?.setValue((newValue || ''), {
        emitEvent: false
      })
    });
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }
}
