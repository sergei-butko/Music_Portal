import {Component} from '@angular/core';
import {Artist} from "../artist/artist"
import {DataService} from "../data.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  artists: Artist[];

  constructor(private dataService: DataService) {
    this.dataService.getTopArtists()
      .subscribe((data: Artist[]) => this.artists = data);
  }

  setColor(): string {
    let colors: string[] = ["cadetblue", "coral", "cornflowerblue", "darkkhaki", "darkcyan", "lightgreen",
      "darkseagreen", "plum", "gray", "lightsalmon", "lightskyblue", "orchid", "thistle"];
    let randNum = Math.floor(Math.random() * colors.length);
    return colors[randNum];
  }
}
