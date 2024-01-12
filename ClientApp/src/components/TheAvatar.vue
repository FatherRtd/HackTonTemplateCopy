<template>
    <div class="outer" @click="clickHandler">
        <div :class="'outer__button button__bottom-top ' + (props.disabel ? 'disabel' : '')">
            <div :class="'text' + (props.small ? ' small' : '')">{{ props.text }}</div>
        </div>
    </div>
</template>
<script setup lang="ts">
const props = withDefaults(
    defineProps<{
        disabel: boolean
        text: string
        small?: boolean
    }>(),
    {
        small: false
    }
)

const emits = defineEmits<{
    (e: 'click'): void
}>()

const clickHandler = () => {
    emits('click')
}
</script>
