import { Injectable } from '@angular/core';
import { Todo, AddCardRequest, AddCardResponse, GetCardRequest, Card, UpdateCardRequest, History } from '@app/modules/shared/models';
import { BaseService } from '@app/modules/shared/services';
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

  getCard(card: GetCardRequest): Observable<Card>{
    return this.baseService.post(`${this.baseUrl}/details`,card);
  }

  updateCard(propertyName: string, cardRequest: UpdateCardRequest){
    return this.baseService.put(`${this.baseUrl}/${propertyName}`, cardRequest);
  }

  updateTodo(todo: Todo){
    return this.baseService.put(`${this.baseUrl}/todo`, todo);
  }

  addTodo(todo: Todo) : Observable<Todo>{
    return this.baseService.post(`${this.baseUrl}/todo`, todo);
  }

  getTodos(cardId: number) : Observable<Todo[]>{
    return this.baseService.get(`${this.baseUrl}/${cardId}/todo`);
  }

  getHistories(cardId: number) : Observable<History[]>{
    return this.baseService.get(`${this.baseUrl}/${cardId}/history`);
  }
}
