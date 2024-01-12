<template>
    <div class="flex justify-center items-center h-[80vh]">
        <el-calendar v-if="!busy" class="w-10/12 backdrop-blur-lg bg-white/30">
            <template #date-cell="{ data }">
                <section v-if="eventExist(data.day)" class="events-block">
                    <div v-for="item in getEvents(data.day)" :key="item.id">
                        <el-tooltip
                            class="box-item"
                            effect="dark"
                            :content="item.name"
                            placement="top-start"
                        >
                            <p class="event-circle" :style="{ backgroundImage: getBackgroundValue(item) }" @click="showEventInDrawer(item)"></p>
                        </el-tooltip>
                    </div>
                </section>

            </template>
        </el-calendar>
    </div>
    <el-drawer v-model="drawerShow" direction="rtl">
        <template #header>
            <h4>{{ selectedEvent.name }}</h4>
        </template>
        <template #default>
            <el-image style="height: 200px" class="flex-center" :src="selectedEvent.imageUrl" fit="contain" />
            <h5 class="title-h5">Описание</h5>
            <p class="mb-2">{{ selectedEvent.desctiption }}</p>
            <h5 class="title-h5">Даты</h5>
            <p class="mb-2">{{ selectedEvent.endDate == null || selectedEvent.endDate == selectedEvent.startDate ? new Date(selectedEvent.startDate).toLocaleDateString() : new Date(selectedEvent.startDate).toLocaleDateString() + " - " + new Date(selectedEvent.endDate).toLocaleDateString() }}</p>
            <div>
                <el-tag type="warning" size="large" class="mb-2 mr-2">{{ selectedEvent.type?.name }}</el-tag>
                <el-tag class="mb-2" size="large">{{ selectedEvent.category?.name }}</el-tag>
            </div>
        </template>
        <template #footer>
            <div class="flex">
                <el-button @click="() => drawerShow = false">Закрыть</el-button>
                <div v-if="isAuthenticated" class="buttons-register">
                    <el-button v-if="selectedEvent.userIsRegister" type="danger" class="backdrop-blur-sm bg-white/30" plain @click="unregister(selectedEvent.id)">Отозвать заявку</el-button>
                    <el-button v-else class="backdrop-blur-sm bg-white/30" plain @click="register(selectedEvent.id)">Подать заявку</el-button>
                </div>
            </div>
        </template>
    </el-drawer>
    <TheAnimationBackground/>
</template>

<script setup lang="ts">
import TheAnimationBackground from "@/components/TheAnimationBackground.vue";
import type { IEvent } from "@/models/Event";
import eventService from "@/services/EventService";
import { computed, ref } from "vue";
import userService from "@/services/UserService"
import { useAuthenticationStore } from "@/stores/AuthenticationStore";

const authenticationStore = useAuthenticationStore();

const events = ref<IEvent[] | null>();
const busy = ref(false);

const selectedEvent = ref({} as IEvent);

const drawerShow = ref(false);

const update = async () => { 
    let temp = await eventService.getEvents();
    events.value = temp;
};

const eventExist = (date: Date) => {
    date = new Date(date)
    return events.value?.findIndex(x => new Date(x.startDate).toISOString().slice(0, 10) == date.toISOString().slice(0, 10)) != -1;
}

const getEvents = (date: Date) => {
    date = new Date(date)
    console.log(events.value?.filter(x => new Date(x.startDate).toISOString().slice(0, 10) == date.toISOString().slice(0, 10)));
    return events.value?.filter(x => new Date(x.startDate).toISOString().slice(0, 10) == date.toISOString().slice(0, 10));
}

const getBackgroundValue = (item: IEvent) => {
    const defultImage = "https://i.pinimg.com/736x/ed/f1/3d/edf13d66ad3a0614eb8362cb91d603ed--abstract-backgrounds-resolutions.jpg";
    return item.imageUrl != "" ? `url(${item.imageUrl})` : `url(${defultImage})`;
};

const showEventInDrawer = (event: IEvent) => {
    drawerShow.value = true;
    selectedEvent.value = event;
}

(async () => {
    busy.value = true;
    await update();
    busy.value = false;
})();

const register = async (eventId: number) => {
    await userService.eventRegistration(eventId);
    selectedEvent.value.userIsRegister = true;
}

const unregister = async (eventId: number) => {
    await userService.eventUnRegistration(eventId);
    selectedEvent.value.userIsRegister = false;
}

const isAuthenticated = computed(() => {
    return authenticationStore.isAuthenticated;
});
</script>

<style scoped>
.buttons-register {
    margin-left: 10px;
}

.flex {
    display: flex;
}

.title-h5 {
    color: #0000006e;
    font-size: 14px;
}
.flex-center {
    display: flex;
    justify-content: center;
    align-items: center;
    margin-bottom: 20px;
}

.events-block {
    display: flex;
    flex-wrap: wrap;
}
.event-circle {
    height: 30px;
    width: 30px;
    border-radius: 100%;
    background-position: center;
    background-size: cover;
    margin-right: 3px;
}

.event-circle:hover {
    transition: 0.3s;
    transform: scale(1.4);
    cursor: pointer;
}
</style>
