import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-wiki',
  template: `
    <p class="text-muted" [ngStyle]="{'display': isShownLess ? 'block' : 'none'}">{{summary}}</p>
    <p class="text-muted" [ngStyle]="{'display': isShownLess ? 'none' : 'block'}">{{wiki}}</p>
    <button type="button" class="btn btn-light border-0" (click)="click()"
            [ngStyle]="{'innerHtml': 'isShownLess ? Show more : Show less'}">Show more</button>`
})

export class WikiComponent {

  @Input() summary: string = "";
  @Input() wiki: string = "";

  isShownLess: boolean = true;

  click(): void {
    this.isShownLess = !this.isShownLess;
  }
}
