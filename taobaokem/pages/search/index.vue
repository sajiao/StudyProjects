<template>
	<view>
		   <view class="s-page-wrapper">
		        <view class="search-pop">
		            <view class="main-title">
		                <view class="search-tab">
		                    <view class="my-sub-src"
		                          @tap="switchTab(0)"
		                          v-bind:class="[current == 0 ? 'cur' : '', '']">
		                        搜券
		                    </view>
		                    <view class="my-sub-src"
		                          @tap="switchTab(1)"
		                          v-bind:class="[current == 1 ? 'cur' : '', '']">
		                        搜全网
		                    </view>
		                    <view class="close-src" @tap="navigateBack">
		                        <text class="iconfont icon-zuojiantou"></text>
		                    </view>
		                    <view class="search">
		                        <input type="text"
		                               value=""
		                               :placeholder="placeholder"
		                               class="search_area"
		                               v-model="keywords" />
		                        <view class="search_submit" @tap="submitSearch">搜 索</view>
		                    </view>
		                </view>
		            </view>
		        </view>
		        <view class="search-cat"  v-if="couponlist.length == 0">
		            <swiper class="swiper" @change="swiperChange" :current="current">
		                <swiper-item>
		                    <view class="search-cat-tit"><text class="up-menu">热门搜索</text></view>
		                    <view class="src-content">
		                        <view class="main-src">
		                            <view class="src-item "
		                                  v-for="(key, index) in keywordlist"
		                                  :key="index"
		                                  @click="searchKey(key.value)">
		                                {{ key.value }}
		                            </view>
		                        </view>
		                    </view>
		                </swiper-item>
		                <swiper-item>
		                    <view class="search-default">
		                        <image src="../../static/img/goods/search-default.png"
		                               mode="widthFix"
		                               class="is-response"></image>
		                        <view class="help-tips has-mgtb-10 is-size-18">搜索方法：</view>
		                        <view class="help-tips color999">
		                            1、打开手机淘宝/天猫，长按拷贝商品标题
		                        </view>
		                        <view class="help-tips color999">2、将商品标题粘贴到搜索框中进行搜索</view>
		                        <view class="help-tips color999 has-mgt-10">"搜全网"功能中的商品信息均来自于互联网</view>
		                        <view class="help-tips color999">商品准确信息请与商品所属店铺经营者沟通确认</view>
		                    </view>
		                </swiper-item>
		            </swiper>
		        </view>
		
		    </view>	
	<view class="index-content" style="margin-top:120px">
		<!-- 领券直播 -->
		<view class="index-coupon has-bg-white has-pd-10">
			<view class="goods-list" v-if="couponlist.length > 0">
				<view class="coupon-page s-row" v-for="(item,index) in couponlist" :key="index" @tap="toGoodsInfo(item.Id)">
					<view class="image">
						<image :src="item.PicUrl"></image>
					</view>
					<view class="content">
						<view class="title">{{item.Title}}</view>
						<view class="num s-row">
							<text class="tmprice"> 原价 ¥{{item.Price}}</text>
							<text class="volume">已售{{item.Volume}}件</text>
						</view>
						<view class="money s-row">
							<text class="coupon-price">券后价<text>¥{{item.FinalPrice}}</text></text>
							<text class="quan">
								<i></i>{{item.YouhuiPrice}}元券<i></i>
							</text>
						</view>
					</view>
				</view>
			</view>
		</view>
		<!-- 领券直播 -->
		
		<!-- 加载更多提示 -->
		<view class="s-col is-col-24" v-if="couponlist.length > 0">
			<load-more :loadingType="loadingType" :contentText="contentText"></load-more>
		</view>
		<!-- 加载更多提示 -->
		
	</view>
</view>
</template>

<script>
	export default {
		data() {
			return {
				placeholder: '开学比屯速食',
			    current: 0,
				keywords: '',
				keywordlist: [
				    { value: '抽纸' },
				    { value: '洗面奶' },
				    { value: '洗衣液' },
				    { value: '卫生巾' },
				    { value: '短袜' },
				    { value: '垃圾袋' },
				    { value: '行李箱' },
				    { value: '洗发水' }
				],
				couponlist: [],
				 //分页
				totalCount: 0,
				currentPage: 1,
				pageSize: 10,
				totalPage: 0,
				loadingType: 0,
				contentText: {
					contentdown: "上拉显示更多",
					contentrefresh: "正在加载...",
					contentnomore: "没有更多数据了"
				}
			}
		},
		methods: {
			searchKey: function (key) {
			    this.keywords = key;
			    this.loadCouponList(0);
			},
			loadCouponList: function(type) {
				this.loadingType = 1; 
				 let param = {
				    pageIndex: this.currentPage,
				    pageSize: this.pageSize,
				};
				this.$Request.getList(this.$api.common.item, param).then(res => {
					this.loadingType = 0; 
					if (res && res.Code == 0) {
						if (res.Result) {
							this.couponlist = res.Result.Results;
							this.totalCount = Math.ceil(res.Result.TotalCount);
							this.totalPage = Math.ceil(this.totalCount / this.pageSize);                            
						} else {
							this.couponlist = [];
						}
					}
					else{
						this.loadingType = 2; 
					}
					if (type == "Refresh") {
						uni.stopPullDownRefresh(); // 停止刷新
					}
					
				}).catch(err=>{
// 					this.loadingType = 2; 
// 					this.contentText.contentnomore = "网络繁忙，请稍后再试"
				})
			},
			
			toGoodsInfo: function(itemid) {
				uni.navigateTo({
					url:"/pages/detail/goodsinfo?itemid="+itemid,
				})
			},
			navigateBack: function () {
			        uni.navigateBack();
			},
			submitSearch: function () {
				this.loadCouponList(0);
			},
			swiperChange: function (e) {
				var current = e.detail.current;
				this.current = current;
			},
			switchTab: function (current) {
				this.current = current;
				this.keywords ="";
				this.couponlist =[];
				uni.stopPullDownRefresh(); // 停止刷新
			},

		},
		onReachBottom: function() {
			this.currentPage = this.currentPage + 1;
			this.loadCouponList();
		},
		onPullDownRefresh: function() {
			this.currentPage = 1;
			this.loadCouponList("Refresh");
		}
	}
