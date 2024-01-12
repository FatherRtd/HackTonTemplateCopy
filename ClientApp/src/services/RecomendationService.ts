import { httpClient } from "@/services/HttpClient/HttpClient";
import type { IRecomendation } from "@/models/Recomendation";
import type Skill from "@/models/Skill";

export default {
    async getRecomendation(skills: Array<Skill>): Promise<IRecomendation[]> {
        const response = await httpClient.get<IRecomendation[]>("https://career.habr.com/api/frontend/suggestions/similar_skills?skill_ids="+skills.join(","));
        return response.data;
    },
}