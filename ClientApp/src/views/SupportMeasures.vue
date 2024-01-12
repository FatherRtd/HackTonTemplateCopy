<template>
    <div class="flex items-center justify-center flex-wrap w-10/12 my-0 mx-auto">
        <p class="text-xl font-bold mb-2">Различные возможности помощи разработки и сопровождения ваших проектов</p>
        <div class="flex flex-col justify-between w-full my-10">
            <p class="text-lg font-bold mb-2">Федеральные</p>
            <div class="flex flex-wrap mb-2">
                <el-card v-for="measur in support" class="box-card cursor-pointer m-2 backdrop-blur-sm bg-white/30" @click="() => {redirect(measur.link)}">
                    <span>{{measur.name}}</span>
                </el-card>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
const redirect = (link: string) => window.open(link);
class Measures {
    constructor(name: string, link: string){
        this.name = name;
        this.link = link;
    }
    name!: string;
    link!: string;
}
const support = new Array(
    new Measures("Налоговые льготы", "https://www.gosuslugi.ru/itindustry/tax_incentives"),
    new Measures("Отсрочка от призыва", "https://www.gosuslugi.ru/itindustry/army_delay"),
    new Measures("Льготные кредиты", "https://www.gosuslugi.ru/itindustry/soft_loans"),
    new Measures("Аккредитация", "https://www.gosuslugi.ru/itindustry/accreditation"),
    new Measures("Гранты", "https://www.gosuslugi.ru/itindustry/grants"),
    new Measures("Стимулирование спроса", "https://www.gosuslugi.ru/itindustry/demand_stimulation"),
    new Measures("Льготная ипотека", "https://www.gosuslugi.ru/itindustry/preferential_mortgage"),
    new Measures("Освобождение от проверок", "https://www.gosuslugi.ru/itindustry/checks_exemption"),
    new Measures("Трудоустройство и ВНЖ для иностранцев", "https://www.gosuslugi.ru/itindustry/employment_foreigners"),
    new Measures("Включение в реестры", "https://www.gosuslugi.ru/itindustry/inclusion_in_registers/inclusion_in_the_unified_register_of_russian_software"),
    new Measures("ИТ-образование", "https://www.gosuslugi.ru/itindustry/it_education/it_education"),
    new Measures("Привлечение финансирования", "https://www.gosuslugi.ru/itindustry/fundraising"),
    new Measures("Поддержка особо значимых проектов", "https://www.gosuslugi.ru/itindustry/significant_projects/particularly_significant_projects"),
    new Measures("Региональные меры поддержки", "https://depit.admhmao.ru/mery-podderzhki-proektov/"),
)
</script>

<style scoped>
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
