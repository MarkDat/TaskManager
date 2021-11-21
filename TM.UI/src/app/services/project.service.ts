import { Injectable } from '@angular/core';
import { AddCardRequest, Phase } from '@app/models';
import { Observable } from 'rxjs';
import { AddProjectRequest, AddProjectResponse, GetProjectResponse } from '../models/project.class';
import { BaseService } from './base.service';
import { API_ENDPOINTS } from './endpoint';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private baseUrl = API_ENDPOINTS.Project;
  constructor(
    private baseService: BaseService
  ) { }

  getProjectsByCurrentUser(): Observable<GetProjectResponse[]>{
    return this.baseService.get(`${this.baseUrl}/user`);
  }

  getOne(projectId: number): Observable<GetProjectResponse>{
    return this.baseService.get(`${this.baseUrl}/${projectId}`);
  }

  addNewProject(newProject : AddProjectRequest): Observable<AddProjectResponse>{
    return this.baseService.post(`${this.baseUrl}`,newProject);
  }

  getPhaseAndListCard(idProject: number): Observable<Phase[]>{
    return this.baseService.get(`${this.baseUrl}/${idProject}/phase`);
  }
}
