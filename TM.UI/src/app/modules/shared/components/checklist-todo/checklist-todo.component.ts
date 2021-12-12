import { Component, Input, OnInit } from '@angular/core';
import { Todo } from '@app/modules/shared/models';
import { CardService } from '@app/services';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-checklist-todo',
  templateUrl: './checklist-todo.component.html',
  styleUrls: ['./checklist-todo.component.css']
})
export class ChecklistTodoComponent implements OnInit {
  @Input() todos: Todo[];

  newTodo: Todo = new Todo({});
  newSubTodo: Todo = new Todo({});
  isLoading: boolean = false;
  constructor(
    private cardService: CardService
  ) { }

  ngOnInit(): void {
  }

  onClickAll(e, index: number) {
    this.todos[index].items.map(subTodo => subTodo.isCheck = e.value);
    this.updateTodo(this.todos[index]);

  }

  updateTodo(todo: Todo) {
    this.isLoading = true;
    this.cardService.updateTodo(todo).pipe(finalize(() => {
      this.isLoading = false;
    })).subscribe(data => {

    });
  }

  clickToEdit(todo: Todo, isEdit: boolean = true) {
    todo.isEdit = isEdit;
  }

  onEdited(todo: Todo){
    todo.isEdit = false;
    this.updateTodo(todo);
  }
}
