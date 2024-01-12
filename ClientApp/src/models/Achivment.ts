import type EventType from "./EventType";
import SkillType from "./SkillType";

export interface IAchivment {
    id: number | null | undefined,
    userId: number,
    name: string,
    year: number,
    eventType: EventType
}

export default class Achivment {
    constructor(options: IAchivment | null) {
        if(options == null)
            return;

        this.id = options.id;
        this.userId = options.userId;
        this.name = options.name;
        this.eventType = options.eventType;
        this.year = options.year;
    }

    id: number | null | undefined;
    userId = 0;
    name = "";
    year = 0;
    eventType = new SkillType(null);
}