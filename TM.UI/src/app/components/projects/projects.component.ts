import { Component, OnInit } from '@angular/core';
import { AppNotify } from 'src/app/common/AppNotify.class';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  popupVisible = false;
  createNewProjectButtonOptions: any;
  closeButtonOptions: any;
  newNameProject : string;
  constructor() { }

  ngOnInit(): void {
    this.onClickCreateProjectPopup();
    this.onClickClosePopup();
  }

  onClickClosePopup(){
    this.closeButtonOptions = {
      text: "Close",
      onClick: function(e) {
          this.popupVisible = false;
      }
    };
  }

  onClickCreateProjectPopup(){
    this.createNewProjectButtonOptions = {
      icon: "check",
      type: "success",
      text: "Create",
      onClick: function(e) {
        AppNotify.success('Project is created');
        this.popupVisible = false;
      }
  };
  }

  showPopupCreate(){
    this.popupVisible = true;
  }
}
