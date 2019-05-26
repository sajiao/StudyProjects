<template>
    <div>
        <v-head></v-head>
      
    </div>
</template>

<script>
    import vHead from './MHeader.vue';
    export default {
        data(){
            return {
                tagsList: [],
                collapse: false
            }
        },
        components:{
            vHead
        },
        created(){
     
        }
    }
</script>
