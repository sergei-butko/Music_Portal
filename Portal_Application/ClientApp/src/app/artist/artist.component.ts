import {Component, OnInit} from '@angular/core';
import {Artist} from "./artist"
import {DataService} from "../data.service"

@Component({
  selector: 'app-artist',
  templateUrl: './artist.component.html',
})

export class ArtistComponent implements OnInit {
  id: number;
  artist: Artist;

  constructor(private dataService: DataService) {
  }

  ngOnInit() {
    this.dataService.getArtistInfo(this.id)
      .subscribe((data: Artist) => this.artist = data);
  }
}
