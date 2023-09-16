import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from "@angular/forms";
import {Tag} from "../../../shared/objects/secondary/Tag";
import {RecipesService} from "../../../services/recipes.service";

@Component({
  selector: 'search-block',
  templateUrl: './search-block.component.html',
  styleUrls: ['./search-block.component.scss'],
})
export class SearchBlockComponent implements ControlValueAccessor, OnInit {

  @Input()
  searchString: string | null = "";

  public tags: Tag[] = [];

  onChange = (searchString: string) => {};
  onTouched = () => {};
  touched = false;
  disabled = false;

  @Output()
  public searchEvent = new EventEmitter<string>();

  @Output()
  public textChange = new EventEmitter<string>();

  constructor(public recipesService: RecipesService) {
  }

  ngOnInit(): void {
    this.recipesService.getBestTags().subscribe(tags => {
      this.tags = tags;
    });
  }

  public onSubmit(value: string) {
    this.searchEvent.emit(value);
    this.onChange(value);
  }

  public onValueChange(value: string) {
    this.textChange.emit(value);
    this.onChange(value);
  }

  writeValue(searchString: string) {
    this.searchString = searchString;
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

