<template>
    <div>
        <!--top-->
        <!-- <a href="#" rel="nofollow" target="_blank">
            <div style="width: 100%; height: 90px; background:url(/data/upload/site/201811/5c0140c425dae.png) no-repeat center;"></div>
            </a> -->
		<div class="top">
			<div class="container cl">
				<div class="f-l">
					<p class="c-666"> </p>
				</div>
				<div class="f-r">

					<p><a href="login/index" class="c-main">亲，请登录</a><span>或</span>
                    <a href="login/register">免费注册</a></p>
				
					<p><a rel="nofollow" href="http://wpa.qq.com/msgrd?v=3&uin={:C('yh_qq')}&site=qq&menu=yes">在线客服</a></p>
					<p><a href="javascript:;" class="btn_baoming" msg="请不要修改“卖家报名”否则将无法享受推券客免费产品服务">商家报名</a></p>
				</div>
			</div>	
		</div>
		<!--head-->
		<div class="head">
			<div class="container cl">
				<div class="logo">
					<a href="/" ><img width="250" height="70" src="/static/pc/images/logo.png"></a>
				</div>
				<div class="search">
				
						<input type="text" v-model="keyWord" class="input-text" placeholder="请输入您要查找的优惠券商品名称" />
						<i class="iconfont icon-sousuo"></i>
                           
					
					   <router-link  :to="{
                                path: '/list', 
                                params: { 
                                    keyword: keyWord, 
                                },
                                query: {
                                    keyword: keyWord, 
                                }
                            }"
                        class="btn btn-main radius">搜索</router-link>
				</div>
				<div class="barcode cl">
					<div class="f-l mr-5">
						<img src="/static/pc/images/logo.png" width="80" height="80">
					</div>
					<div class="f-l text-c">
						<h5 class="c-main">微信扫一扫</h5>
						<p>关注微信公众号<br>领券更方便</p>
					</div>
				</div>
			</div>
		</div>
		<!--nav-->
        <div class="navigation"><div class="container cl"><ul><li ><a href="/">首页</a></li>
        <li ><a href="/list">超级人气榜</a></li>
        <li  ><a href="/list">特卖精选</a></li>
        <li  ><a href="/list">九块九包邮</a></li>
        <li  ><a href="/list">二十元封顶</a></li>
        <li  ><a href="/list">优惠券头条</a></li></ul>
        </div>
        </div>
        <!--series-->
        <div class="series">
            <div class="container cl">
                <ul>
                    <li class="cur"><router-link to='/list'>全部</router-link></li>
                    <li ><a href="/list">女装</a></li>
                    <li ><a href="/list">男装</a></li>
                    <li ><a href="/list">鞋包</a></li>
                    <li ><a href="/list">美妆</a></li>
                    <li ><a href="/list">母婴</a></li>
                    <li ><a href="/list">食品</a></li>
                    <li ><a href="/list">内衣</a></li>
                    <li ><a href="/list">数码</a></li>
                    <li ><a href="/list">家居用品</a></li>
                    <li ><a href="/list">文体车品</a></li></ul>
             </div>
        </div>
 
</div>
</template>
<script>
    export default {
        data() {
            return {
                title: 'Header',
                keyWord:"",
            }
        },
        created() {
            
        },
        computed:{
            username(){
                let username = localStorage.getItem('ms_username');
                return username ? username : this.name;
            }
        },
        methods: {
           
        },
        mounted(){
        }
    }
</script>