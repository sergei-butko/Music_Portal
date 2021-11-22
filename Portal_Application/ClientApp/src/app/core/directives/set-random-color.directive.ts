import {Directive, ElementRef} from '@angular/core';

@Directive({
  selector: '[appSetRandomColor]'
})
export class SetRandomColorDirective {

  constructor(elem: ElementRef) {
    elem.nativeElement.style.color = 'white';
    elem.nativeElement.style.backgroundColor = this.getRandomColor();
  }

  getRandomColor(): string {
    let colors: string[] = ["gray", "plum", "coral", "orchid", "thistle", "cadetblue", "cornflowerblue",
      "darkcyan", "darkseagreen", "darkkhaki", "lightgreen", "lightsalmon", "lightblue", "lightskyblue"];
    let randNum = Math.floor(Math.random() * colors.length);
    return colors[randNum];
  }
}
