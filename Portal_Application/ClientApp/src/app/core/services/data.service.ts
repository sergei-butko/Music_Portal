import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Artist} from '../../artist/artist';
import {Track} from "../../track-info/track";
import {Album} from "../../album/album";

@Injectable()
export class DataService {

  constructor(private http: HttpClient) {
  }

  getTopArtists(): Observable<Artist[]> {
    return this.http.get<Artist[]>('/artist/top_artists');
  }

  getArtistInfo(artistId: number): Observable<Artist> {
    return this.http.get<Artist>('/artist/artist_info/' + artistId);
  }

  getArtistTopAlbums(artistId: number): Observable<Album[]> {
    return this.http.get<Album[]>('/artist/artist_top_albums/' + artistId);
  }

  getAlbumInfo(albumId: number): Observable<Album> {
    return this.http.get<Album>('/album/album_info/' + albumId);
  }

  getAlbumTracks(albumId: number): Observable<Track[]> {
    return this.http.get<Track[]>('/album/album_tracks/' + albumId);
  }

  getArtistTopTracks(artistId: number): Observable<Track[]> {
    return this.http.get<Track[]>('/artist/artist_top_tracks/' + artistId);
  }

  getTrackInfo(trackId: number): Observable<Track> {
    return this.http.get<Track>('/track/track_info/' + trackId);
  }

  getSimilarArtists(artistId: number): Observable<Artist[]> {
    return this.http.get<Artist[]>('/artist/similar_artists/' + artistId);
  }

  downloadTrack(trackId: number) {
    let track;
    this.getTrackInfo(trackId)
      .subscribe((data: Track) => track = data);

    return this.http.get(`/track/get_track/${trackId}`, {responseType: 'blob'})
      .subscribe((result: Blob) => {
        const blob = new Blob([result], {type: "audio/mp3"});
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a') as HTMLAnchorElement;

        a.href = url;
        a.download = `${track.artistName} - ${track.name}`;

        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        URL.revokeObjectURL(url);
      })
  }

  getTrackUrl(trackId: number): Observable<Blob> {
    return this.http.get(`/track/get_track/${trackId}`, {responseType: 'blob'});
  }
}
