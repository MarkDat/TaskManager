import { Injectable } from '@angular/core';
import { AddCardRequest, AddCardResponse } from '@app/models';
import { BaseService } from '@app/services';
import { Observable } from 'rxjs';
import { API_ENDPOINTS } from './endpoint';

@Injectable({
  providedIn: 'root'
})
export class CardService {

  private baseUrl = API_ENDPOINTS.Card;
  constructor(
    private baseService: BaseService
  ) { }

  addNewCard(card: AddCardRequest): Observable<AddCardResponse>{
    return this.baseService.post(`${this.baseUrl}`, card);
  }

  moveCard(cardId: number, phaseId: number ): Observable<boolean>{
    return this.baseService.put(`${this.baseUrl}/${cardId}/phase/${phaseId}/move`,{});
  }
}
