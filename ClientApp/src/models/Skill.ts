import SkillType from "./SkillType";

export interface ISkill {
    id: number | null | undefined,
    userId: number,
    ageExperience: number,
    type: SkillType
}

export default class Skill {
    constructor(options: ISkill | null) {
        if(options == null)
            return;

        this.id = options.id;
        this.userId = options.userId;
        this.ageExperience = options.ageExperience;
        this.type = options.type;
    }

    id: number | null | undefined;
    userId = 0;
    ageExperience = 0;
    type = new SkillType(null);
}