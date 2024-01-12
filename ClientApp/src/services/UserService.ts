import { httpClient } from "@/services/HttpClient/HttpClient";
import { type IUser } from "@/models/User";

export default {
    async getCurentUser(): Promise<IUser> {
        const response = await httpClient.get<IUser>("api/user/getCurrentUser");
        return response.data;
    },

    async eventRegistration(eventId: number) {
        await httpClient.post("api/user/event-register/" + eventId);
    },

    async eventUnRegistration(eventId: number) {
        await httpClient.delete("api/user/event-unregister/" + eventId);
    }
}