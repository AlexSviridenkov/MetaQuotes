<template>
    <div style="display: flex; flex-direction:column">
        <div>
            <span>Город</span>
            <input v-model="cityName" />
            <button @click="getLocations">Искать</button>
        </div>

        <location-table style="margin-top:30px" :locations="locations"></location-table>

    </div>
</template>

<script>
    import LocationTable from "./LocationTable.vue"
    import axios from 'axios';

    export default {
        components: {
            LocationTable
        },
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