<template>
   <div class="main" v-loading="loading">
    <div class="wrap container cl">
     <div class="new">
            <div class="goods-list2 cl">
                <ul>
                    <li  v-for="item in tableData" :key="item.NumIid">
                        <a :href="item.ClickUrl"   target="_blank" class="img cnzzCounter">
                            <img  v-bind:src="item.PicUrl" class="lazy" width="270" height="270" />
                            <div class="lq">
                                <div class="lq-t">
                                    <p class="lq-t-d1">领优惠券</p>
                                    <p class="lq-t-d2">省<span> {{item.Commission}}</span>元</p>
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
                                            淘宝 :￥{{item.FinalPrice}}
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
      <div class="pagination container">
                <el-pagination @size-change="handleSizeChange"
                               @current-change="handleCurrentChange"
                               :current-page.sync="currentPage"
                               :page-size="pageSize" background
                               :page-sizes="[16, 32, 64, 80, 100]"
                               layout="total, sizes, prev, pager, next"
                               :total="totalCount">
                </el-pagination>
     </div>
         
  </div>
</template>

<script>
    import itemsService from '@/server/itemsService'
    import session from '@/store/storage'
    import api from '@/server/api'
    import eventBus from '../common/bus';

    export default {
        name: 'basetable',
        data() {
            return {
                queryKeyword:'',
                tableData: [],
                //分页
                totalCount: 0,
                currentPage: 1,
                pageSize: 32,
                totalPage: 0,
                loading: true,
            }
        },
        created() {
       
            debugger;
            if(this.$vm.$route.params != null && this.$vm.$route.params != "")
            {
                   this.queryKeyword = this.$vm.$route.params.keyword;
            }
            
            this.getData();
        },
        computed: {
            data() {
                return this.tableData;
            }
        },
        methods: {
            // 分页导航
            handleCurrentChange(val) {
                this.currentPage = val;
                this.getData();
            },
            handleSizeChange(v) {
                this.currentPage = 1;
                this.pageSize = v;
                this.getData();
            },
            search() {
                this.fullScreenLoading = true;
                this.currentPage = 1;
                this.getData();
            },
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
        }
    }

</script>
