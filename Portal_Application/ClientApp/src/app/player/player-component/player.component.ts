import {Component} from "@angular/core";
import {Observable} from "rxjs";
import {PlayerService} from "../../core/services/player.service";
import {StreamState} from "../stream-state";
import {TrackForPlayer} from "../trackForPlayer";

@Component({
  selector: 'app-player',
  templateUrl: 'player.component.html',
  styleUrls: ['player.component.css']
})

export class PlayerComponent {
  state$: Observable<StreamState>;
  currentTrack$: Observable<TrackForPlayer>;

  constructor(private playerService: PlayerService) {
    this.currentTrack$ = this.playerService.getFile();
    this.state$ = this.playerService.audioService.getState();
  }

  play() {
    this.playerService.play();
  }

  pause() {
    this.playerService.pause();
  }

  onSliderChangeEnd(change) {
    this.playerService.onSliderChangeEnd(change);
  }
}
