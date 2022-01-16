import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
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

  private _card: Card;
  @Input() get card(): Card {
    return this._card || new Card({});
  };
  
  set card(value) {
    if (this._card !== value) {
			this._card = value;
			this.cardChange.emit(value);
		}
  }

  @Output() cardChange = new EventEmitter<any>();

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
    this.card.dueDate = new Date(e.value);
    let cardRequest = new UpdateCardRequest({
      cardId: this.card.id,
      value: this.card.dueDate.toDateString()
    });

    console.log(cardRequest);

    this.cardService.updateCard("duedate", cardRequest).pipe(finalize(() => {
      this.isLoading = false
    })).subscribe(data => {
      AppNotify.success("Changed Due Date");
      this.loadHistories();
      this.reloadKanban();
    });
  }

  onClickSaveDesc() {
    this.card.description = this.card.description.trim();

    let cardRequest = new UpdateCardRequest({
      cardId: this.card.id,
      value: this.card.description
    });

    this.cardService.updateCard("description", cardRequest).pipe(finalize(() => {
      this.isLoading = false
    })).subscribe(data => {
      AppNotify.success("Changed description");
      this.loadHistories();
    });
  }

  onSaveClick(e) {

  }

  reloadKanban(){
    this.kanbanComponent.loadKanban();
  }

  public loadHistories(){
    this.isLoading = true;
    this.cardService.getHistories(this.card.id).pipe(finalize(() => {
      this.isLoading = false
    })).subscribe(data => {
          this.card.cardHistories = data;
    });
  }
  
  treeViewSelectionChanged(e){
    console.log(e)
  }
}
