import {Component} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Observable} from "rxjs";
import {DataService} from "../core/services/data.service";
import {Track} from '../track-info/track';

@Component({
  selector: 'app-track',
  templateUrl: './track.component.html'
})

export class TrackComponent {
  id: number;
  track$: Observable<Track>;

  constructor(private dataService: DataService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.id = params['id']
    })
    this.track$ = this.dataService.getTrackInfo(this.id);
  }

  downloadTrack(trackId): void {
    this.dataService.downloadTrack(trackId);
  }
}
