export class Todo {
    id: number;
    name: string;
    isCheck: boolean = false;
    createdDate: Date;
    updatedDate: Date;
    createdBy: string;
    updatedBy: string;
    parentId?: number;
    cardId: number;
    items?: Todo[] = [];

    isEdit: boolean = false;
    isShowChild: boolean = false;
    nameNewTodo: string;
    isCheckSubToDo: boolean = false;

    public constructor(init?: Partial<Todo>) {
        Object.assign(this, init);
    }
}

export class TodoAdd {
    name: string;

    public constructor(init?: Partial<TodoAdd>) {
        Object.assign(this, init);
    }
}