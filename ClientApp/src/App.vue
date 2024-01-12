<template>
    <main class="min-h-screen relative flex">
        <aside class="min-h-screen w-[120px]">
            <div class="my-10 border-r-[2px] border-color py-3 px-2 fix-h">
                <div class="text-center mb-5">
                    <el-icon :size="40">
                        <ElementPlus />
                    </el-icon>
                    <h1>event</h1>
                </div>

                <UiSidebar>
                    <template #top>
                        <div v-if="isAuthenticated" class="main-page-btn">
                            <UiSidebarItem title="Профиль" :to="Routes.Profile">
                                <template #icon>
                                    <el-icon>
                                        <User :size="20" />
                                    </el-icon>
                                </template>
                            </UiSidebarItem>
                        </div>

                        <div v-if="!isAuthenticated" class="main-page-btn">
                            <UiSidebarItem title="Авторизация" :to="Routes.Signup">
                                <template #icon>
                                    <el-icon>
                                        <User :size="20" />
                                    </el-icon>
                                </template>
                            </UiSidebarItem>
                        </div>

                        <div class="main-page-btn">
                            <UiSidebarItem title="Календарь событий" :to="Routes.Home">
                                <template #icon>
                                    <el-icon>
                                        <Calendar :size="20" />
                                    </el-icon>
                                </template>
                            </UiSidebarItem>
                        </div>

                        <div class="main-page-btn">
                            <UiSidebarItem title="События" :to="Routes.EventEditor">
                                <template #icon>
                                    <el-icon>
                                        <Place />
                                    </el-icon>
                                </template>
                            </UiSidebarItem>
                        </div>

                        <div class="main-page-btn">
                            <UiSidebarItem title="Меры поддержки" :to="Routes.SupportMeasures">
                                <template #icon>
                                    <el-icon>
                                        <Menu />
                                    </el-icon>
                                </template>
                            </UiSidebarItem>
                        </div>
                    </template>

                    <template #bottom>
                        <div v-if="isAuthenticated" class="main-page-btn" @click="logoutClickHandler">
                            <UiSidebarItem title="Выход">
                                <template #icon>
                                    <el-icon><Exit :size="20" /></el-icon>
                                </template>
                            </UiSidebarItem>
                        </div>
                    </template>
                </UiSidebar>
            </div>
        </aside>

        <section class="flex-1 flex flex-col overflow-hidden full-h fix-fix">
            <div id="header-teleport" class="p-3 h-[57px] flex items-center justify-around">
                {{ getRouteName }}
            </div>

            <div>
                <RouterView v-slot="{ Component }">
                    <Component :is="Component" />
                </RouterView>
            </div>
        </section>
    </main>
</template>

<script setup lang="ts">
import { RouterView } from 'vue-router'
import { Calendar, User, ElementPlus, Search, Menu, Place } from '@element-plus/icons-vue'
import UiSidebar from '@/components/ui/sidebar/UiSidebar.vue'
import UiSidebarItem from '@/components/ui/sidebar/UiSidebarItem.vue'
import Exit from '@/components/ui/icons/SendIcon.vue'
import Router, { Routes } from '@/router'
import { computed, onMounted, ref } from 'vue'
import { useAuthenticationStore } from "@/stores/AuthenticationStore";
import { ElMessage } from 'element-plus'
import { useUserStore } from './stores/UserStore'

const userStore = useUserStore();
const authenticationStore = useAuthenticationStore();

const isAuthenticated = computed(() => {
    return authenticationStore.isAuthenticated;
});

const logoutClickHandler = async () => {
  authenticationStore.logout();
  userStore.clearCurrentUser();
  goToSignup();
  showWindow("Пользователь вышел с сайта", false);
}

const goToSignup = () => {
  Router.push(Routes.Signup);
}

const showWindow = (value: string, isSuccessful: boolean) => {
  ElMessage({
      showClose: true,
      dangerouslyUseHTMLString: true,
      message: value,
      type: !isSuccessful ? 'error' : 'success',
    });
}

const getRouteName = computed(() => {
    const routeByEnum = Router.currentRoute.value.name as Routes;
    if(routeByEnum == Routes.Signup) return "Авторизация";
    if(routeByEnum == Routes.Profile) return "Профиль пользователя"; 
    if(routeByEnum == Routes.EventEditor) return "События"; 
    return "Календарь событий";
});

</script>

<style scoped>
.fix-fix {
    height: 100vh !important;
}
.border-color {
    border-right: 2px solid #c7c7c752 !important;
}
.main-page-btn {
    border-radius: 20%;
    width: 50px;
    height: 50px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.fix-h {
    height: 90vh !important;
}
</style>

<style>
body {
    background: linear-gradient(
        135deg,
        rgba(206, 214, 255, 1) 0%,
        rgba(255, 253, 255, 1) 48%,
        rgba(224, 184, 193, 1) 100%
    );
}


::-webkit-scrollbar {
  width: 6px;
  height: 6px;
}

::-webkit-scrollbar-track {
  border-radius: 10px;
  background: rgba(0,0,0,0.1);
}

::-webkit-scrollbar-thumb{
  border-radius: 10px;
  background: rgba(0,0,0,0.2);
}

::-webkit-scrollbar-thumb:hover{
	background: rgba(0,0,0,0.4);
}

::-webkit-scrollbar-thumb:active{
	background: rgba(0,0,0,.9);
}
</style>
