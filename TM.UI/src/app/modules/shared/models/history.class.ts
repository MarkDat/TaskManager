export class History {
    content : string;
    createdDate : Date;

    public constructor(init? : Partial <History>) {
        Object.assign(this, init);
    }
}
