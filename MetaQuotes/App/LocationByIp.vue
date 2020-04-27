<template>
    <div style="display: flex; flex-direction:column">
        <div>
            <div>
                <span>IP</span>
                <input v-model="ip" />
                <button @click="getLocations">Искать</button>
            </div>
            <span v-show="error">{{error}}</span>
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
                ip: '',
                error: '',
            }
        },
        methods: {
            getLocations() {
                axios.get('/ip/location', {
                    params: {
                        'ip': this.ip
                    }
                })
                    .then((resp) => {
                        this.error = '';
                        this.locations = [resp.data];
                    })
                    .catch((error) => {
                        if (error.response.data.code === 1)
                            this.error = "Неправильный формат IP";
                    });

            }
        },
    }
</script>

<style lang="scss">
</style>