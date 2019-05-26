<template>

   <div class="main">

<!--banner-->
    <div class="banner-wrap">
        <div class="container cl">
            <div class="banner-l">
                <h3>[今日推荐]</h3>
                <div class="recom">
                    <div class="pbd"><ul></ul></div>
                    <div class="phd"><ul></ul></div>
                </div>
            </div>
            <div class="banner cl">
                <div class="bd">
                <ul><li _src="#" style="width: 100%;">
                <a href="#" target="_blank">
                <img src="/static/img/5971438c5f4f5.png" width="760" height="338"></a></li></ul>
                </div>
                <div class="hd"><ul></ul></div>
            </div>
            <div class="banner-r">
                    <div class="login">
                        <img src="/static/pc/images/default.png" class="round"><p>提交订单换积分</p>
                        <a href="/index.php/login/index.html" class="btn btn-pink">立即登录</a>
                        </div>
                    <div class="jifen flexbox">
                            <div class="txt"><span class="title">直播数量</span>
                        <span>当前优惠<em>0</em>款</span></div>
                        </div>
            </div>
        </div>
     </div>
  
  <!--service-->
    <div class="service">
        <div class="container cl">
            <ul class="cl">
                <li><p class="f-l">券</p><div class="f-l"><h3 class="c-main">先领券再下单</h3><h4>享现金立减优惠</h4></div></li>
                <li><p class="f-l"><i class="iconfont icon-filter"></i></p><div class="f-l"><h3 class="c-main">人工精选</h3><h4>优质、优惠商品</h4></div></li>
                <li><p class="f-l"><i class="iconfont icon-fuwuwuyou"></i></p><div class="f-l"><h3 class="c-main">安全无忧</h3><h4>所有交易在淘宝完成安全放心</h4></div></li>
                <li><p class="f-l">24</p><div class="f-l"><h3 class="c-main">每日上新</h3><h4>24小时不间断上新</h4></div></li>
                <li><p class="f-l"><i class="iconfont icon-meiyuan"></i></p><div class="f-l"><h3 class="c-main">积分兑好礼</h3><h4>提交订单就能赚积分</h4></div></li>
            </ul>
        </div>
    </div>

     <div class="container cl">
        <div class="indexblock" style="margin-top: 0px;">
                <div class="hottitle">
                    <p><i class="iconfont icon-fire"></i>特卖精选</p>
                </div>
                <div class="temai cl">
                    <ul>
                        <li v-for="item in temaiData" :key="item.Id">
                            <div class="item">
                                <a target="_blank" :href="item.ClickUrl" class="link">
                                    <img :src="item.PicUrl">
                                </a>
                                <a  target="_blank" :href="item.ProductUrl" :title="item.Title" class="link"> <p class="text-overflow">{{item.Title}}</p></a>
                                <div class="price">
                                    <p class="c-main">￥<span>{{item.FinalPrice}}</span>(券后价)</p><p><del>￥{{item.Price}}</del></p>
                                </div>
                                <div class="sales cl">
                                    <img src="/static/pc/images/taobao.png"> <p>月销量：<span class="c-primary">{{item.Volume}}</span>件</p>
                                </div>
                                <a target="_blank" :href="item.ClickUrl" class="coupon">
                                    <h5>优惠券：<span>{{item.Commission}}</span>元</h5>
                                    <p>立即<br>领券</p>
                                </a>
                            </div>
                        </li>
                    </ul>
                </div>
        </div>
    </div>

        
      <div class="container cl">
        <div class="indexblock">
            <div class="hottitle">
                <p><i class="iconfont icon-gift"></i>超级人气榜</p>
            </div>
            <div class="hot cl">
                <ul>
                    <li  v-for="item in renqiData" :key="item.Id">
                        <div class="item cl">
                            <div class="pic f-l mr-10">
                                <a target="_blank" :href="item.ClickUrl">
                                    <img :src="item.PicUrl"></a>
                            </div>
                            <div class="txt f-l">
                                <div class="tit">
                                    <img src="/static/pc/images/tmall.png"><a href="/item/id/986908.html">夏季冰爽袖套防晒女护臂手臂套袖男袖子开车防紫外线冰丝手套长款</a>
                                </div>
                                <div class="price cl">
                                    <p class="f-l c-main">￥<span>{{item.FinalPrice}}</span><del>￥{{item.Price}}</del></p>
                                    <p class="f-r">省<span class="c-main">{{item.YouhuiPrice}}</span>元</p>
                                </div>
                                <a target="_blank"  :href="item.ClickUrl" class="sales">月销量{{item.Volume}}件</a>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>


    <div class="wrap container cl" v-loading="loading">
        <div class="new">
            <div class="goods-list2 cl">
                <ul>
                    <li  v-for="item in tableData" :key="item.NumIid">
                        <a :href="item.ProductUrl"   target="_blank" class="img cnzzCounter">
                            <img  v-bind:src="item.PicUrl" class="lazy" width="270" height="270" />
                            <div class="lq" @click="lingquan(item.ClickUrl)">
                                <div class="lq-t">
                                    <p class="lq-t-d1">领优惠券</p>
                                    <p class="lq-t-d2">省<span> {{item.YouhuiPrice}}</span>元</p>
                                </div>
                                <div class="lq-b"></div>
                            </div>
                            <div class="padding">
                                <p class="title cl ellipsis-2 cnzzCounter">
                                    {{item.Title}}
                                    </p>
                                <div class="coupon-wrap cl">
                                    <p class="price">
                                        <span class="f-28 c-main">{{item.FinalPrice}}<i class="quanhou"></i></span>
                                        <del class="c-999">
                                            淘宝 :￥{{item.Price}}
                                        </del>
                                    </p>
                                    <div class="num">
                                        <p>月销量 <span class="c-main">{{item.Volume}}</span></p>
                                        <span class="lingqu">
                                            预计返￥
                                            {{item.Commission}}
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </li>
                </ul>
            </div>
            
        </div>
    </div>

    <div class="container cl">
        <div class="indexblock">
            <div class="hottitle">
                <p><i class="iconfont icon-fire"></i>头条资讯</p>
            </div>
            <div class="topic cl">
                <ul>
                    <li>
                        <div class="item cl">
                            <div class="pic f-l">
                                <a target="_blank" href="/article/view_2870"> <img src="/data/upload/avatar/201903/5c9db0404f7f7.jpg"></a>
                            </div>
                            <div class="txt f-l">
                                <a target="_blank" href="/article/view_2870" class="tit">春夏焕新，跟着小编买，女士时尚单品推荐</a>
                                <p class="details">
                                    褪去了冬天的寒冷，随着路边树梢小嫩芽的冒头，粉白色花花的争相盛开，外面已是满满的春天气息。懒惰了一冬天的人们也开始勤快起来，换上了轻薄的春装，开始了更多的户外活
                                </p>
                                <p>
                                    <span class="f-l">2019-03-29</span>
                                </p>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item cl">
                            <div class="pic f-l">
                                <a target="_blank" href="/article/view_2862"> <img src="http://image.uc.cn/o/wemedia/s/upload/2017/e13e3983062fe5bf62c64c40f99455b8x640x582x60.jpeg;,3,jpegx;3,700x.jpg"></a>
                            </div>
                            <div class="txt f-l">
                                <a target="_blank" href="/article/view_2862" class="tit">传统棉被早就out了, 现流行法兰绒四件套, 单身狗也有暖被窝</a>
                                <p class="details">
                                    对很多人来说，冬天里最舒服的事情，一定就是躲在温暖的被窝里面，玩着手机安然入睡了。但是在天气寒冷的情况下，很多体质弱的MM总是会发现，被窝总是冰冷冷的，怎么捂都
                                </p>
                                <p>
                                    <span class="f-l">2017-10-13</span>
                                </p>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item cl">
                            <div class="pic f-l">
                                <a target="_blank" href="/article/view_2861"> <img src="http://image.uc.cn/o/wemedia/s/2017/da4d09f4cb17a5e0d605ca1144997b46x640x640x47.jpeg;,3,jpegx;3,700x.jpg" heavypress="http://image.uc.cn/o/wemedia/s/2017/da4d09f4cb17a5e0d605ca1144997b46x640x640x47.jpeg;,3,jpegx;3,700x.jpg"></a>
                            </div>
                            <div class="txt f-l">
                                <a target="_blank" href="/article/view_2861" class="tit">几种很好养的懒人植物, 寓意还特别好</a>
                                <p class="details">
                                    很多朋友，都喜欢在家里养养花草，让家里绿意嫣然，显得生机勃勃，不仅赏心悦目，对净化空气也很有一套。但是身为一个上班族，相信很多朋友都像小编一样，没有太多的时间去
                                </p>
                                <p>
                                    <span class="f-l">2017-10-13</span>
                                </p>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item cl">
                            <div class="pic f-l">
                                <a target="_blank" href="/article/view_2860"> <img src="http://image.uc.cn/o/wemedia/s/2017/6056803bd184027a93d273426ef45564x640x442x56.jpeg;,3,jpegx;3,700x.jpg" heavypress="http://image.uc.cn/o/wemedia/s/2017/6056803bd184027a93d273426ef45564x640x442x56.jpeg;,3,jpegx;3,700x.jpg"></a>
                            </div>
                            <div class="txt f-l">
                                <a target="_blank" href="/article/view_2860" class="tit">妈妈说这三种食物绝不能放冰箱, 扔了都比存着好</a>
                                <p class="details">
                                    周末回娘家看望妈妈，发现她正在收拾食材，一大堆的食材往冰箱放，却唯独把一部分留出来。我很是惊讶，觉得这样很浪费冰箱空间，妈妈却不以为然，她笑着解释说，这些食物每
                                </p>
                                <p>
                                    <span class="f-l">2017-10-13</span>
                                </p>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>

  </div>
