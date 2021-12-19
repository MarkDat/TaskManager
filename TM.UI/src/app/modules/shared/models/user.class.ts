export class LoginUserRequest{
    userName : string;
    password : string;
    public constructor(init?: Partial<LoginUserRequest>) {
		Object.assign(this, init);
	}
}

export class LoginUserResponse{
  message : string;
  isSuccess : boolean;
  public constructor(init?: Partial<LoginUserResponse>) {
		Object.assign(this, init);
  }
}

export class GetUserResponse{
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  image:  string;
  
  public constructor(init?: Partial<GetUserResponse>) {
		Object.assign(this, init);
  }
}