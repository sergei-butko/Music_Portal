import {Component} from '@angular/core';
import {Observable} from "rxjs";
import {DataService} from "../data.service";
import {Artist} from "../artist/artist"

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  artists$: Observable<Artist[]>;

  constructor(private dataService: DataService) {
    this.artists$ = this.dataService.getTopArtists();
  }
}
