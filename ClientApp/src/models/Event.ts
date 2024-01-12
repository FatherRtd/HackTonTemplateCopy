import EventCategory from "./EventCategory";
import EventType from "./EventType";
import User from "./User";

export interface IEvent {
    id: number,
    name: string,
    desctiption: string,
    address: string,
    imageUrl: string,
    siteUrl: string,
    startDate: Date,
    endDate: Date,
    isOnline: boolean,
    type: EventType,
    category: EventCategory,
    createUser: User,
    userIsRegister: boolean
}

export default class Event {
    constructor(options: IEvent | null) {
        if(options == null)
            return;

        this.id = options.id;
        this.name = options.name;
        this.desctiption = options.desctiption;
        this.address = options.address;
        this.imageUrl = options.imageUrl;
        this.siteUrl = options.siteUrl;
        this.startDate = new Date(options.startDate);
        this.endDate = new Date(options.endDate);
        this.isOnline = options.isOnline;
        this.type = new EventType(options.type);
        this.category = new EventCategory(options.category);
        this.createUser = new User(options.createUser);
        this.userIsRegister = options.userIsRegister;
    }

    id = 0;
    name = "";
    desctiption = "";
    address = "";
    imageUrl = "";
    siteUrl = "";
    startDate = new Date();
    endDate = new Date();
    isOnline = false;
    type = new EventType(null);
    category = new EventCategory(null);
    createUser = new User(null);
    userIsRegister = false;
}