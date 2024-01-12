import { httpClient } from "@/services/HttpClient/HttpClient";
import type { IEvent } from "@/models/Event";
import type { IEventType } from "@/models/EventType";
import type { IEventCategory } from "@/models/EventCategory";

export default {
    async getEvents(): Promise<IEvent[]> {
        const response = await httpClient.get<IEvent[]>("api/event/get");
        return response.data;
    },

    async getUserEvents(): Promise<IEvent[]> {
        const response = await httpClient.get<IEvent[]>("api/event/getUserEvents");
        return response.data;
    },

    async getEvent(id: number): Promise<IEvent> {
        const response = await httpClient.get<IEvent>("api/event/get/" + id);
        return response.data;
    },

    async getEventTypes(): Promise<IEventType[]> {
        const response = await httpClient.get<IEventType[]>("api/event-types");
        return response.data;
    },

    async getEventCategories(): Promise<IEventCategory[]> {
        const response = await httpClient.get<IEventCategory[]>("api/event-categories");
        return response.data;
    },

    async saveEvent(event: IEvent): Promise<IEvent> {
        const response = await httpClient.post<IEvent>("api/event/save", event);
        return response.data;
    }
}