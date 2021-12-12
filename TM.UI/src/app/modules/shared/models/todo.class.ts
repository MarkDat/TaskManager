export class Todo{
    id: number;
    name: string;
    isCheck: boolean;
    createdDate: Date;
    updatedDate: Date;
    createdBy: string;
    updatedBy: string;
    parentId: number;
    cardId: number;
    items?: Todo[];
    
    isEdit: boolean = false;

    public constructor(init? : Partial <Todo>) {
        Object.assign(this, init);
    }
}