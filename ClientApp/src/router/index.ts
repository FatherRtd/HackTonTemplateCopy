import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import TheLogin from '@/views/TheLogin.vue'
import TheAdminPanel from '@/views/TheAdminPanel.vue'
import NotFound from '@/views/NotFound.vue'
import Signup from '@/views/TheSignup.vue'
import Profile from '@/views/TheProfile.vue'
import EventEditor from '@/views/EventEditor.vue'
import SupportMeasures from '@/views/SupportMeasures.vue'

import { useAuthenticationStore } from '@/stores/AuthenticationStore'

export enum Routes {
    Home = 'home',
    Login = 'login',
    Signup = 'signup',
    Admin = 'admin',
    Profile = 'profile',
    EventEditor = 'event-editor',
    SupportMeasures = 'support-measures'
}

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: Routes.Home,
            component: HomeView
        },
        {
            path: '/login',
            name: Routes.Login,
            component: TheLogin
        },
        {
            path: '/signup',
            name: Routes.Signup,
            component: Signup
        },
        {
            path: '/profile',
            name: Routes.Profile,
            component: Profile
        },
        {
            path: '/admin',
            name: Routes.Admin,
            component: TheAdminPanel
        },
        {
            path: '/event-editor',
            name: Routes.EventEditor,
            component: EventEditor
        },
        {
            path: '/support-measures',
            name: Routes.SupportMeasures,
            component: SupportMeasures
        },
        {
            path: '/:pathMatch(.*)*',
            name: 'NotFound',
            component: NotFound
        }
    ]
})

router.beforeEach(async (to, from, next) => {
    const AdminRouteName = 'Admin'
    const LoginRouteName = 'Login'

    const UserStore = useAuthenticationStore()

    if (to.name == AdminRouteName) {
        if (UserStore.isAuthenticated) {
            next()
            return
        }
        router.push(LoginRouteName)
    }

    if (to.name == LoginRouteName) {
        if (!UserStore.isAuthenticated) {
            next()
            return
        }
        router.push(AdminRouteName)
    }

    next()
})

export default router
