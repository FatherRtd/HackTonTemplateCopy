<template>
    <el-dialog v-model="props.dialogVisible" title="На взлет" width="50%" draggable>
        <el-form :model="form" label-width="180px">
            <el-form-item label="Имеете ли вы коммерческие проекты">
                <el-switch v-model="form.haveCommercialProject" />
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
                <el-button @click="onCancel">Отмена</el-button>
                <el-button type="primary" plain @click="onSubmit">Сохранить</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script setup lang="ts">
import { reactive, defineProps, computed } from 'vue'
import Opros from '@/models/Opros';
import UserService from '@/services/UserService';
import User from '@/models/User';

const form = reactive(new Opros(null));
const props = defineProps({
  dialogVisible: {
    type: Boolean,
    required: true
  }
})
const emit = defineEmits<{
    (event: "update:dialogVisible", value: Boolean) : void
}>()

const value = computed({
  get() {
    return props.dialogVisible
  },
  set(value) {
    emit('update:dialogVisible', value)
  }
})

const onSubmit = async () => {
    form.date = new Date();
    form.user = new User(await UserService.getCurentUser());
    value.value = false;
}

const onCancel = () => {
    value.value = false;
}
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
