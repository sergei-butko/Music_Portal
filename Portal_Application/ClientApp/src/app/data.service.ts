import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from "rxjs";
import {map} from "rxjs/operators";
import {Artist} from "./artist/artist";
import {Track} from "./track/track";

@Injectable()
export class DataService {

  @Inject('BASE_URL') baseUrl: string;
  private url = this.baseUrl + "/artist";

  constructor(private http: HttpClient) {
  }

  getTopArtists(): Observable<Artist[]> {
    return this.http.get(this.url + '/top_artists')
      .pipe(map((data: any) => {
        return data.map(function (artist: Artist): Artist {
          return artist;
        });
      }));
  }

  getArtistInfo(artistId: number): Observable<Artist> {
    return this.http.get<Artist>(this.url + '/artist_info/' + artistId);
  }

  getArtistTopTracks(artistId: number): Observable<Track> {
    return this.http.get<Track>(this.url + '/artist_top_tracks/' + artistId);
  }

  getSimilarArtists(artistId: number): Observable<Artist[]> {
    return this.http.get<Artist[]>(this.url + '/similar_artists/' + artistId);
  }
}
