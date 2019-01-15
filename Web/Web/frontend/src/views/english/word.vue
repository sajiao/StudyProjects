<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.word" placeholder="word" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter"/>
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">{{ $t('table.search') }}</el-button>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">{{ $t('table.add') }}</el-button>
      <el-button v-waves :loading="downloadLoading" class="filter-item" type="primary" icon="el-icon-download" @click="handleDownload">{{ $t('table.export') }}</el-button>
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
          <a href="#" @click="handleUpdate(scope.row)">{{ scope.row.word }}</a>
        </template>
      </el-table-column>

      <el-table-column label="SplitWord" width="150px">
        <template slot-scope="scope">
          <span>{{ scope.row.splitWordDesc }}</span>
        </template>
      </el-table-column>

      <el-table-column label="EtymaWord" width="150px">
        <template slot-scope="scope">
          <span>{{ scope.row.etymaWord }}</span>
        </template>
      </el-table-column>

      <el-table-column label="phoneticSymbolUS" width="160px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.phoneticSymbolUS }}</span>
        </template>
      </el-table-column>

      <el-table-column label="phoneticSymbolUK" width="160px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.phoneticSymbolUK }}</span>
        </template>
      </el-table-column>

      <el-table-column label="desc" align="center" width="150">
        <template slot-scope="scope">
          <span>{{ scope.row.desc }}</span>
        </template>
      </el-table-column>

      <el-table-column label="zhDesc" align="center" width="150">
        <template slot-scope="scope">
          <span>{{ scope.row.zhDesc }}</span>
        </template>
      </el-table-column>

      <el-table-column label="frequency" align="left" min-width="60">
        <template slot-scope="scope">
          <span>{{ scope.row.frequency }}</span>
        </template>
      </el-table-column>

      <el-table-column label="frequency2" align="left" min-width="60">
        <template slot-scope="scope">
          <span>{{ scope.row.frequency2 }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Examination" align="left" min-width="60">
        <template slot-scope="scope">
          <span>{{ scope.row.examination }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('table.actions')" align="center" width="230" class-name="small-padding fixed-width">
        <template slot-scope="scope">
          <el-button type="primary" size="mini" @click="handleUpdate(scope.row)">{{ $t('table.edit') }}</el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="left" label-width="150px" style="width: 700px; margin-left:50px;">

        <el-form-item label="Word" prop="word">
          <el-input v-model="temp.word" />
        </el-form-item>

        <el-form-item label="SplitWord">
          <el-input v-model="temp.splitWord" />
        </el-form-item>

        <el-form-item label="SplitWordDesc">
          <el-input :autosize="{ minRows: 2, maxRows: 4}" v-model="temp.splitWordDesc" type="textarea" placeholder="Please input" />
        </el-form-item>

        <el-form-item label="EtymaId" prop="etymaId">
          <el-input v-model="temp.etymaId" />
        </el-form-item>

        <el-form-item label="Level">
          <el-input v-model="temp.level" />
        </el-form-item>

        <el-form-item label="PhoneticSymbolUK" prop="PhoneticSymbolUK">
          <el-input v-model="temp.phoneticSymbolUK" />
        </el-form-item>
        <el-form-item label="PhoneticSymbolUS" prop="phoneticSymbolUS">
          <el-input v-model="temp.phoneticSymbolUS" />
        </el-form-item>
        <el-form-item label="Desc">
          <el-input :autosize="{ minRows: 2, maxRows: 4}" v-model="temp.desc" type="textarea" placeholder="Please input" />
        </el-form-item>

        <el-form-item label="ZhDesc">
          <el-input :autosize="{ minRows: 2, maxRows: 4}" v-model="temp.zhDesc" type="textarea" placeholder="Please input" />
        </el-form-item>

        <el-form-item label="FullDesc">
          <el-input :autosize="{ minRows: 2, maxRows: 6}" v-model="temp.fullDesc" type="textarea" placeholder="Please input" />
        </el-form-item>

        <el-form-item label="Frequency">
          <el-input v-model="temp.frequency" />
        </el-form-item>
        <el-form-item label="Frequency2">
          <el-input v-model="temp.frequency2" />
        </el-form-item>
        <el-form-item label="Examination">
          <el-input v-model="temp.examination" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">{{ $t('table.cancel') }}</el-button>
        <el-button type="primary" @click="dialogStatus==='create'?createData():updateData()">{{ $t('table.confirm') }}</el-button>
      </div>
    </el-dialog>

    <el-dialog :visible.sync="dialogPvVisible" title="Reading statistics">
      <el-table :data="pvData" border fit highlight-current-row style="width: 100%">
        <el-table-column prop="key" label="Channel"/>
        <el-table-column prop="pv" label="Pv"/>
      </el-table>
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="dialogPvVisible = false">{{ $t('table.confirm') }}</el-button>
      </span>
    </el-dialog>

  </div>
</template>

<script>
import waves from '@/directive/waves' // Waves directive
import { parseTime } from '@/utils'
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
import wordAPI from '@/api/word'

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
    },
    typeFilter(type) {
      return calendarTypeKeyValue[type]
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
      importanceOptions: [1, 2, 3],
      sortOptions: [{ label: 'ID Ascending', key: '+id' }, { label: 'ID Descending', key: '-id' }],
      showReviewer: false,
      temp: {
        id: undefined,
        word: '',
        etymaId: 0,
        desc: '',
        extention: '',
        phoneticSymbolUK: '',
        phoneticSymbolUS: '',
        fullDesc: '',
        zhDesc: '',
        frequency: 0,
        frequency2: '',
        examination: '',
        splitWord: '',
        splitWordDesc: '',
        level: '',
        status: 1
      },
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: 'Edit',
        create: 'Create'
      },
      dialogPvVisible: false,
      pvData: [],
      rules: {
        word: [{ required: true, message: 'word is required', trigger: 'change' }],
        extention: [{ required: true, message: 'extention is required', trigger: 'change' }]
      },
      downloadLoading: false
    }
  },
  created() {
    const id = this.$route.params && this.$route.params.id
    this.getList(id)
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
        etymaId: 0,
        desc: '',
        extention: '',
        phoneticSymbolUK: '',
        phoneticSymbolUS: '',
        fullDesc: '',
        zhDesc: '',
        frequency: 0,
        frequency2: '',
        examination: '',
        splitWord: '',
        splitWordDesc: '',
        level: '',
        status: 1
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
          wordAPI.post(this.temp).then(response => {
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
          wordAPI.post(this.temp).then(response => {
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
    },
    handleFetchPv(pv) {
      fetchPv(pv).then(response => {
        this.pvData = response.data.pvData
        this.dialogPvVisible = true
      })
    },
    handleDownload() {
      this.downloadLoading = true
      import('@/vendor/Export2Excel').then(excel => {
        const tHeader = ['timestamp', 'title', 'type', 'importance', 'status']
        const filterVal = ['timestamp', 'title', 'type', 'importance', 'status']
        const data = this.formatJson(filterVal, this.list)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: 'table-list'
        })
        this.downloadLoading = false
      })
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v => filterVal.map(j => {
        if (j === 'timestamp') {
          return parseTime(v[j])
        } else {
          return v[j]
        }
      }))
    }
  }
}
</script>
