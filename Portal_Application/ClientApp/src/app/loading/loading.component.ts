import {Component} from '@angular/core';

@Component({
  selector: 'app-loading',
  template: `<div  class="d-flex align-items-center mt-5 text-dark"></div>
             <div class="spinner-border mx-4" role="status"></div>
             <strong>Loading...</strong>`
})

export class LoadingComponent {
}
