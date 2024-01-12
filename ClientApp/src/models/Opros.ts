import User from "./User";

export interface IOpros {
    id: number,
    user: User,
    date: Date,
    haveCommercialProject: boolean
}

export default class Opros {
    constructor(options: IOpros | null) {
        if(options == null)
            return;

        this.id = options.id;
        this.date = new Date(options.date);
        this.haveCommercialProject = options.haveCommercialProject;
        this.user = new User(options.user);
    }

    id = 0;
    date = new Date();
    haveCommercialProject = false;
    user = new User(null);
}