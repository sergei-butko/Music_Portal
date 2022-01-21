import {Injectable} from "@angular/core";
import {BehaviorSubject, Observable} from "rxjs";
import {tap} from "rxjs/operators";
import {AudioService} from "./audio.service";
import {StreamState} from "../../player/stream-state";
import {TrackForPlayer} from "../../player/trackForPlayer";

@Injectable({
  providedIn: "root"
})

export class PlayerService {
  state$: Observable<StreamState>;
  state: StreamState;
  trackSubject = new BehaviorSubject<TrackForPlayer>(
    {
      id: 0,
      name: "",
      artistName: "",
      url: ""
    }
  );

  constructor(public audioService: AudioService) {
    this.audioService.getState().subscribe(state => {
      this.state = state;
    });
    this.state$ = new Observable<StreamState>(_ => {
      _.next(this.state);
      _.complete();
    });
  }

  getFile(): Observable<TrackForPlayer> {
    return this.trackSubject.pipe(tap(track => track));
  }

  setFile(trackForPlayer: TrackForPlayer): void {
    this.trackSubject.next(trackForPlayer);
  }

  playStream(url: string): void {
    this.audioService.playStream(url).subscribe();
  }

  openFile(): void {
    this.audioService.stop();
    this.playStream(this.trackSubject.getValue().url);
    this.audioService.play();
    this.state.playing = true;
  }

  play() {
    this.audioService.play();
    this.state.playing = true;
  }

  pause() {
    this.audioService.pause();
    this.state.playing = false;
  }

  onSliderChangeEnd(change) {
    this.audioService.seekTo(change.value);
  }
}
