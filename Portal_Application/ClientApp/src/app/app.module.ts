import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatTabsModule} from "@angular/material/tabs";

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {DataService} from "./core/services/data.service";
import {HomeComponent} from './home/home.component';
import {ArtistComponent} from './artist/artist.component';
import {AlbumComponent} from "./album/album.component";
import {TrackInfoComponent} from "./track-info/track-info.component";
import {TrackComponent} from "./track/track.component";
import {TrackListComponent} from "./track-list/track-list.component";
import {LoadingComponent} from "./loading/loading.component";
import {WikiComponent} from "./wiki/wiki.component";
import {PlayerComponent} from "./player/player-component/player.component";
import {SetRandomColorDirective} from "./core/directives/set-random-color.directive";
import {MatListModule} from "@angular/material/list";
import {MaterialModule} from "./material.module";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ArtistComponent,
    AlbumComponent,
    TrackInfoComponent,
    TrackComponent,
    TrackListComponent,
    LoadingComponent,
    WikiComponent,
    PlayerComponent,
    SetRandomColorDirective
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'artist/:id', component: ArtistComponent},
      {path: 'album/:id', component: AlbumComponent},
      {path: 'track/:id', component: TrackInfoComponent}
    ]),
    BrowserAnimationsModule,
    MatTabsModule,
    MatListModule,
    MaterialModule
  ],
  providers: [DataService, PlayerComponent],
  bootstrap: [AppComponent]
})
export class AppModule {
}
