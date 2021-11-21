import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DxPopupComponent } from 'devextreme-angular';
import { finalize } from 'rxjs/operators';
import { AddProjectRequest, GetProjectResponse } from '@app/models';
import { AppNotify } from '@app/common';
import { ProjectService } from '@app/services';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  @ViewChild('popup', {static: true}) popup: DxPopupComponent;
  popupVisible = false;
  createNewProjectButtonOptions: any;
  closeButtonOptions: any;
  newNameProject: string;
  isLoading: boolean = false;
  projects: GetProjectResponse[];
  
  constructor(
    private projectService: ProjectService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadProjects();
  }
  
  loadProjects(){
    this.projectService.getProjectsByCurrentUser().subscribe(res=>{
      this.projects = res;
    });
  }

  onClickCreateProjectPopup() {
    this.isLoading = true;
    
    

    var newProject = new AddProjectRequest();
    newProject.name = this.newNameProject;

    this.projectService.addNewProject(newProject).pipe(finalize(()=>{
        this.isLoading = false;
    })).subscribe(res => {

      AppNotify.success("Created new a project.");
      this.loadProjects();
      this.onActionPopup();
    });
  }

  onActionPopup() {
    this.popupVisible = !this.popupVisible;
  }

  onCellClick(e){
    if(e.columnIndex != 0){
      console.log(e.data);
      this.router.navigate(['projects',e.data.id,'kanban']);
    }
  }
}
