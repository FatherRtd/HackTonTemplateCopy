<template>
    <nav class="flex flex-col justify-between fixX">
        <section class="flex flex-col items-center gap-y-3">
            <slot name="top" />
        </section>

        <section class="flex flex-col items-center gap-y-3">
            <slot name="bottom" />
        </section>           
    </nav>
</template>
<style>
.fixX {
    height: 80vh;
}
</style>
