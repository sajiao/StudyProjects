<template>
    <div>
       <!--foot-->
<div class="foot">
<div class="container cl">
<ul><li>
    <dl><dt>关于我们</dt>
    <dd><a href="#">免责声明</a></dd>
    </dl></li><li><dl><dt>帮助中心</dt>
    <dd><a href="#">新手指南</a></dd>
    <dd><a href="#">联系我们</a></dd></dl></li>
    <li><dl><dt>微信扫一扫</dt><dd>
        <div class="public"><img src="/data/upload/site/59b79fa615434.png"><p>关注公众号</p></div>
        </dd></dl></li><li><dl>
		<dd>
			<router-link  :to="{
		                       path: '/list', 
		                       query: {
								   tag: '人气', 
		                       }
		                    }">超级人气榜</router-link>
		
		</dd>
        <dd><router-link  :to="{
		                       path: '/list', 
		                       query: {
								   tag: '特卖', 
		                       }
		                    }">特卖精选</router-link></a></dd>
	   <dd><router-link  :to="{
					   path: '/list', 
					   query: {
						   tag: '精选', 
					   }
					}">精选</router-link></a></dd>
        <dd><router-link  :to="{
		                       path: '/list', 
		                       query: {
								   tag: '9.9', 
		                       }
		                    }">九块九包邮</router-link> 
							</dd>
        <dd>
			<router-link  :to="{
		                       path: '/list', 
		                       query: {
								  tag: '20', 
		                       }
		                    }">二十元封顶</router-link> </dd>
	     	</dl>
		</li>

        <li>
            <dl><dt>商家合作</dt><dd><a href="javascript:;" class="btn_baoming">商家报名</a></dd></dl>
			
			</li>
			</ul>
</div>
</div>
<!--bottom-->
<div class="bottom"><p>Copyright © 2019-2020 无忧优惠券 版权所有  蜀ICP备9999999号</p>
<p style="display: none;"></p></div>
<div id="back_top" class="back_top">
    <a href="#top" class="call-top" title="返回顶部" _hover-ignore="1" style="display: block;"></a>
   <!-- <a id="checkTrap" rel="nofollow" class="checkTrap" target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=234234234&site=qq&menu=yes">
    <span class="call-check" title="联系客服"></span></a>-->
    </div>
  </div>
</template>
<script>

    export default {
        data() {
            return {
              
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

