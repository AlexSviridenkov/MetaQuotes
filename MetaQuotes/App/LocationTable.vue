<template>
    <div class="location-table">
        <table border="1">
            <tr>
                <td>Страна</td>
                <td>Регион</td>
                <td>Почтовый Код</td>
                <td>Город</td>
                <td>Организация</td>
                <td>Широта</td>
                <td>Долгота</td>
            </tr>
            <tr v-for="location in locations">
                <td>{{location.country}}</td>
                <td>{{location.region}}</td>
                <td>{{location.postal}}</td>
                <td>{{location.city}}</td>
                <td>{{location.organization}}</td>
                <td>{{location.latitude}}</td>
                <td>{{location.longitude}}</td>
            </tr>
        </table>
    </div>
</template>

<script>
    export default {
        props: {
            locations: Array
        },
    }
</script>

<style lang="scss">
    .location-table {
    }
</style>