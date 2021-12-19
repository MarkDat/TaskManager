import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { Todo } from '@app/modules/shared/models';
import { CardService } from '@app/services';
import { finalize } from 'rxjs/operators';
import { AppNotify } from '../../utilities';
import {cloneDeep} from 'lodash';
import { ModalDetailsComponent } from '@app/modules/project/kanban/modal/modal-details.component';

@Component({
	selector: 'app-checklist-todo',
	templateUrl: './checklist-todo.component.html',
	styleUrls: ['./checklist-todo.component.css']
})
export class ChecklistTodoComponent implements OnInit {
	@Input() cardId: number = null;
	@Input() todos: Todo[] = [];
	@Input() modalComponent: ModalDetailsComponent;

	newTodo: Todo = new Todo({});
	newSubTodo: Todo = new Todo({});
	isLoading: boolean = false;

	constructor(
		private cardService: CardService,
		private cdref: ChangeDetectorRef
	) { }

	ngOnInit(): void {
		
	}

	ngAfterContentChecked() {
		this.cdref.detectChanges();
	}

	loadTodos(){
		this.isLoading = true;

		this.cardService.getTodos(this.cardId).pipe(finalize(() => {
			this.isLoading = false;
		})).subscribe(data => {
			this.todos = data;
		});
	}

	onCheckTodo(todo: Todo){
		console.log('check Todo');
		if(!todo.isCheckSubToDo)
			todo.items?.map(subTodo => subTodo.isCheck = todo.isCheck);

		this.updateTodo(todo);
		todo.isCheckSubToDo = false;
	}

	onCheckSubTodo(subTodo: Todo, parentTodo: Todo){
		console.log('check sub');
		if(parentTodo.isCheck && subTodo.isCheck == false){
			parentTodo.isCheckSubToDo = true;
			parentTodo.isCheck = false;
		}

		if(!parentTodo.items.some((e) => !e.isCheck))
			parentTodo.isCheck = true;

		this.updateTodo(subTodo);
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

	onAddTodo(todo: Todo, parentId: number = null) {

		if (!this.cardId)
			return;

		let newTodo = new Todo({
			cardId: this.cardId,
			parentId: parentId ? parentId : null,
			name: todo.nameNewTodo
		});

		this.isLoading = true;
		this.cardService.addTodo(newTodo).pipe(finalize(() => {
			this.isLoading = false;
		})).subscribe(data => {

			this.handleAddTodo(parentId, data);
			AppNotify.success("Added todo");
			todo.nameNewTodo = '';
			this.modalComponent.loadHistories();
		});
	}

	handleAddTodo(parentId: number, todo: Todo) {
		todo.isCheck = false;
		if (todo.parentId) {
			const indexParent =  this.todos.findIndex(_ => _.id === parentId);

			this.todos[indexParent].items = !this.todos[indexParent].items ? [] : this.todos[indexParent].items;
			this.todos[indexParent].items.push(todo);

			if(this.todos[indexParent].isCheck){
				this.todos[indexParent].isCheckSubToDo = true;
			this.todos[indexParent].isCheck = false;
			}
			
			
			return;
		}

		this.todos.push(todo);
	}

	onShowChild(todo: Todo){
		todo.isShowChild = !todo.isShowChild;
	}
}
