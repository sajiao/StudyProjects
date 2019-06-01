<template>
    <div>
        <!--top-->
        <!-- <a href="#" rel="nofollow" target="_blank">
            <div style="width: 100%; height: 90px; background:url(/data/upload/site/201811/5c0140c425dae.png) no-repeat center;"></div>
            </a> -->
		<div class="top" id="top">
			<div class="container cl">
				<div class="f-l">
					<p class="c-666"> </p>
				</div>
				<div class="f-r">

					<!--<p><a href="login/index" class="c-main">亲，请登录</a><span>或</span>
                    <a href="login/register">免费注册</a></p>
				
					<p><a rel="nofollow" href="http://wpa.qq.com/msgrd?v=3&uin={:C('yh_qq')}&site=qq&menu=yes">在线客服</a></p>-->
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
				
						<input type="text" v-model="keyword" class="input-text" placeholder="请输入您要查找的优惠券商品名称" />
						<i class="iconfont icon-sousuo"></i>
                           
			
				</div>
			   <div class="searchbtn">
							
						 <router-link  :to="{
						         path: '/list', 
						         query: {
						             keyword: keyword, 
									 isFull:false,
						         }
						     }"
						class="btn-main radius">搜索</router-link>
												
						 <router-link  :to="{
								 path: '/list', 
								 query: {
									 keyword: keyword, 
									 isFull:true,
								 }
							 }"
						class="btn-main radius">搜全网</router-link>
			
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
        <div class="navigation">
			<div class="container cl">
				<ul><li ><a href="/">首页</a></li>
					<li v-for="item in navList" :key="item.Name">
						<router-link  :to="{
										   path: '/list', 
										   query: {
											   tag: item.Tag, 
										   }
										}">{{item.Name}}</router-link>
					</li>
								
	            </ul>
        </div>
        </div>
        <!--series-->
        <div class="series">
            <div class="container cl">
                <ul>
                    <li class="cur"><router-link to='/list'>全部</router-link></li>
                    <li v-for="item in tagList" :key="item.id"> <router-link  :to="{
                               path: '/list', 
                               query: {
                                   tag: item.Tag, 
                               }
                            }">{{item.Tag}}</router-link></li>
                 </ul>
             </div>
        </div>
 
</div>
</template>
<script>
    export default {
        data() {
            return {
                title: 'Header',
                keyword:"",
				navList:[
						{Name:"特卖", Tag:"特卖"},
						{Name:"精选", Tag:"精选"},
						{Name:"女装", Tag:"女装"},
						{Name:"九块九包邮", Tag:"9.9"},
						{Name:"二十元封顶", Tag:"20"},
						{Name:"超级人气榜", Tag:"人气"},
					],
				tagList:[
					{id:1, Tag:"女装"},
					{id:2, Tag:"男装"},
					{id:2, Tag:"鞋包"},
					{id:4, Tag:"母婴"},
					{id:5, Tag:"食品"},
					{id:6, Tag:"内衣"},
					{id:7, Tag:"家用"},
					{id:8, Tag:"文体"},
					{id:9, Tag:"数码"},
					{id:10, Tag:"文具"},
					{id:11, Tag:"体育"},
					{id:12, Tag:"车"},
				],
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
<style>


</style>