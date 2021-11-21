import { Injectable } from '@angular/core';
import { BaseService } from '@app/services';
import { API_ENDPOINTS } from './endpoint';

@Injectable({
  providedIn: 'root'
})
export class PhaseService {

  private baseUrl = API_ENDPOINTS.Phase;
  constructor(
    private baseService: BaseService
  ) { }
}
