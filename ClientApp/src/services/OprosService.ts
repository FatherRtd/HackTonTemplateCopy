import { httpClient } from "@/services/HttpClient/HttpClient";
import type { ISkill } from "@/models/Skill";
import type { IOpros } from "@/models/Opros";

export default {
    async getSkills(): Promise<ISkill[]> {
        const response = await httpClient.get<ISkill[]>("api/skill-type/get");
        return response.data;
    },

    async saveEvent(opros: IOpros) {
        const response = await httpClient.post<IOpros>("api/opros/save", opros);
    }
}