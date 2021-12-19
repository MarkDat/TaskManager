import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Card, Employee, UpdateCardRequest } from '@app/models';
import { AppNotify, DISPLAY_FORMAT_DATETIME, PHASE_CODE } from '@app/modules/shared/utilities';
import { CardService, EmployeeService } from '@app/services';
import $ from 'jquery'
import { finalize } from 'rxjs/operators';
import { KanbanComponent } from '../kanban.component';
import { DatePipe } from '@angular/common'

@Component({
  selector: 'app-modal',
  templateUrl: './modal-details.component.html',
  styleUrls: ['./modal-details.component.css']
})
export class ModalDetailsComponent implements OnInit {
  @ViewChild('modal')
  modal: ElementRef;

  @Input() card: Card = new Card({});
  @Input() kanbanComponent: KanbanComponent;

  employees: Employee[];
  displayFormatDatetime = DISPLAY_FORMAT_DATETIME;
  showCheckBoxesModes: string[] = ["normal", "selectAll", "none"];
  showCheckBoxesMode: string = this.showCheckBoxesModes[0];
  isLoading: boolean = false;
  oldDate: Date = this.card.dueDate;
  opCode: string = PHASE_CODE.Opportunity;
  orderCode: string =  PHASE_CODE.Order;
  quoteCode: string = PHASE_CODE.Quote;

  constructor(
    private employeeService: EmployeeService,
    private cardService: CardService,
    public datepipe: DatePipe
  ) { }

  ngOnInit(): void {
    this.employees = this.employeeService.getEmployees();
    console.log(this.card);
  }

  ngAfterViewInit() {

  }

  ngOnDestroy(): void {
    console.log("DESTROY");
  }

  onSaveSubTodo() {
    console.log("Save sub TODO");
  }

  onDateChange(e) {

    this.card.dueDate = e.value;

    let cardRequest = new UpdateCardRequest({
      cardId: this.card.id,
      value: this.card.dueDate.toDateString()
    });

    console.log(cardRequest);

    this.cardService.updateCard("duedate", cardRequest).pipe(finalize(() => {
      this.isLoading = false
    })).subscribe(data => {
      AppNotify.success("Changed Due Date");
      this.reloadKanban();
    });
  }

  onClickSaveDesc() {
    let cardRequest = new UpdateCardRequest({
      cardId: this.card.id,
      value: this.card.description
    });

    this.cardService.updateCard("description", cardRequest).pipe(finalize(() => {
      this.isLoading = false
    })).subscribe(data => {
      AppNotify.success("Changed description");
    });
  }

  onSaveClick(e) {

  }

  reloadKanban(){
    this.kanbanComponent.loadKanban();
  }

  
  public getHistoryString(){
    let str = '';

    this.card.cardHistories.sort((a,b) =>{
         
      let dateA = new Date(a.createdDate).getTime();
       let dateB = new Date(b.createdDate).getTime();

       return dateA < dateB ? 1 : -1; 
    });

    this.card.cardHistories.map(e => {
      str+=`[${this.datepipe.transform(e.createdDate, 'dd/MM/yyyy')}] ${e.content} \n`
    });
    return str;
  }
  
  treeViewSelectionChanged(e){
    console.log(e)
  }
}
