<template>
  <div class="app-container">

    <el-table v-loading="loading" :data="list" border fit highlight-current-row style="width: 100%">

      <el-table-column align="center" label="ID" width="80">
        <template slot-scope="scope">
          <router-link :to="'/english/word/'+scope.row.id">
            <span>{{ scope.row.id }}</span>
          </router-link>
        </template>
      </el-table-column>

      <el-table-column width="120px" align="center" label="Word">
        <template slot-scope="scope">
          <span>{{ scope.row.word }}</span>
        </template>
      </el-table-column>

      <el-table-column width="130px" label="PhoneticSymbolUK">
        <template slot-scope="scope">
          <span>{{ scope.row.phoneticSymbolUK }}</span>
        </template>
      </el-table-column>
      <el-table-column width="150px" label="fullDesc">
        <template slot-scope="scope">
          <span>{{ scope.row.fullDesc }}</span>
        </template>
      </el-table-column>
      <el-table-column width="200px" label="etymaSource">
        <template slot-scope="scope">
          <span>{{ scope.row.etymaSource }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="150px" label="ZhDesc">
        <template slot-scope="scope">
          <template v-if="scope.row.edit">
            <el-input v-model="scope.row.zhDesc" class="edit-input" size="small"/>
            <el-button class="cancel-btn" size="small" icon="el-icon-refresh" type="warning" @click="cancelEdit(scope.row)">cancel</el-button>
          </template>
          <span v-else>{{ scope.row.zhDesc }}</span>
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
import etymaAPI from '@/api/etyma'

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
  data() {
    return {
      list: null,
      loading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 10
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.loading = true
      etymaAPI.get(this.listQuery).then(response => {
        const items = response.data.result.results
        this.list = items.map(v => {
          this.$set(v, 'edit', false) // https://vuejs.org/v2/guide/reactivity.html
          v.originalTitle = v.zhDesc //  will be used when user click the cancel botton
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
