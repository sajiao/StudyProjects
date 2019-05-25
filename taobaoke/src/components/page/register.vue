<template>
    <div class="table">
        <div class="container">
            <div class="handle-box" style="height:500px;">
                <br /><br /><br /><br />
                <h1>您还没有注册，请先注册！</h1>
                <br /><br /><br />
                <div>
                    请输入注册码：<el-input v-model="inputSeries" style="width:600px;"></el-input>
                    <el-button type="primary" @click="registerSeries">注 册</el-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import eventBus from '@/components/common/bus';
    import session from '@/store/storage';
    import registerService from '@/server/registerService';

    export default {
        name: 'tabs',
        data() {
            return {
                inputSeries: '',
                seriesFromServer:''
            }
        },
        created() {
            this.seriesFromServer = session.get("SeriesFromServer");
        },
        methods: {
            registerSeries() {
                console.log(this.inputSeries + "    " + this.seriesFromServer);
                if (this.inputSeries != this.seriesFromServer) {
                    this.$notify.error({
                        title: '错误',
                        message: '注册码输入错误, 注册失败, 请重新输入.'
                    });
                    this.inputSeries = "";
                } else {
                    registerService.post({ SeriesNumber: this.inputSeries}).then((res) => {                        
                        console.log(res);
                        if (res && res.data) {
                            this.$notify({
                                type: 'success',
                                title: '成功',
                                message: '注册成功'
                            });
                            var isShowTopMenu = true;
                            eventBus.$emit("isShowTopMenu", isShowTopMenu);
                            this.$router.push('/score');
                        } else {
                            this.$notify.error({
                                title: '错误',
                                message: "服务访问错误，请检查网络"
                            });
                        }
                    })
                }
            }
        },
        computed: {

        }
    }

</script>

<style>
    
</style>

