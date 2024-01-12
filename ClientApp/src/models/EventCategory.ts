export interface IEventCategory {
    id: number | null | undefined,
    name: string
}

export default class EventCategory {
    constructor(options: IEventCategory | null) {
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