<template>
    <div id="app">
        <header>
            <h1>MetaQuotes</h1>
        </header>

        <div style="display: flex; flex-direction:row">
            <aside class="sidebar">
                <div>
                    <router-link to="/location-by-ip">Поиск местоположения по IP</router-link>
                </div>
                <div>
                    <router-link to="/locations-by-city">Поиск местоположений по названию города</router-link>
                </div>
            </aside>

            <main>
                <div class="content">
                    <div><router-view></router-view></div>
                </div>
            </main>
        </div>
    </div>
</template>

<script>
export default { }
</script>

<style lang="scss">
    aside {
        width: 300px;
    }

    main {
        margin-left: 20px;
    }

</style>