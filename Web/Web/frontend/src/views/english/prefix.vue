<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.word" placeholder="word" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter"/>
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">{{ $t('table.search') }}</el-button>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">{{ $t('table.add') }}</el-button>
    </div>

    <el-table
      v-loading="loading"
      :key="tableKey"
      :data="list"
      border
      fit
      highlight-current-row
      style="width: 100%;"
      @sort-change="sortChange">
      <el-table-column :label="$t('table.id')" prop="id" sortable="custom" align="center" width="65">
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column label="word" width="150px">
        <template slot-scope="scope">
            <span>{{ scope.row.word }}</span>
        </template>
      </el-table-column>
      <el-table-column label="source" width="500px" align="left">
        <template slot-scope="scope">
          <span>{{ scope.row.source }}</span>
        </template>
      </el-table-column>

      <el-table-column label="desc" align="center" width="150">
        <template slot-scope="scope">
          <span>{{ scope.row.desc }}</span>
        </template>
      </el-table-column>

      <el-table-column label="fullDesc" align="center" width="150">
        <template slot-scope="scope">
          <span>{{ scope.row.Json }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('table.actions')" align="center" width="230" class-name="small-padding fixed-width">
        <template slot-scope="scope">
          <el-button type="primary" size="mini" @click="handleUpdate(scope.row)">{{ $t('table.edit') }}</el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageIndex" :limit.sync="listQuery.pageSize" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="left" label-width="150px" style="width: 700px; margin-left:50px;">

        <el-form-item label="Word" prop="word">
          <el-input v-model="temp.word"/>
        </el-form-item>

        <el-form-item label="Desc">
          <el-input v-model="temp.desc"/>
        </el-form-item>

        <el-form-item label="Source">
          <el-input :autosize="{ minRows: 2, maxRows: 6}" v-model="temp.source" type="textarea" placeholder="Please input"/>
        </el-form-item>

        <el-form-item label="Json">
          <el-input :autosize="{ minRows: 2, maxRows: 4}" v-model="temp.json" type="textarea" placeholder="Please input"/>
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">{{ $t('table.cancel') }}</el-button>
        <el-button type="primary" @click="dialogStatus==='create'?createData():updateData()">{{ $t('table.confirm') }}</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import waves from '@/directive/waves' // Waves directive
import { parseTime } from '@/utils'
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
import api from '@/api/api'
import baseapi from '@/api/baseapi'

export default {
  name: 'ComplexTable',
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
      tableKey: 0,
      list: null,
      total: 0,
      loading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        sort: 'id'
      },
      sortOptions: [{ label: 'ID Ascending', key: 'id' }, { label: 'ID Descending', key: 'id' }],
      temp: {
        id: undefined,
        word: '',
        desc: '',
        source: '',
        json: '',
        status: 1,
        extensions: []
      },
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: 'Edit',
        create: 'Create'
      },
      rules: {
        word: [{ required: true, message: 'word is required', trigger: 'change' }],
        extention: [{ required: true, message: 'extention is required', trigger: 'change' }]
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.loading = true
      baseapi.get(api.prefixAPI,this.listQuery).then(response => {
        this.total = response.data.result.totalCount;
        this.list = response.data.result.results;
        this.loading = false;
      })
    },
    handleFilter() {
      this.listQuery.pageIndex = 1
      this.getList()
    },
    handleModifyStatus(row, status) {
      this.$message({
        message: '操作成功',
        type: 'success'
      })
      row.status = status
    },
    sortChange(data) {
      const { prop, order } = data
      if (prop === 'id') {
        this.sortByID(order)
      }
    },
    sortByID(order) {
      if (order === 'ascending') {
        this.listQuery.sort = '+id'
      } else {
        this.listQuery.sort = '-id'
      }
      this.handleFilter()
    },
    resetTemp() {
      this.temp = {
        id: undefined,
        word: '',
        desc: '',
        source: '',
        json: '',
        status: 1,
        extensions: []
      }
    },
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          this.loading = true
          baseapi.post(api.prefixAPI,this.temp).then(response => {
            if (response.data.id > 0) {
              this.list.unshift(this.temp)
              this.dialogFormVisible = false
              this.$notify({
                title: '成功',
                message: '创建成功',
                type: 'success',
                duration: 2000
              })
            }
            this.loading = false
          })
        }
      })
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          baseapi.post(api.prefixAPI,this.temp).then(response => {
            if (response.data.id > 0) {
              for (const v of this.list) {
                if (v.id === this.temp.id) {
                  const index = this.list.indexOf(v)
                  this.list.splice(index, 1, this.temp)
                  break
                }
              }

              this.dialogFormVisible = false
              this.$notify({
                title: '成功',
                message: '更新成功',
                type: 'success',
                duration: 2000
              })
            }
            this.loading = false
          })
        }
      })
    },
    handleDelete(row) {
      this.$notify({
        title: '成功',
        message: '删除成功',
        type: 'success',
        duration: 2000
      })
      const index = this.list.indexOf(row)
      this.list.splice(index, 1)
    }
  }
}
</script>
