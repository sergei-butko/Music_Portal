import {Injectable} from "@angular/core";
import {Observable, BehaviorSubject, Subject} from "rxjs";
import {takeUntil} from "rxjs/operators";
import {StreamState} from "../../player/stream-state";

@Injectable({
  providedIn: "root"
})
export class AudioService {

  private stop$ = new Subject();
  private audioObj = new Audio();
  private audioEvents = [
    "ended",
    "error",
    "play",
    "playing",
    "pause",
    "timeupdate",
    "canplay",
    "loadedmetadata",
    "loadstart"
  ];

  private state: StreamState = {
    playing: false,
    readableCurrentTime: '',
    readableDuration: '',
    duration: undefined,
    currentTime: undefined,
    canplay: false,
    error: false
  };

  private stateChange: BehaviorSubject<StreamState> = new BehaviorSubject(
    this.state
  );

  getState(): Observable<StreamState> {
    return this.stateChange.asObservable();
  }

  private resetState() {
    this.state = {
      playing: false,
      readableCurrentTime: '',
      readableDuration: '',
      duration: undefined,
      currentTime: undefined,
      canplay: false,
      error: false
    };
  }

  private streamObservable(url: string) {

    return new Observable(observer => {
      this.audioObj.src = url;
      this.audioObj.load();
      this.audioObj.play();

      const handler = (event: Event) => {
        observer.next(event);
      };

      this.addEvents(this.audioObj, this.audioEvents, handler);
      return () => {
        this.audioObj.pause();
        this.audioObj.currentTime = 0;

        this.removeEvents(this.audioObj, this.audioEvents, handler);

        this.resetState();
      };
    });
  }

  private addEvents(obj, events, handler) {
    events.forEach(event => {
      obj.addEventListener(event, handler);
    });
  }

  private removeEvents(obj, events, handler) {
    events.forEach(event => {
      obj.removeEventListener(event, handler);
    });
  }

  playStream(url: string) {
    return this.streamObservable(url).pipe(takeUntil(this.stop$));
  }

  play(): void {
    this.audioObj.play();
  }

  pause(): void {
    this.audioObj.pause();
  }

  stop(): void {
    this.stop$.next;
  }

  seekTo(seconds): void {
    this.audioObj.currentTime = seconds;
  }
}
