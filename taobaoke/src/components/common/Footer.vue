<template>
    <div>
       <!--foot-->
<div class="foot">
<div class="container cl">
<ul><li>
    <dl><dt>关于我们</dt>
    <dd><a href="/index.php/help/index/id/1.html">关于推券客联盟</a></dd>
    <dd><a href="/index.php/help/index/id/3.html">免责声明</a></dd>
    </dl></li><li><dl><dt>帮助中心</dt>
    <dd><a href="/index.php/help/index/id/4.html">新手指南</a></dd>
    <dd><a href="/index.php/help/index/id/2.html">联系我们</a></dd></dl></li>
    <li><dl><dt>微信扫一扫</dt><dd>
        <div class="public"><img src="/data/upload/site/59b79fa615434.png"><p>关注公众号</p></div>
        </dd></dl></li><li><dl><dd><a href="/index.php/top100">超级人气榜</a></dd>
        <dd><a href="/index.php/jingxuan">特卖精选</a></dd>
        <dd><a href="/index.php/jiu">九块九包邮</a></dd>
        <dd><a href="/index.php/ershi">二十元封顶</a></dd>
        <dd><a href="/index.php/article/index.html">优惠券头条</a></dd></dl></li>
        <li>
            <dl><dt>商家合作</dt><dd><a href="javascript:;" class="btn_baoming">商家报名</a></dd></dl></li></ul>
</div>
</div>
<!--bottom-->
<div class="bottom"><p>Copyright © 2019-2020 推券客联盟 版权所有  苏ICP备9999999号</p>
<p style="display: none;"></p></div>
<div id="back_top" class="back_top">
    <a href="javascript:;" class="call-top" title="返回顶部" _hover-ignore="1" style="display: block;"></a>
    <a id="checkTrap" rel="nofollow" class="checkTrap" target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=234234234&site=qq&menu=yes">
    <span class="call-check" title="联系客服"></span></a>
    </div>
  </div>
</template>
<script>
    import eventBus from '../common/bus';
    export default {
        data() {
            return {
                activeIndex: '2',
                isStartExamination:false,
                title: '考试模块',
                isShowMenu:true,
            }
        },
        created() {
            eventBus.$on('StartExaminationLockMenu', target => {
                this.isStartExamination = target.data;
            });

            eventBus.$on('isShowTopMenu', target => {
                this.isShowMenu = target;
            });
        },
        computed:{
            username(){
                let username = localStorage.getItem('ms_username');
                return username ? username : this.name;
            }
        },
        methods: {
            handleSelect(key, keyPath) {
                if (key == 1) {
                    this.$router.push('/test');
                } else if (key == 2) {
                    this.$router.push('/score');
                } 
                //else if (key == 4) {
                //    this.$router.push('/socket');
                //}
            },
            // 用户名下拉菜单选择事件
            handleCommand(command) {
                if(command == 'loginout'){
                    localStorage.removeItem('ms_username')
                    this.$router.push('/login');
                }
            },
      
        },
        mounted(){
        }
    }
</script>

