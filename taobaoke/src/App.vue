<template>
    <div id="app">
        <router-view></router-view>
    </div>
</template>
<script>
    import session from '@/store/storage';

    export default {
        data() {
            return {
               
            }
        },
        created() {
            //页面刚进入时开启长连接
            //this.getRegisterInformation();
            
        },
        methods: {
          
            getRegisterInformation() {
                /*
                registerService.get().then((res) => {
                    if (res && res.statusText == "OK") {
                        if (res.data) {
                            if (!res.data.isPassVerification) {
                                var isShowTopMenu = false;
                                eventBus.$emit("isShowTopMenu", isShowTopMenu);
                                session.set("SeriesFromServer", res.data.systemSeriesNumber);
                                this.$router.push('/register');
                            }
                        }
                    }
                });
                */
            }
        },
        mounted() {
            
        },
    }
</script>
<style>
   
</style>
