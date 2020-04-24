import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App.vue'
import LocationByIp from './LocationByIp.vue'
import LocationByCity from './LocationByCity.vue'

Vue.config.productionTip = false
Vue.use(VueRouter)

const routes = [
    {
        path: '/location-by-ip',
        component: LocationByIp
    },
    {
        path: '/locations-by-city',
        component: LocationByCity
    }
]

const router = new VueRouter({
    routes,
    mode: 'history'
})

new Vue({
    el: '#app',
    render: h => h(App),
    router
})