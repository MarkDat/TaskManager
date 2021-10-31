import { GetUserResponse } from "./user.class";

export class AddProjectRequest{
    userId: number;
    name: string;

    constructor(init?: Partial<AddProjectRequest>){
        Object.assign(this, init);
    }
}

export class AddProjectResponse{
    id: number;
    name: string;

    constructor(init?: Partial<AddProjectResponse>){
        Object.assign(this, init);
    }
}

export class GetProjectMemberResonse{
    userId: number;
    projectId: number;
    owner: boolean;
    user: GetUserResponse;

    constructor(init?: Partial<GetProjectMemberResonse>){
        Object.assign(this, init);
    }
}

export class GetProjectResponse{
    id: number;
    name: string;
    projectMembers: GetProjectMemberResonse[];

    constructor(init?: Partial<GetProjectResponse>){
        Object.assign(this, init);
    }
}