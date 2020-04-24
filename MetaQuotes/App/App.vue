<template>
    <div id="app">
        <header>
            <h1>MetaQuotes</h1>
        </header>

        <div style="display: flex; flex-direction:row">
            <aside class="sidebar">
                <div>
                    <router-link to="/location-by-ip">Get location by ip</router-link>
                </div>
                <div>
                    <router-link to="/locations-by-city">Get location by city</router-link>
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
        width: 200px;
    }
</style>