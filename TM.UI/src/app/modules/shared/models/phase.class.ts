import { Card } from ".";

export class Phase{
    id: number;
    name: string;
    acceptMoveId: number;
    code: string;
    cards: Card[];

    constructor(init?: Partial<Phase>){
        Object.assign(this, init);
    }
}