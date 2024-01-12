import { createApp } from 'vue'
import { createPinia } from 'pinia'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import './index.css'
import ru from '@/components/ru';

import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(ElementPlus, {
    locale: ru
})
app.use(createPinia())
app.use(router)

app.mount('#app')
