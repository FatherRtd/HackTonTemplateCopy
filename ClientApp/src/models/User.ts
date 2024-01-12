import Achivment from "./Achivment";
import EventCategory from "./EventCategory";
import { SexType } from "./SexType";
import Skill from "./Skill";
import { UserRole } from "./UserRole";

export interface IUser {
    id: number,
    name: string,
    login: string,
    password: string,
    mail: string,
    age: number,
    sex: SexType,
    role: UserRole,
    category: EventCategory,
    skills: Array<Skill>,
    achivments: Array<Achivment>
}

export default class User {
    constructor(options: IUser | null) {
        if(options == null)
            return;
        
        this.id = options.id;
        this.name = options.name;
        this.login = options.login;
        this.password = options.password;
        this.mail = options.mail;
        this.age = options.age;
        this.sex = options.sex;
        this.role = options.role;
        this.category = options.category;
        this.skills = options.skills?.map(x => new Skill(x));
        this.achivments = options.achivments?.map(x => new Achivment(x));
        console.log(options.achivments)
    }

    id = 0;
    name = ""
    login = "";
    password = "";
    mail = "";
    age = 0;
    sex = SexType.Man;
    role = UserRole.Participant;
    category = new EventCategory(null);
    skills = new Array<Skill>();
    achivments = new Array<Achivment>();

    isEmpty() {
        return this.id == undefined && this.name == undefined;
    }
}