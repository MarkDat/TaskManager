export class Priority{
    id: number;
    name: string;
    color: string;

    public constructor(init? : Partial <Priority>) {
        Object.assign(this, init);
    }
}