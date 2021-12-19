import { Component, Input, OnInit } from '@angular/core';
import { Todo } from '@app/modules/shared/models';
import { CardService } from '@app/services';
import { finalize } from 'rxjs/operators';
import { AppNotify } from '../../utilities';
import {cloneDeep} from 'lodash';

@Component({
	selector: 'app-checklist-todo',
	templateUrl: './checklist-todo.component.html',
	styleUrls: ['./checklist-todo.component.css']
})
export class ChecklistTodoComponent implements OnInit {
	@Input() cardId: number = null;
	@Input() todos: Todo[] = [];

	newTodo: Todo = new Todo({});
	newSubTodo: Todo = new Todo({});
	isLoading: boolean = false;

	constructor(
		private cardService: CardService
	) { this.loadTodos();}

	ngOnInit(): void {
		
	}

	loadTodos(){
		this.isLoading = true;

		this.cardService.getTodos(this.cardId).pipe(finalize(() => {
			this.isLoading = false;
		})).subscribe(data => {
			this.todos = data;
		});
	}

	onClickTodo(e, index: number, isParent: boolean = true, todoChild: Todo = null) {
		if (isParent) {
			this.todos[index].items.map(subTodo => subTodo.isCheck = e.value);
			this.updateTodo(this.todos[index]);
			return;
		}

		this.todos[index].isCheck = !e.value ? false : this.todos[index].isCheck;
		this.updateTodo(todoChild);
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

	onEdited(todo: Todo) {
		todo.isEdit = false;
		todo.name = todo.name.trim();
		this.updateTodo(todo);
	}

	onAddTodo(value, parentId: number = null) {
		let name = cloneDeep(value);

		if (!this.cardId)
			return;

		let newTodo = new Todo({
			cardId: this.cardId,
			parentId: parentId ? parentId : null,
			name: name
		});

		this.isLoading = true;
		this.cardService.addTodo(newTodo).pipe(finalize(() => {
			this.isLoading = false;
		})).subscribe(data => {

			this.handleAddTodo(parentId, data);
			AppNotify.success("Added todo");

		});
	}

	handleAddTodo(parentId: number, todo: Todo) {
		if (todo.parentId) {
			this.todos[parentId].items.push(todo);
			return;
		}

		this.todos.push(todo);
	}
}
