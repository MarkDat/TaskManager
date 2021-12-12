export class Tag{
    id: number;
    name: string;
    color: string

    public constructor(init? : Partial <Tag>) {
        Object.assign(this, init);
    }
}