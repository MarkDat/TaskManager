export class History {
    content : string;

    public constructor(init? : Partial <History>) {
        Object.assign(this, init);
    }
}
