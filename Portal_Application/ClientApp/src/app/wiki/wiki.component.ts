import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-wiki',
  template: `
    <p class="text-muted" *ngIf="summary ? summary : 'Sorry, no information is currently available'"
       [ngStyle]="{'display': isShownLess ? 'block' : 'none'}">{{summary}}</p>
    <p class="text-muted" *ngIf="wiki ? wiki : 'Sorry, no information is currently available'"
       [ngStyle]="{'display': isShownLess ? 'none' : 'block'}">{{wiki}}</p>
    <button type="button" class="btn btn-light border-0"
            (click)="click()">{{isShownLess ? 'Show more' : 'Show less'}}</button>`
})

export class WikiComponent {

  @Input() summary: string = "";
  @Input() wiki: string = "";

  isShownLess: boolean = true;

  click(): void {
    this.isShownLess = !this.isShownLess;
  }
}
