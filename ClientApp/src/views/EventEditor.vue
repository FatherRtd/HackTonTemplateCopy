<template>
    <div class="fix-overflow">
        <div class="flex items-center justify-center flex-wrap w-10/12 my-0 mx-auto scroll-fix">
            <div class="flex justify-between w-full my-10">
                <el-button class="backdrop-blur-sm bg-white/30" plain @click="update">Обновить</el-button>
                <el-button type="primary" class="backdrop-blur-sm bg-white/30" plain @click="dialogVisible = true">Добавить новое мероприятие</el-button>
            </div>
            <div v-for="temp in test" class="w-full flex p-5 flex-nowrap rounded-xl backdrop-blur-sm bg-white/30 mb-2">
                <el-image style="height: 200px" class="mr-8" :src="temp.imageUrl" fit="contain" />
                <div class="flex flex-col justify-between">
                    <p class="text-xl font-bold mb-2">{{ temp.name }}</p>
                    <p class="mb-2">{{ temp.desctiption }}</p>
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
        </div>

        <el-dialog v-model="dialogVisible" title="Создание мероприятия" width="50%" draggable>
            <el-form :model="form" label-width="180px">
                <el-form-item label="Наименование">
                    <el-input v-model="form.name" placeholder="Введите наименование мероприятия" />
                </el-form-item>

                <el-form-item label="Тип">
                    <el-select v-model="form.type.id" placeholder="Выберите тип">
                        <el-option v-for="type in eventTypes" :key="type.id" :label="type.name" :value="type.id" />
                    </el-select>
                </el-form-item>

                <el-form-item label="Категория">
                    <el-select v-model="form.category.id" placeholder="Выберите категорию">
                        <el-option v-for="category in eventCategories" :key="category.id" :label="category.name" :value="category.id" />
                    </el-select>
                </el-form-item>

                <el-form-item label="Онлайн">
                    <el-switch v-model="form.isOnline" />
                </el-form-item>

                <el-form-item v-if="!form.isOnline" label="Адрес">
                    <el-input v-model="form.address" placeholder="Введите адрес проведения мероприятия" />
                </el-form-item>

                <el-form-item label="Сылка на изображение">
                    <el-input v-model="form.imageUrl" placeholder="Введите ссылку на изображение" />
                </el-form-item>

                <el-form-item label="Сылка на сайт">
                    <el-input v-model="form.siteUrl" placeholder="Введите ссылку на сайт мероприятия" />
                </el-form-item>

                <el-form-item label="Дата начала">
                    <el-date-picker v-model="form.startDate" type="date" placeholder="Выберите дату начала"/>
                </el-form-item>

                <el-form-item label="Дата окончания">
                    <el-date-picker v-model="form.endDate" type="date" placeholder="Выберите дату окончания"/>
                </el-form-item>

                <el-form-item label="Описание">
                    <el-input v-model="form.desctiption" :rows="3" placeholder="Введите описание мероприятия" type="textarea" />
                </el-form-item>
            </el-form>
            <template #footer>
                <span class="dialog-footer">
                    <el-button @click="dialogVisible = false">Отмена</el-button>
                    <el-button type="primary" plain @click="onSubmit">Сохранить</el-button>
                </span>
            </template>
        </el-dialog>
        <TheAnimationBackground/>
    </div>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'
import eventService from "@/services/EventService"
import userService from "@/services/UserService"
import Event, { type IEvent } from '@/models/Event';
import type { IEventType } from '@/models/EventType';
import type { IEventCategory } from '@/models/EventCategory';
import TheAnimationBackground from "@/components/TheAnimationBackground.vue";
import User from '@/models/User';

const test = ref<IEvent[] | null>();
const dialogVisible = ref(false);
const eventTypes = ref<IEventType[] | null>();
const eventCategories = ref<IEventCategory[] | null>();
const form = reactive(new Event(null));

const update = async () => { 
    let temp = await eventService.getEvents();
    test.value = temp;
};

const onSubmit = async () => {
    var user = await userService.getCurentUser();
    form.createUser = new User(user);

    await eventService.saveEvent(form);
    await update();
    dialogVisible.value = false;
}

const register = async (eventId: number) => {
    await userService.eventRegistration(eventId);
    (test.value?.find(x => x.id == eventId) as Event).userIsRegister = true;
}

const unregister = async (eventId: number) => {
    await userService.eventUnRegistration(eventId);
    (test.value?.find(x => x.id == eventId) as Event).userIsRegister = false;
}

(async () => {
    await update();
    eventTypes.value = await eventService.getEventTypes();
    eventCategories.value = await eventService.getEventCategories();
})();
</script>

<style scoped>
.fix-overflow {
    height: 93vh;
    overflow-y: auto !important;
}
.login-form {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 1rem;
    padding: 0.5rem;
    border-radius: 10px;
    background-color: rgba(234, 246, 246, 1);
}
.login-form > div {
    width: 300px;
}
</style>
