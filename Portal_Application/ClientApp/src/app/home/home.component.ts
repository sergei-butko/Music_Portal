import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Artist} from "./artist"

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  artists: Artist[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Artist[]>(baseUrl + 'artist/top_artists').subscribe(result => {
      this.artists = result;
    }, error => console.error(error));
  }
}
