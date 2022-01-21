import {Component} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Observable} from "rxjs";
import {DataService} from "../core/services/data.service";
import {Artist} from "./artist"
import {Album} from "../album/album";
import {Track} from "../track-info/track";

@Component({
  selector: 'app-artist',
  templateUrl: './artist.component.html'
})

export class ArtistComponent {
  id: number;
  artist$: Observable<Artist>;
  albums$: Observable<Album[]>;
  tracks$: Observable<Track[]>;
  similar$: Observable<Artist[]>;

  constructor(private dataService: DataService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.id = params['id']
    })
    this.artist$ = this.dataService.getArtistInfo(this.id);
    this.albums$ = this.dataService.getArtistTopAlbums(this.id);
    this.tracks$ = this.dataService.getArtistTopTracks(this.id);
    this.similar$ = this.dataService.getSimilarArtists(this.id);
  }
}
