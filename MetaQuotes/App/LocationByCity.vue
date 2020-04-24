<template>
    <div style="display: flex; flex-direction:row">
        <div>
            <span>Город</span>
            <input v-model="cityName" />
            <button @click="getLocations">Искать</button>
        </div>

        <div style="display: flex; flex-direction:column">
            <div v-for="location in locations">
                <span>{{location.country}}</span>
                <span>{{location.region}}</span>
                <span>{{location.postal}}</span>
                <span>{{location.city}}</span>
                <span>{{location.organization}}</span>
                <span>{{location.latitude}}</span>
                <span>{{location.longitude}}</span>
            </div>
        </div>

    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                locations: [],
                cityName: '',
            }
        },
        methods: {
            getLocations() {
                axios.get('/city/locations', {
                    params: {
                        'city': this.cityName
                    }
                })
                .then((resp) => {
                    this.locations = resp.data;
                });

            }
        },
    }
</script>

<style lang="scss">
</style>