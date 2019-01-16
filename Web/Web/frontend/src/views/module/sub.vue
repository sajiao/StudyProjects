<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.word" placeholder="word" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">{{ $t('table.search') }}</el-button>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">{{ $t('table.add') }}</el-button>
    
    </div>

    <el-table v-loading="loading"
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

      <el-table-column label="Name" width="150px">
        <template slot-scope="scope">
          <a href="#" @click="handleUpdate(scope.row)">{{ scope.row.name }}</a>
        </template>
      </el-table-column>

      <el-table-column label="Alias" width="150px">
        <template slot-scope="scope">
          <span>{{ scope.row.alias }}</span>
        </template>
      </el-table-column>

      <el-table-column label="ModuleId" width="150px">
        <template slot-scope="scope">
          <span>{{ scope.row.moduleId }}</span>
        </template>
      </el-table-column>

      <el-table-column label="ModuleAlias" width="160px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.moduleAlias }}</span>
        </template>
      </el-table-column>

      <el-table-column label="IfOpen" width="160px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.ifOpen }}</span>
        </template>
      </el-table-column>

      <el-table-column label="IsClient" align="center" width="150">
        <template slot-scope="scope">
          <span>{{ scope.row.isClient }}</span>
        </template>
      </el-table-column>

      <el-table-column label="OpenNeedPlayLv" align="center" width="150">
        <template slot-scope="scope">
          <span>{{ scope.row.openNeedPlayLv }}</span>
        </template>
      </el-table-column>

      <el-table-column label="OpenNeedPlayIsVip" align="left" min-width="60">
        <template slot-scope="scope">
          <span>{{ scope.row.openNeedPlayIsVip }}</span>
        </template>
      </el-table-column>

      <el-table-column label="OpenNeedPlayIsVip" align="left" min-width="60">
        <template slot-scope="scope">
          <span>{{ scope.row.openNeedPlayIsVip }}</span>
        </template>
      </el-table-column>

      <el-table-column label="AdvancedOpenVIPLv" align="left" min-width="60">
        <template slot-scope="scope">
          <span>{{ scope.row.advancedOpenVIPLv }}</span>
        </template>
      </el-table-column>
      <el-table-column label="NeedTaskID" align="left" min-width="60">
        <template slot-scope="scope">
          <span>{{ scope.row.needTaskID }}</span>
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

        <el-form-item label="ID" prop="name">
          <el-input v-model="temp.id" />
        </el-form-item>

        <el-form-item label="Name" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>

        <el-form-item label="Alias">
          <el-input v-model="temp.alias" />
        </el-form-item>

        <el-form-item label="ModuleId">
          <el-select v-model="temp.moduleId" filterable placeholder="请选择">
            <el-option v-for="item in modules"
                       :key="item.id"
                       :label="item.name"
                       :value="item.id">
            </el-option>
          </el-select>
        </el-form-item>

        <el-form-item label="IfOpen">
          <el-checkbox v-model="temp.ifOpen">是否开放</el-checkbox>
        </el-form-item>
        <el-form-item label="IsClient">
          <el-checkbox v-model="temp.isClient">是否是客户端模块</el-checkbox>
        </el-form-item>

        <el-form-item label="OpenNeedPlayLv">
          <el-input v-model="temp.openNeedPlayLv" />
        </el-form-item>
        <el-form-item label="OpenNeedPlayIsVip">
          <el-checkbox v-model="temp.openNeedPlayIsVip">是否当前是vip</el-checkbox>
        </el-form-item>
        <el-form-item label="AdvancedOpenVIPLv">
          <el-input v-model="temp.advancedOpenVIPLv" />
        </el-form-item>
        <el-form-item label="NeedTaskID">
          <el-input v-model="temp.needTaskID" />
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
        id: 0,
        name: '',
        alias: '',
        moduleId: undefined,
        moduleAlias: '',
        ifOpen: false,
        isClient: false,
        openNeedPlayLv: 0,
        openNeedPlayIsVip: false,
        advancedOpenVIPLv: 0,
        needTaskID: 0
      },
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: 'Edit',
        create: 'Create'
      },
      modules:[],
      rules: {
        word: [{ required: true, message: 'word is required', trigger: 'change' }],
        extention: [{ required: true, message: 'extention is required', trigger: 'change' }]
      }
    }
  },
    created() {
      const id = this.$route.params && this.$route.params.id

      baseapi.get(api.moduleAPI, null).then(response => {
        this.modules = response.data.result;
      });

     this.getList(id)

  },
  methods: {
    getList(id) {
      this.loading = true
      this.listQuery.moduleId = id
      baseapi.get(api.modulesubAPI,this.listQuery).then(response => {
        const items = response.data.result
        this.list = items;
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
        id: 0,
        name: '',
        alias: '',
        moduleId: undefined,
        moduleAlias: '',
        ifOpen: false,
        isClient: false,
        openNeedPlayLv: 0,
        openNeedPlayIsVip: false,
        advancedOpenVIPLv: 0,
        needTaskID: 0
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
          baseapi.post(api.modulesubAPI,this.temp).then(response => {
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
          baseapi.post(api.modulesubAPI,this.temp).then(response => {
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
