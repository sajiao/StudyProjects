<template>
    <div>
        <div id="offCanvasWrapper" class="mui-off-canvas-wrap mui-draggable">
    <aside id="offCanvasSide" class="mui-off-canvas-right">
        <div id="offCanvasSideScroll" class="mui-scroll-wrapper">
        <!--slide-->
        <div class="mui-scroll">
            <div class="series"><h4>商品分类</h4>
            <div class="mui-row">
                <div class="mui-col-xs-4">
                <a href="m/list">全部商品</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/list/index/cid/1.html">女装</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/list/index/cid/2.html">男装</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/list/index/cid/3.html">鞋包</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/list/index/cid/6.html">美妆</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/list/index/cid/7.html">母婴</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/list/index/cid/8.html">食品</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/list/index/cid/9.html">内衣</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/list/index/cid/10.html">数码</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/list/index/cid/12.html">家居用品</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/list/index/cid/16.html">文体车品</a></div></div><h4>导航索引</h4>
            <div class="mui-row">
                <div class="mui-col-xs-4">
                <a href="/m">首页</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/xinping/index.html">今日新品</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/jiu/index.html">9块9包邮</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/ershi/index.html">20元封顶</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/jingxuan/index.html">特卖精选</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/top100/index.html">超级人气榜</a></div>
                <div class="mui-col-xs-4"><a href="/index.php/m/article/index.html">今日头条</a></div></div>
                </div></div>
        </div>
    </aside>
    <div class="mui-inner-wrap">
        <header id="header" class="mui-bar mui-bar-nav">
        <form action="/index.php/m/list/index.html" id="so" method="get">
            <!--<div class="mui-input-row mui-search"><input type="search" value="" name="k" onblur="$('#so').submit()" class="mui-input-clear" placeholder="搜搜看您想要的商品优惠券"></div>-->
            <div class="mui-input-row searchtop">
            <span class="mui-icon mui-icon-search"></span>
            <input type="text" value="" name="k" onblur="$('#so').submit()" placeholder="搜搜看您想要的商品优惠券"></div>
            <button class="headsub" type="submit">搜索</button></form>
        <a id="offCanvasBtn" href="#offCanvasSide" class="mui-icon mui-action-menu mui-icon-bars mui-pull-right"></a></header>
        <div id="offCanvasContentScroll" class="mui-content mui-scroll-wrapper">
        <div class="mui-scroll">
            <!--banner-->
        <div id="slider" class="mui-slider banner">
         <div class="mui-slider-group">
            <div class="mui-slider-item">
            <a href="/m">
            <img src="/data/upload/ad/5971438c5f4f5.png"></a></div>
            </div>
            <div class="mui-slider-indicator"
            ><div class="mui-indicator"></div></div></div>
            <!--nav-->
            <div class="nav mui-clearfix">
                <ul><li><a href="/index.php/m/list/index.html">
                <img src="/static/wap/images/icon01.png"><p>全部商品</p></a></li>
                <li><a href="/index.php/m/list/index/cid/1.html">
                <img src="/static/wap/images/icon02.png"><p>时尚女装</p></a></li>
                <li><a href="/index.php/m/list/index/cid/2.html">
                <img src="/static/wap/images/icon03.png"><p>精品男装</p></a></li>
                <li><a href="/index.php/m/list/index/cid/3.html"><img src="/static/wap/images/icon04.png"><p>鞋包配饰</p></a></li>
                <li><a href="/index.php/m/list/index/cid/6.html"><img src="/static/wap/images/icon05.png"><p>美妆洗护</p></a></li>
                <li><a href="/index.php/m/list/index/cid/8.html"><img src="/static/wap/images/icon06.png"><p>美食小吃</p></a></li>
                <li><a href="/index.php/m/list/index/cid/9.html"><img src="/static/wap/images/icon07.png"><p>内衣裤袜</p></a></li>
                <li><a href="/index.php/m/list/index/cid/10.html"><img src="/static/wap/images/icon08.png"><p>数码家电</p></a></li>
                <li><a href="/index.php/m/list/index/cid/12.html"><img src="/static/wap/images/icon09.png"><p>家居用品</p></a></li>
                <li><a href="/index.php/m/article/index.html"><img src="/static/wap/images/icon10.png"><p>优惠头条</p></a></li>
                </ul></div>
                <!--toutiao-->
                <div class="toutiao mui-clearfix">
                    <a href="/index.php/m/article/index.html"><img src="/static/wap/images/toutiao.png" class="mui-pull-left"></a>
                    <div class="notice mui-pull-right">
                        <div class="notes mui-clearfix">
                            <div class="bd">
                                <ul>
                                    <li><a href="/index.php/m/article/read/id/2851.html">新版上线通知</a></li>
                                    <li><a href="/index.php/m/article/read/id/11.html">邀请好友,这些奖品任你挑选</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            <!--polular-->
            <div class="popular"><!--main-->
            <form action="/?g=m&m=jump" id="jump" method="post"><div style="clear: both;"></div>
            <div id="refreshContainer">
                <div class="goods-list am-cf list-wrapper" style="padding-top: 10px;">
                    <div class="mui-text-center">
                        <a href="/index.php/m/list/index/p/2.html" class="mui-btn">查看更多优惠商品</a>
                    </div>
                </div>
                    <input type="hidden" id="item" name="item" value="" />
                    <input type="hidden" id="quan" name="quan" value="" />
                    <input type="hidden" id="quanurl" name="quanurl" value="" />
            </div>
            </form>
        </div>
            <!--add-->
            </div>
            <a id="scrollToTop" class="backTop hide"><span class="iconfont icon-top"></span></a>
            </div>

        <!--footer-->
        <div class="footer">
            <div class="footer-top">
                <div class="line"></div><div class="circle"></div>
                <div class="line"></div>
            </div>
            <ul class="mui-clearfix">
                <li><a href="/m">
                <i class="iconfont icon-shouye"></i><span>首页</span></a></li>
                <li><a href="/index.php/m/jiu/index.html"><i class="iconfont icon-gift"></i><span>9块9包邮</span></a></li>
                <li><a href="/index.php/m/so/index.html"><i class="iconfont icon-sousuo"></i><span>超级搜券</span></a></li>
                <li><a href="/index.php/m/top100/index.html"><i class="iconfont icon-temai"></i><span>超级人气榜</span></a></li>
                <li><a href="/index.php/m/user/ucenter.html"><i class="iconfont icon-wode"></i><span>我的</span></a></li>
                </ul>
            </div>
            <div class="foot" style="display: none;"></div>
            <div class="mui-off-canvas-backdrop"></div></div>
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