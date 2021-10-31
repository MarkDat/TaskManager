import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isLogin: boolean = false;
  constructor(){
    if(sessionStorage.getItem('tkn'))
      this.isLogin = true;
    else
    this.isLogin = false;
  }
}
