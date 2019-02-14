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
          <el-menu-item index="1"> <router-link to="/nanhu">❤&nbsp首页</router-link></el-menu-item>

          <el-menu-item index="2"> <router-link to="/guide">消息中心</router-link></el-menu-item>
          <el-menu-item index="3"> <router-link to="/dashboard">帮助中心</router-link></el-menu-item>
        </el-menu>
        <div class="line" />
      </el-row>

      <el-tabs type="border-card">
        <el-tab-pane label="用户管理">
          <div v-for="o in 4" :key="o" class="text item">
            <router-link :to="'/nanhu/edit/'">
              {{ '列表内容 ' + o }}
            </router-link>
          </div>
        </el-tab-pane>
        <el-tab-pane label="配置管理">配置管理</el-tab-pane>
        <el-tab-pane label="角色管理">角色管理</el-tab-pane>
        <el-tab-pane label="定时任务补偿">定时任务补偿</el-tab-pane>
      </el-tabs>

      <app-main />

      <el-footer id="footer">
        <p class="footer">
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
      activeIndex: '1'
    }
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
    handleSelect(key) {
      this.activeIndex = key
    }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
  @import "~@/styles/mixin.scss";
  .app-wrapper {
    @include clearfix;
    position: relative;
    height: 100%;
    width: 100%;
    &.mobile.openSidebar{
      position: fixed;
      top: 0;
    }
  }
  .drawer-bg {
    background: #000;
    opacity: 0.3;
    width: 100%;
    top: 0;
    height: 100%;
    position: absolute;
    z-index: 999;
  }


</style>
