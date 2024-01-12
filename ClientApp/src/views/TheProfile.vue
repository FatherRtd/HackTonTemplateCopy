<template>
    <el-button class="absolute end-1 mr-8 backdrop-blur-sm bg-[#409eff] shadow-lg px-10" size="large" type="primary" @click="setDialog">На взлет</el-button>
    <div v-loading="loading" class="flex flex-wrap justify-center p-8 main-block">
        <div class="w-7/12">
            <div class="flex justify-between backdrop-blur-sm bg-white/30 h-fit rounded-xl shadow-lg ml-8 mt-[50px] mb-8 items-end p-5">
                <div class="flex flex-col">
                    <p class="text-xl font-bold mb-2">{{currentUser.name == "" ? "No name" : currentUser.name}}</p>
                    <div class="row-content">{{ currentUser.mail }}</div>
                    <div class="row-content">{{ currentUser.age }}</div>
                    <div class="row-content">Уровень: {{ currentUser.category == null ? "Неопределен" : currentUser.category }}</div>
                </div>
                <div class="flex w-[20vw]">
                    <div class="avatar">{{ getFirstName }}</div>
                </div>
                <div class="flex flex-col">
                    <el-tag class="mx-1 mb-2 p-4 text-lg" size="large">{{ getUserRolle }}</el-tag>
                    <el-tag class="mx-1 p-4 text-lg" type="success" size="large">{{ getSex }}</el-tag>
                </div>
            </div>
            <div class="flex">
                <section v-if="!loading" class="column-brain backdrop-blur-sm bg-white/30 ml-8 rounded-xl shadow-lg p-5 column-block2">
                    <h1 class="text-lg font-bold mb-2">Достижения пользователя</h1>
                    <div v-if="currentUser.achivments.length == 0" class="empty-block flex justify-center items-center h-full -mt-10">
                        <CatIcon class="-mb-20" :size="110" :color="'#d6d6d680'" />
                        <p class="text-black">Вы не прошли опрос "На взлет"</p>
                    </div>
                    <div v-else class="flex flex-col">
                        <div v-for="item in currentUser.achivments">
                            {{ item.name }}. Тип: {{ item.eventType.name }}
                        </div>
                    </div>
                </section>
                <section class="column-brain backdrop-blur-sm bg-white/30 ml-8 rounded-xl shadow-lg p-5 column-block2">
                    <h1 class="text-lg font-bold mb-2">Навыки пользователя</h1>
                    <div v-if="currentUser.skills.length == 0" class="empty-block flex justify-center items-center h-full -mt-10">
                        <CatIcon class="-mb-20" :size="110" :color="'#d6d6d680'" />
                        <p class="text-black">Вы не прошли опрос "На взлет"</p>
                    </div>
                    <div v-else class="flex flex-col">
                        <div v-for="item in currentUser.skills">
                            {{ item.type.name }}. Опыт {{ item.ageExperience }} лет
                        </div>
                    </div>
                </section>
            </div>
        </div>
        <section class="column-block mt-[50px] ml-10 w-5/12 backdrop-blur-sm bg-white/30 rounded-xl shadow-lg p-5">
            <h1 class="text-lg font-bold mb-2">Мероприятия пользователя</h1>
            <div v-if="eventsIsEmpty" class="empty-block flex justify-center items-center h-full -mt-10">
                <CatIcon class="-mb-20" :size="110" :color="'#d6d6d680'" />
                <p class="text-black">Вы не записались на меропиятие</p>
            </div>
            <div v-for="temp in eventsUser" :key="temp.id" class="w-full flex p-5 flex-nowrap rounded-xl backdrop-blur-sm bg-white/30 mb-2">
                <el-image style="height: 100px" class="mr-8" :src="temp.imageUrl" fit="contain" />
                <div class="flex flex-col justify-between">
                    <p class="text-xl font-bold mb-2">{{ temp.name }}</p>
                    <p class="mb-2">{{  substringDescription(temp.desctiption) }}</p>
                    <p class="mb-2">{{ temp.endDate == null || temp.endDate == temp.startDate ? new Date(temp.startDate).toLocaleDateString() : new Date(temp.startDate).toLocaleDateString() + " - " + new Date(temp.endDate).toLocaleDateString() }}</p>
                    <div class="flex justify-between">
                        <div>
                            <el-tag type="warning" size="large" class="mb-2 mr-2">{{ temp.type?.name }}</el-tag>
                            <el-tag class="mb-2" size="large">{{ temp.category?.name }}</el-tag>
                        </div>
                        <el-button v-if="temp.userIsRegister" type="danger" class="backdrop-blur-sm bg-white/30" plain @click="unregister(temp.id)">Отозвать заявку</el-button>
                        <el-button v-else class="backdrop-blur-sm bg-white/30" plain @click="register(temp.id)">Подать заявку</el-button>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <TheAnimationBackground/>
    <FlyForm v-model:dialogVisible="dialogVisible" />