</template>

<script>
    import itemsService from '@/server/itemsService'
    import session from '@/store/storage'
    import api from '@/server/api'

    export default {
        name: 'basetable',
        data() {
            return {
                queryKeyword:'',
                tableData: [],
                temaiData: [],
                renqiData: [],
                //分页
                totalCount: 0,
                currentPage: 1,
                pageSize: 16,
                totalPage: 0,
                loading: true,
            }
        },
        created() {
            this.getData();
            this.getTemai();
            this.getRenqi();
        },
        computed: {
           
        },
        methods: {
            lingquan(url)
            {
                 window.open(url,"_blank");   
            }
            ,
            getData() {
                this.loading = true;
                let param = {
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    title: this.queryKeyword
                };
                itemsService.get(param).then((res) => {
                    if (res && res.statusText == "OK") {
                        this.fullScreenLoading = false;
                        if (res.data && res.data.Result) {
                            this.tableData = res.data.Result.Results;
                            this.totalCount = Math.ceil(res.data.Result.TotalCount);
                            this.totalPage = Math.ceil(this.totalCount / this.pageSize);                            
                        } else {
                            this.tableData = [];
                        }
                    }
                    this.loading = false;
                })
            },
            getTemai() {
                let param = {
                    pageIndex: 1,
                    pageSize: 5,
                    title: "",
                    typeId:1,
                };
                itemsService.get(param).then((res) => {
                    if (res && res.statusText == "OK") {
                        if (res.data && res.data.Result) {
                            this.temaiData = res.data.Result.Results;                     
                        } else {
                            this.temaiData = [];
                        }
                    }
                })
            },
            getRenqi() {
                let param = {
                    pageIndex: 1,
                    pageSize: 6,
                    title: "",
                    typeId:1,
                };
                itemsService.get(param).then((res) => {
                    if (res && res.statusText == "OK") {
                        if (res.data && res.data.Result) {
                            this.renqiData = res.data.Result.Results;                     
                        } else {
                            this.renqiData = [];
                        }
                    }
                })
            },
        }
    }

</script>

