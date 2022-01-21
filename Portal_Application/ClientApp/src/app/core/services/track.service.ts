import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {DataService} from "./data.service";
import {PlayerService} from "./player.service";
import {StreamState} from "../../player/stream-state";
import {TrackForPlayer} from "../../player/trackForPlayer";
import {Track} from "../../track-info/track";

@Injectable({
  providedIn: "root"
})

export class TrackService {
  track: Track;

  constructor(private dataService: DataService, public playerService: PlayerService) {
  }

  getState(): Observable<StreamState> {
    return this.playerService.state$;
  }

  playTrack(trackId: number): void {
    if (this.playerService.trackSubject.getValue().id != trackId) {
      this.dataService.getTrackInfo(trackId).subscribe(
        (result: Track) => this.track = result,
        (error) => {
          console.log(error)
        },
        () => this.setTrackObjectAndDo(this.track, () =>
          this.playerService.openFile())
      );
    } else {
      this.playerService.play();
    }
  }

  pauseTrack(): void {
    this.playerService.pause();
  }

  setTrackObjectAndDo(track: Track, onComplete: () => void): void {
    this.dataService.getTrackUrl(track.id)
      .subscribe(
        (result: Blob) => {
          const blob = new Blob([result], {type: "audio/mp3"});

          let trackForPlayer = new TrackForPlayer;
          trackForPlayer.id = track.id;
          trackForPlayer.name = track.name;
          trackForPlayer.artistName = track.artistName;
          trackForPlayer.url = window.URL.createObjectURL(blob).toString();

          this.playerService.setFile(trackForPlayer);
        },
        (error) => {
          console.log(error)
        },
        onComplete);
  }
}
