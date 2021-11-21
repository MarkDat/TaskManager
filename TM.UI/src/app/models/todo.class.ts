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

    public constructor(init? : Partial <Todo>) {
        Object.assign(this, init);
    }
}