<template>
  <div :class="classObj" class="app-wrapper">
    <div class="main-container">
      <el-row class="header">
        <el-menu :default-active="activeIndex"
                 theme="dark"
                 background-color="#2B6695"
                 text-color="#fff"
                 active-text-color="#ffd04b"
                 mode="horizontal"
                 @select="handleSelect">
          <el-menu-item index="1"> <router-link to="/">❤&nbsp首页</router-link></el-menu-item>

          <el-menu-item index="2"> <router-link to="/">消息中心</router-link></el-menu-item>
          <el-menu-item index="3"> <router-link to="/">帮助中心</router-link></el-menu-item>
        </el-menu>
      
      </el-row>

      <el-tabs type="border-card">
        <el-tab-pane label="通知">
          <div v-for="item in notifications" :key="item.id" class="text item">
            <router-link :to="'/article/'+item.id">
              {{ item.title }}
            </router-link>
          </div>
        </el-tab-pane>
        <el-tab-pane label="新闻">
          <div v-for="item in news" :key="item.id" class="text item">
            <router-link :to="'/article/'+item.id">
              {{ item.title }}
            </router-link>
          </div>
        </el-tab-pane>
        <el-tab-pane label="便民">
          <div v-for="item in bianmin" :key="item.id" class="text item">
            <router-link :to="'/article/'+item.id">
              {{ item.title }}
            </router-link>
          </div>
        </el-tab-pane>
        <el-tab-pane label="周边">
          <div v-for="item in zhoubian" :key="item.id" class="text item">
            <router-link :to="'/article/'+item.id">
              {{ item.title }}
            </router-link>
          </div>
        </el-tab-pane>
      </el-tabs>

      <app-main />

      <el-footer id="footer">
        <p id="footer_bottom">
          Copyright <a href="http://www.nanhuyijia.com">南湖逸家</a>   2019-2025|  蜀ICP备11019899号

          <a href="www.nanhuyijia.com">www.nanhuyijia.com</a>  <a target="_top" href="javascript:window.external.addFavorite('http://www.nanhuyijia.com','南湖逸家');">加入收藏</a>
          <a href='#' onClick="this.style.behavior='url(#default#homepage)';this.setHomePage('http://www.nanhuyijia.com');">设为首页</a>
        </p>

      </el-footer>

    </div>
    </div>
</template>

<script>
import { Navbar, Sidebar, AppMain, TagsView } from './components'
import ResizeMixin from './mixin/ResizeHandler'
import api from '@/api/api'
import baseapi from '@/api/baseapi'

export default {
  name: 'Layout',
  components: {
    Navbar,
    Sidebar,
    AppMain,
    TagsView
  },
  mixins: [ResizeMixin],
  data() {
    return {
      activeIndex: '1',
      notifications:[],
      news:[],
      zhoubian:[],
      bianmin:[]
    }
  },
  created() {
    this.getList()
  },
  computed: {
    sidebar() {
      return this.$store.state.app.sidebar
    },
    device() {
      return this.$store.state.app.device
    },
    classObj() {
      return {
        hideSidebar: !this.sidebar.opened,
        openSidebar: this.sidebar.opened,
        withoutAnimation: this.sidebar.withoutAnimation,
        mobile: this.device === 'mobile'
      }
    }
  },
  methods: {
  getList() {
      var queryNotification = {categoryId:202,pageIndex:1,pageSize:5,sortFields:'id',sort: 'desc'};
      var queryNews = {categoryId:201,pageIndex:1,pageSize:5,sortFields:'id',sort: 'desc'};
      var queryZhoubian = {categoryId:203,pageIndex:1,pageSize:5,sortFields:'id',sort: 'desc'};
      var queryBianmin = {categoryId:204,pageIndex:1,pageSize:5,sortFields:'id',sort: 'desc'};

      baseapi.get(api.nanhuarticleAPI, queryNotification).then(response => {
        this.notifications = response.data.result.results
      })

     baseapi.get(api.nanhuarticleAPI, queryNews).then(response => {
        this.news = response.data.result.results
      })

   baseapi.get(api.nanhuarticleAPI, queryZhoubian).then(response => {
        this.zhoubian = response.data.result.results
      })

   baseapi.get(api.nanhuarticleAPI, queryBianmin).then(response => {
        this.bianmin = response.data.result.results
      })
    },
    handleSelect(key) {
      this.activeIndex = key
    }
  }
}
</script>

<style scoped>

  #footer_bottom {
    text-align: center;
    margin-top: 15px;
    padding-bottom: 20px;
  }

</style>
