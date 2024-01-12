export interface IEventType {
    id: number | null | undefined,
    name: string
}

export default class EventType {
    constructor(options: IEventType | null) {
        if(options == null)
            return;

        this.id = options.id;
        this.name = options.name;
    }

    id: number | null | undefined;
    name = ""

    isEmpty() {
        return this.id == undefined && this.name == undefined;
    }
}