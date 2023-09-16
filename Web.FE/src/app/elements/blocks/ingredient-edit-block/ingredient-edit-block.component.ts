import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Ingredient} from "../../../shared/objects/secondary/Ingredient";
import {animate, style, transition, trigger} from "@angular/animations";

@Component({
  selector: 'ingredient-edit-block[number][ingredient]',
  templateUrl: './ingredient-edit-block.component.html',
  styleUrls: ['./ingredient-edit-block.component.scss'],
  animations: [
    trigger('fadeAnimation', [
      transition('void => *', [
        style({opacity: 0}),
        animate('0.15s', style({opacity: 1})),
      ])
    ]),
  ],
})
export class IngredientEditBlockComponent {

  @Input()
  number: number = 0;

  @Input()
  public ingredient!: Ingredient;

  @Output()
  blockRemove = new EventEmitter<number>();

  constructor() {
  }

  removeBlock() {
    this.blockRemove.emit(this.number);
  }

}
