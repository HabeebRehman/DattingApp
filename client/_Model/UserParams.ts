import { User } from "./User";

export class UserParams{
    Gender:string;
    MinAge=18;
    MaxAge=99;
    PageNumber=1;
    PageSize=5;
    OrderBy='LastActive';
    constructor(user:User)
    {
       
        this.Gender=user.gender=='female'?'male':'female'
        
    }


}
