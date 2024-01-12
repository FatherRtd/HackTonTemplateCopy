<template>
    <div class="flex items-center justify-center direction-column fix-position">
        <el-tabs v-loading.fullscreen.lock="loading" :tab-position="tabPosition" class="demo-tabs">
            <el-tab-pane label="Вход">
                <div class="login-form rounded-xl backdrop-blur-sm bg-white/60">
                    <div class="mb-1">
                        <el-input
                            v-model="loginData.login"
                            aria-autocomplete="none"
                            placeholder="Логин"
                        />
                    </div>
                    <div class="mb-1">
                        <el-input
                            v-model="loginData.password"
                            aria-autocomplete="none"
                            type="password"
                            placeholder="Пароль"
                        />
                    </div>
                    <el-button
                        class="bg-white"
                        :icon="Right"
                        :disabled="!loginDataIsValid"
                        @click="loginClickHandler"
                    >
                        Авторизоваться
                    </el-button>
                </div>
            </el-tab-pane>
            <el-tab-pane label="Регистрация">
                <div class="registration-form rounded-xl backdrop-blur-sm bg-white/60">
                    <div class="mb-1">
                        <el-input
                            v-model="registrationData.login"
                            aria-autocomplete="none"
                            placeholder="Логин"
                        />
                    </div>
                    <div class="mb-1">
                        <el-input
                            v-model="registrationData.mail"
                            aria-autocomplete="none"
                            placeholder="Email"
                        />
                    </div>
                    <div class="mb-1">
                        <el-input
                            v-model="registrationData.name"
                            aria-autocomplete="none"
                            placeholder="Имя"
                        />
                    </div>
                    <div class="mb-1">
                        <el-input
                            v-model="registrationData.password"
                            aria-autocomplete="none"
                            type="password"
                            placeholder="Пароль"
                        />
                    </div>
                    <div class="mb-1">
                        <el-select
                            v-model="registrationData.sex"
                            placeholder="Пол"
                            class="el-select-fix"
                        >
                            <el-option
                                v-for="item in sexTypeOptions"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value"
                            />
                        </el-select>
                    </div>
                    <el-button class="bg-white" :icon="Top" @click="registrationClickHandler">
                        Зарегистрироваться
                    </el-button>
                </div>
            </el-tab-pane>
        </el-tabs>
    </div>
    <TheAnimationBackground />
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { Top } from '@element-plus/icons-vue'
import { Right } from '@element-plus/icons-vue'
import { SexType } from '@/models/SexType'
import type { LoginData } from '@/models/LoginData'
import type User from '@/models/User'
import { useAuthenticationStore } from '@/stores/AuthenticationStore'
import { useUserStore } from '@/stores/UserStore'
import { ElMessage } from 'element-plus'
import Router, { Routes } from '@/router'
import TheAnimationBackground from '@/components/TheAnimationBackground.vue'

const authenticationStore = useAuthenticationStore()
const userStore = useUserStore()

const tabPosition = ref('left')

const loginData = ref({} as LoginData)
loginData.value.login = ''
loginData.value.password = ''

const registrationData = ref({} as User)
registrationData.value.mail = ''
registrationData.value.login = ''
registrationData.value.password = ''
registrationData.value.name = ''

const loading = ref(false)

const sexTypeOptions = [
    {
        value: SexType.Man,
        label: 'Мужчина'
    },
    {
        value: SexType.Woman,
        label: 'Женщина'
    }
]

const registrationClickHandler = async () => {
    loading.value = true
    var authenticationResponse = await authenticationStore.register(registrationData.value)

    var message = authenticationResponse.isSuccessful
        ? 'Авторизация прошла успешно'
        : authenticationResponse.message
    showWindow(message, authenticationResponse.isSuccessful)
    if (authenticationResponse.isSuccessful) {
        userStore.fillCurrentUser()
    }
    await updateUser()
    loading.value = false
}

const loginDataIsValid = computed(() => {
    return (
        loginData.value.login != '' &&
        loginData.value.password != '' &&
        loginData.value.password.length > 1 &&
        loginData.value.login.length > 1
    )
})

const registrationDataIsValid = computed(() => {
    return (
        registrationData.value.login != '' &&
        registrationData.value.password != '' &&
        registrationData.value.name != '' &&
        registrationData.value.mail != '' &&
        registrationData.value.password.length > 5 &&
        registrationData.value.login.length > 5 &&
        registrationData.value.name.length > 5 &&
        registrationData.value.mail.length > 5
    )
})

const loginClickHandler = async () => {
    loading.value = true
    var authenticationResponse = await authenticationStore.login(
        loginData.value.login,
        loginData.value.password
    )
    var message = authenticationResponse.isSuccessful
        ? 'Авторизация прошла успешно'
        : authenticationResponse.message
    showWindow(message, authenticationResponse.isSuccessful)
    if (authenticationResponse.isSuccessful) {
        userStore.fillCurrentUser()
    }
    await updateUser()
    loading.value = false
}

const showWindow = (value: string, isSuccessful: boolean) => {
    ElMessage({
        showClose: true,
        dangerouslyUseHTMLString: true,
        message: value,
        type: !isSuccessful ? 'error' : 'success'
    })
}

const updateUser = async () => {
    if (authenticationStore.isAuthenticated) {
        goToProfile()
        loading.value = false
    }
}

const goToProfile = () => {
    Router.push(Routes.Profile)
}
</script>

<style scoped>
.fix-position {
    height: calc(100vh - 90px);
}
.flex-center {
    display: flex;
    justify-content: center;
}
.el-select-fix {
    width: 100%;
}

.login-form {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 1rem;
    padding: 18px;
    border-radius: 10px;
}
.login-form > div {
    width: 300px;
}

.direction-column {
    flex-direction: column;
}

.registration-form {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 1rem;
    padding: 18px;
    border-radius: 10px;
}
.registration-form > div {
    width: 300px;
}
</style>
