<template>
  <div class="app-container">

    <el-table v-loading="loading" :data="list" border fit highlight-current-row style="width: 100%">

      <el-table-column align="center" label="ID" width="80">
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column align="center" label="etymaWord" width="120">
        <template slot-scope="scope">
          <span>{{ scope.row.etymaWord }}</span>
        </template>
      </el-table-column>
      <el-table-column width="120px" align="center" label="Word">
        <template slot-scope="scope">
          <span>{{ scope.row.word }}</span>
        </template>
      </el-table-column>

      <el-table-column width="160px" label="PhoneticSymbolUK">
        <template slot-scope="scope">
          <span>{{ scope.row.phoneticSymbolUK }}</span>
        </template>
      </el-table-column>
      <el-table-column width="200px" label="Desc">
        <template slot-scope="scope">
          <span>{{ scope.row.desc }}</span>
        </template>
      </el-table-column>
      <el-table-column width="200px" label="FullDesc">
        <template slot-scope="scope">
          <span>{{ scope.row.fullDesc }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" label="ZhDesc">
        <template slot-scope="scope">
          <span>{{ scope.row.zhDesc }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" label="Frequency">
        <template slot-scope="scope">
          <span>{{ scope.row.frequency }}</span>
        </template>
      </el-table-column>

      <el-table-column align="center" label="Actions" width="120">
        <template slot-scope="scope">
          <el-button v-if="scope.row.edit" type="success" size="small" icon="el-icon-circle-check-outline" @click="confirmEdit(scope.row)">Ok</el-button>
          <el-button v-else type="primary" size="small" icon="el-icon-edit" @click="scope.row.edit=!scope.row.edit">Edit</el-button>
        </template>
      </el-table-column>

    </el-table>
  </div>
</template>

<script>
import wordAPI from '@/api/word'

const defaultForm = {
  status: 'draft',
  title: '', // 文章题目
  content: '', // 文章内容
  content_short: '', // 文章摘要
  source_uri: '', // 文章外链
  image_uri: '', // 文章图片
  display_time: undefined, // 前台展示时间
  id: undefined,
  platforms: ['a-platform'],
  comment_disabled: false,
  importance: 0
}

export default {
  name: 'InlineEditTable',
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
  props: {
    isEdit: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      list: null,
      loading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 10
      },
      postForm: Object.assign({}, defaultForm),
      tempRoute: {}
    }
  },
  created() {
    const id = this.$route.params && this.$route.params.id
    this.getList(id)

    // Why need to make a copy of this.$route here?
    // Because if you enter this page and quickly switch tag, may be in the execution of the setTagsViewTitle function, this.$route is no longer pointing to the current page
    // https://github.com/PanJiaChen/vue-element-admin/issues/1221
    this.tempRoute = Object.assign({}, this.$route)
  },
  methods: {
    getList(id) {
      this.loading = true
      this.listQuery.etymaId = id
      wordAPI.get(this.listQuery).then(response => {
        const items = response.data.result
        this.list = items.map(v => {
          this.$set(v, 'edit', false) // https://vuejs.org/v2/guide/reactivity.html

          return v
        })
        this.loading = false
      })
    },
    cancelEdit(row) {
      row.zhDesc = row.zhDesc
      row.edit = false
      this.$message({
        message: 'The title has been restored to the original value',
        type: 'warning'
      })
    },
    confirmEdit(row) {
      row.edit = false
      row.originalTitle = row.zhDesc
      this.$message({
        message: 'The title has been edited',
        type: 'success'
      })
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
