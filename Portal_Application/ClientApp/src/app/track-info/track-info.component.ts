import {Component} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {DataService} from "../core/services/data.service";
import {Track} from "./track";
import {Observable} from "rxjs";

@Component({
  selector: 'app-track-info',
  templateUrl: './track-info.component.html'
})

export class TrackInfoComponent {
  id: number;
  track$: Observable<Track>;

  constructor(private dataService: DataService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.id = params['id']
    })
    this.track$ = this.dataService.getTrackInfo(this.id);
  }
}
