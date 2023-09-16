import {Component, Input, OnInit} from '@angular/core';
import {SafeHtml} from "@angular/platform-browser";
import {ExternalHtmlService} from "../../services/external-html.service";
import {Observable} from "rxjs";

@Component({
  selector: 'custom-svg[name]',
  template: '<span [innerHTML]="svgIcon | async"></span>',
  styleUrls: ['./custom-svg.component.css']
})
export class CustomSvgComponent implements OnInit {

  @Input()
  public name: string | undefined;

  public svgIcon: Observable<SafeHtml> | undefined;

  constructor(private externalHtml: ExternalHtmlService) {
  }

  ngOnInit(): void {
    if (this.name) {
      this.svgIcon = this.externalHtml.getSvg(this.name);
    }
  }

}