</template>

<script setup lang="ts">
import TheAnimationBackground from "@/components/TheAnimationBackground.vue";
import { SexType } from "@/models/SexType";
import User from "@/models/User";
import { UserRole } from "@/models/UserRole";
import { useUserStore } from "@/stores/UserStore";
import { computed, ref } from "vue";
import EventService from "@/services/EventService";
import type { IEvent } from "@/models/Event";
import userService from "@/services/UserService"
import CatIcon from "@/components/ui/icons/CatIcon.vue";
import FlyForm from "@/views/FlyForm.vue"

const loading = ref(true);

const userStore = useUserStore();

const currentUser = ref({} as User);
const eventsUser = ref([] as Array<IEvent>);
const dialogVisible = ref(false)

const get = () => {
    console.log(currentUser)
    return currentUser.value.achivments[0].id
}

const update = async () => { 
    let temp = await EventService.getUserEvents();
    eventsUser.value = temp;
};

const setDialog = () => {
    console.log("1");
    dialogVisible.value = true
}

(async () => {
    loading.value = true;
    const test = await userStore.getCurentUser();
    console.log(test);
    currentUser.value = new User(test);
    await update();
    loading.value = false;
})();

const getFirstName = computed(() => {
    if(currentUser.value.name == null) return "";
    return currentUser.value.name[0];
});

const getUserRolle = computed(() => {
    if(currentUser.value.role == UserRole.Administrator) return "Администратор";
    if(currentUser.value.role == UserRole.Participant) return "Участник";
    return "Организатор";
});

const getSex = computed(() => {
    return currentUser.value.sex == SexType.Man ? "Мужчина" : "Женщина";
});

const substringDescription = (description: string) => {
    return description.substring(0, 90) + "...";
}

const register = async (eventId: number) => {
    await userService.eventRegistration(eventId);
    (eventsUser.value?.find(x => x.id == eventId) as IEvent).userIsRegister = true;
    await update();
}

const unregister = async (eventId: number) => {
    await userService.eventUnRegistration(eventId);
    (eventsUser.value?.find(x => x.id == eventId) as IEvent).userIsRegister = false;
    await update();
}

const eventsIsEmpty = computed(() => {
    return eventsUser.value.length == 0;
});

</script>

<style scoped>
h1 {
    font-size: 19px;
}

.empty-block {
    color: #d6d6d680;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    font-size: 20px;
}

.name { 
    margin: 15px 0;
    font-size: 27px;
    font-weight: 500;
}
.row-content {
    font-size: 18px;
}

.row-title {
    color: #0000008f;
    width: 100px;
    text-align: right;
}

.column-userinfo {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 5px;
    overflow: hidden;
}

.role-content {
    color: #6a8e4f;
}

.mb-10 {
    margin-bottom: 10px;
}

.column-block {
    flex: 1;
    height: 80vh;
    overflow-y: auto;
}

.column-block2 {
    flex: 1;
    height: 48vh;
    overflow-y: auto;
}
.main-block {
    display: flex;
    flex-direction: row;
}
.direction-column {
    flex-direction: column;
}

.m-5 {
    margin: 5px;
}

.mb-15 {
    margin-bottom: 15px;
}

.mr-15 {
    margin-right: 15px;
}

.title {
    margin: 5px 0;
    font-size: 17px;
    font-weight: 500;
}

.row {
    display: flex;
    justify-content: space-between;
}

.avatar {
    width: 300px;
    height: 300px;
    margin: 0 auto;
    margin-top: -50px;
    margin-bottom: -20px;
    border-radius: 100%;
    background: #dd3c3cd9;
    display: flex;
    justify-content: center;
    color: #ffffff;
    align-items: center;
    font-size: 120px;
    text-shadow: 4px 4px 2px rgba(0,0,0,0.6);
    border: 2px solid #bb4646;
}

.flex-column {
    flex-direction: column;
}
</style>
