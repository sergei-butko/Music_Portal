import {Component, Input} from '@angular/core';
import {Observable} from "rxjs";
import {DataService} from "../core/services/data.service";
import {TrackService} from "../core/services/track.service";
import {StreamState} from "../player/stream-state";
import {Track} from "../track-info/track";

@Component({
  selector: 'track-list',
  templateUrl: './track-list.component.html'
})

export class TrackListComponent {

  @Input() tracks: Track[];
  state$: Observable<StreamState>;
  state: StreamState;

  constructor(private dataService: DataService, private trackService: TrackService) {
    this.state$ = this.trackService.getState();
    this.state$.subscribe(state => this.state = state);
  }

  playTrack(trackId: number): void {
    this.state.playing ? this.trackService.pauseTrack() : this.trackService.playTrack(trackId);
  }

  downloadTrack(trackId): void {
    this.dataService.downloadTrack(trackId);
  }
}
