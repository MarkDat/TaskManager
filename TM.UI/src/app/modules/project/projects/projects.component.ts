import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DxPopupComponent } from 'devextreme-angular';
import { finalize } from 'rxjs/operators';
import { AddProjectRequest, GetProjectMemberResonse, GetProjectResponse } from '@app/models';
import { AppNotify, HEIGHT_BUTTON, HEIGHT_POPUP, WIDTH_BUTTON, WIDTH_POPUP } from '@app/utilities';
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
  heightPopup: number = HEIGHT_POPUP;
  widthPopup: number = WIDTH_POPUP;
  heightButton: number = HEIGHT_BUTTON;
  widthButton: number = WIDTH_BUTTON;
  
  constructor(
    private projectService: ProjectService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadProjects();
    console.log("PROJECTS INIT");
  }
  
  loadProjects(){
    this.projectService.getProjectsByCurrentUser().subscribe(res=>{
      this.projects = res;
      this.projects.sort((a,b) =>{
         
        let dateA = new Date(a.CreatedDate).getTime();
         let dateB = new Date(b.CreatedDate).getTime();

         return dateA > dateB ? 1 : -1; 
      });
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

  handleProjectMember(projectMembers: GetProjectMemberResonse[]){
    return projectMembers.map((e) => {
        return e.user.firstName + " " + e.user.lastName;
    }).join(", ");
  }
}
