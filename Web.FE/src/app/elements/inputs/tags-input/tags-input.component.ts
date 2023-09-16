import {Component} from '@angular/core';
import {Tag} from "../../../shared/objects/secondary/Tag";
import {ENTER} from "@angular/cdk/keycodes";
import {MatChipInputEvent} from "@angular/material/chips";
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from "@angular/forms";

@Component({
  selector: 'tags-input[formControlName]',
  templateUrl: './tags-input.component.html',
  styleUrls: ['./tags-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: TagsInputComponent
    }]
})
export class TagsInputComponent implements ControlValueAccessor {
  readonly separatorKeysCodes = [ENTER] as const;

  tags: Tag[] = [];

  onChange = (tags: Tag[]) => {
  };
  onTouched = () => {
  };
  touched = false;
  disabled = false;

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value) {
      if (this.isTagsLimit()) return;
      this.tags.push({id: 0, tagName: event.value})
    }
    event.chipInput!.clear();
    this.onChange(this.tags);
  }

  isTagsLimit(): boolean {
    return (this.tags.length > 10);
  }

  remove(fruit: Tag): void {
    const index = this.tags.indexOf(fruit);

    if (index >= 0) {
      this.tags.splice(index, 1);
    }
    this.onChange(this.tags);
  }

  writeValue(tag: Tag[]) {
    this.tags = tag;
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