</script>

<style>
	@import "../../static/css/index.css";
	
	 .swiper {
	    height: 100%;
	}
	
	.help-tips {
	    font-size: 13px;
	    color: #999;
	    line-height: 26px;
	    padding: 0 0 0 30px;
	    margin: 0;
	    width: 80%;
	    text-align: left;
	}
	
	    .help-tips.color999 {
	        color: #999999;
	    }
	
	.search-default {
	    text-align: center;
	    height: 200px;
	    margin: auto;
	}
	
	    .search-default image {
	        display: block;
	        margin: auto;
	        width: 80%;
	    }
	
	.search-cat {
	    position: fixed;
	    top: 0;
	    bottom: 0;
	    padding-top: 130px;
	    width: 100%;
	    height: 100%;
	    padding-bottom: 11px;
	    background-color: #fff;
	}
	
	    .search-cat .search-cat-tit {
	        position: relative;
	        width: 150px;
	        height: 10px;
	        margin: 13px 3% 20px;
	    }
	
	        .search-cat .search-cat-tit .up-menu {
	            display: block;
	            height: 20px;
	            line-height: 20px;
	            font-size: 0.9em;
	            color: #999;
	        }
	
	.src-content {
	    margin: 20px 0 30px;
	    width: 97%;
	}
	
	.main-src .src-item {
	    float: left;
	    border-radius: 22px;
	    padding: 0 10px;
	    height: 23px;
	    line-height: 23px;
	    background-color: #f6f6f6;
	    margin: 10px;
	    position: relative;
	    font-size: 13px;
	    color: #333;
	}
	
	.main-title {
	    height: 130px;
	}
	
	.main-title {
	    background: -moz-linear-gradient(left, #fa4dbe 0, #fbaa58 100%);
	    background: -webkit-gradient( linear, left top, left right, color-stop(0, #fa4dbe), color-stop(100%, #fbaa58) );
	    background: -webkit-linear-gradient(left, #fa4dbe 0, #fbaa58 100%);
	    background: -o-linear-gradient(left, #fa4dbe 0, #fbaa58 100%);
	    background: -ms-linear-gradient(left, #fa4dbe 0, #fbaa58 100%);
	    background: linear-gradient(to left, #fa4dbe 0, #fbaa58 100%);
	    border-bottom-color: transparent;
	    padding: 8px 10px;
	    position: fixed;
	    top: 0;
	    left: 0;
	    width: 100%;
	    z-index: 120;
	    display: block;
	    box-sizing: border-box;
	}
	
	.search-pop .search-tab {
	    margin: 5px 0 10px;
	    color: #fff;
	    font-size: 15px;
	    text-align: center;
	    /* #ifdef APP-PLUS */
	    padding-top: var(--status-bar-height);
	    /* #endif */
	}
	
	    .search-pop .search-tab .my-sub-src {
	        margin: 0 auto 0 20px;
	        display: inline-block;
	        color: #fff;
	        line-height: 30px;
	        margin-bottom: 10px !important;
	    }
	
	        .search-pop .search-tab .my-sub-src:nth-child(1) {
	            margin-left: 0px !important;
	        }
	
	.main-title .search-tab .cur {
	    opacity: 1;
	    border-bottom: 1px solid #fff;
	}
	
	.main-title .search-tab .close-src {
	    position: absolute;
	    left: 20px;
	    display: block;
	    float: left;
	    color: #ffffff;
	    margin-top: 15px;
	}
	
	    .main-title .search-tab .close-src .iconfont {
	        font-size: 20px;
	    }
	
	.main-title .search {
	    background-color: #fff;
	    border-radius: 20px;
	    width: 76%;
	    margin-left: 12%;
	    position: relative;
	    padding: 0 9px;
	    height: 32px;
	    line-height: 32px;
	    font-size: 13px;
	    margin-top: 10px;
	}
	
	.search_submit {
	    width: 25%;
	    height: 32px;
	    background: #ffb925;
	    color: #fff;
	    float: right;
	    margin-top: -32px;
	    position: relative;
	    border-radius: 15px;
	    margin-right: -20px;
	    z-index: 30;
	}
	
	.main-title .search input {
	    border: none;
	    outline: 0;
	    height: 32px;
	    font-size: 13px;
	    line-height: 30px;
	    background: #fff;
	    color: #666;
	    border-radius: 5px;
	    padding: 0 0 0 5px;
	    text-align: left;
	}
	
.main-title .search input.search_area {
	background-color: transparent;
	position: relative;
	z-index: 99;
	width: 80%;
	color: #333;
	font-size: 12px;
}
</style>
