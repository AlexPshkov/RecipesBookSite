import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Step} from "../../../shared/objects/secondary/Step";
import {animate, style, transition, trigger} from "@angular/animations";

@Component({
  selector: 'step-edit-block[number][step]',
  templateUrl: './step-edit-block.component.html',
  styleUrls: ['./step-edit-block.component.scss'],
  animations: [
    trigger('fadeAnimation', [
      transition('void => *', [
        style({opacity: 0}),
        animate('0.15s', style({opacity: 1})),
      ])
    ]),
  ],
})
export class StepEditBlockComponent {

  @Input()
  number: number = 0;

  @Input()
  public step: Step | undefined;

  @Output()
  blockRemove = new EventEmitter<number>();

  constructor() {
  }

  removeBlock() {
    this.blockRemove.emit(this.number);
  }

}
