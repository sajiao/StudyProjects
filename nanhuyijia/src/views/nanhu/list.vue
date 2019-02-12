<template>
  <el-container>
    <el-aside v-if="!isMobile" width="200px"/>
    <el-main>
      <div class="app-container">

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

        <el-table v-loading="loading" :data="list" highlight-current-row style="width: 100%">

          <el-table-column align="left" label="标题">
            <template slot-scope="scope">
              <router-link :to="'/nanhu/detail/'+scope.row.id" class="link-type">
                <span v-if="scope.row.IsStick">置顶</span> <span>{{ scope.row.title }}</span>
              </router-link>
            </template>
          </el-table-column>

          <el-table-column v-if="!isMobile" width="100px" align="center" label="作者">
            <template slot-scope="scope" />
          </el-table-column>
          <el-table-column v-if="!isMobile" width="100px" align="center" label="阅读量">
            <template slot-scope="scope">
              <span>{{ scope.row.ReadingCount }}</span>
            </template>
          </el-table-column>

          <el-table-column v-if="!isMobile" width="100px" align="center" label="评论数">
            <template slot-scope="scope">
              <span>{{ scope.row.CommentCount }}</span>
            </template>
          </el-table-column>

          <el-table-column v-if="!isMobile" width="180px" align="center" label="发布日期">
            <template slot-scope="scope">
              <span>{{ scope.row.showStartTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
            </template>
          </el-table-column>

        </el-table>

        <pagination v-show="total>0" :total="total" :page-index.sync="listQuery.pageIndex" :page-size.sync="listQuery.pageSize" @pagination="getList" />

      </div>
    </el-main>
    <el-aside v-if="!isMobile" width="200px"/>
  </el-container>

</template>

<script>
import waves from '@/directive/waves' // Waves directive
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
import api from '@/api/api'
import baseapi from '@/api/baseapi'

export default {
  name: 'ArticleList',
  components: { Pagination },
  directives: { waves },
  filters: {
    statusFilter(status) {
      const statusMap = {
        published: 'success',
        draft: 'info',
        deleted: 'danger'
      }
      return statusMap[status]
    }
  },
  data() {
    return {
      list: null,
      isMobile: this.$store.state.app.device === 'mobile',
      total: 0,
      loading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        title: '',
        sort: 'id'
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.loading = true
      baseapi.get(api.nanhuarticleAPI, this.listQuery).then(response => {
        this.total = response.data.result.totalCount
        this.list = response.data.result.results
        this.loading = false
      })
    },
    handleFilter() {
      this.listQuery.pageIndex = 1
      this.getList()
    },
    handleSizeChange(val) {
      this.listQuery.pageSize = val
      this.getList()
    },
    handleCurrentChange(val) {
      this.listQuery.page = val
      this.getList()
    }
  }
}
</script>

<style scoped>
.edit-input {
  padding-right: 100px;
}
.cancel-btn {
  position: absolute;
  right: 15px;
  top: 10px;
}
</style>
