<template>
    <div>
        Админка
        <div class="button-block" @click="logoutClick">
            <LogoutIcon :size="20" />
            {{ test }}
        </div>
        <TheAnimationBackground/>
    </div>
</template>

<script setup lang="ts">
import LogoutIcon from '@/components/ui/icons/LogoutIcon.vue'
import { ref, computed } from 'vue'
import { useAuthenticationStore } from '@/stores/AuthenticationStore'
import { useRouter } from 'vue-router'
import _ from 'lodash'
import TestService from '@/services/TestService'
import TheAnimationBackground from "@/components/TheAnimationBackground.vue";


const authenticationStore = useAuthenticationStore()
const router = useRouter()

const test = ref('');

(async () => {
    var response = await TestService.test()
    test.value = response.value
})()

const logoutClick = () => {
    authenticationStore.logout()
    goToLogin()
}

const goToLogin = () => {
    router.push('Login')
}
</script>
