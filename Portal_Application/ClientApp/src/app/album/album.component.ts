import {Component} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Observable} from "rxjs";
import {DataService} from "../data.service";
import {Album} from "./album";
import {Track} from "../track/track";

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html'
})

export class AlbumComponent {
  id: number;
  album$: Observable<Album>;
  tracks$: Observable<Track[]>;

  constructor(private dataService: DataService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.id = params['id']
    })
    this.album$ = this.dataService.getAlbumInfo(this.id);
    this.tracks$ = this.dataService.getAlbumTracks(this.id);
  }
}
