<template>
    <el-tooltip effect="dark" :content="props.title" :hide-after="0" placement="right-start">
        <router-link
            v-if="props.to"
            class="h-8 w-8 rounded flex items-center justify-center cursor-pointer text-black hover:bg-gray-200 transition-all ease-out duration-500"
            :class="{ 'route-active': isActive }"
            :to="{ name: props.to }"
        >
            <slot name="icon" />
        </router-link>
        <div
            v-else
            class="h-8 w-8 rounded flex items-center justify-center cursor-pointer text-black hover:bg-gray-200 transition-all ease-out duration-500"
        >
            <slot name="icon" />
        </div>
    </el-tooltip>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'

const props = defineProps({
    title: { type: String, required: true },
    to: { type: String, default: '' }
})

const route = useRoute()
const isActive = computed(() => {
    return props.to === route.name
})
</script>

<style scoped>
.route-active {
    color: white;
    border-radius: 20%;
    width: 50px;
    height: 50px;
    display: flex;
    align-items: center;
    justify-content: center;
    color: black;
    background-color: rgba(234, 246, 246, 0.5);
}
</style>
