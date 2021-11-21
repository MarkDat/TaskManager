import { History, Priority, Tag, Todo } from ".";

export class AddCardRequest {
    projectId : number;
    name : string;

    public constructor(init? : Partial < AddCardRequest >) {
        Object.assign(this, init);
    }
}

export class AddCardResponse {
    id : number;
    projectId : number;
    name : string;

    public constructor(init? : Partial < AddCardResponse >) {
        Object.assign(this, init);
    }
}

export class Card{
    id:number;
    projectId:number;
    name:string;
    description:string;
    dueDate: Date;
    todos: Todo[];
    priority: Priority;
    tags: Tag[];
    assignUser: string;
    cardHistories: History[];

    public constructor(init? : Partial < Card >) {
        Object.assign(this, init);
    }
}