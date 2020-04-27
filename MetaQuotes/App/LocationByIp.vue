<template>
    <div style="display: flex; flex-direction:column">
        <div>
            <span>IP</span>
            <input v-model="ip" />
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
                ip: '',
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
                        this.locations = [resp.data];
                    });

            }
        },
    }
</script>

<style lang="scss">
</style>