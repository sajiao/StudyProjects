<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.Word" placeholder="word" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter"/>
			<el-input v-model="listQuery.Tags" placeholder="tag" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter"/>
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
      <el-table-column :label="$t('table.Id')" prop="Id" sortable="custom" align="center" width="65">
        <template slot-scope="scope">
          <span>{{ scope.row.Id }}</span>
        </template>
      </el-table-column>

      <el-table-column label="word" width="150px">
        <template slot-scope="scope">
          <a href="#" @click="handleUpdate(scope.row)">{{ scope.row.Word }}</a>
        </template>
      </el-table-column>
			   <el-table-column label="Tags" width="150px">
				<template slot-scope="scope">
					<template v-if="scope.row.edit">
					   <el-input v-model="scope.row.Tags" class="edit-input" size="small"/>
					   <el-button class="cancel-btn" size="small" icon="el-icon-cancel" type="warning" @click="cancelEdit(scope.row)">cancel</el-button>
					 </template>
					 <span v-else>{{ scope.row.Tags }}</span>
					 <el-button v-if="scope.row.edit" type="success" size="small" icon="el-icon-circle-check-outline" @click="confirmEdit(scope.row)">Ok</el-button>
					 <el-button v-else type="primary" size="small" icon="el-icon-edit" @click="scope.row.edit=!scope.row.edit"></el-button>
			  	 </template>
			</el-table-column>
      <el-table-column label="Trans" width="150px">
        <template slot-scope="scope">
          <span>{{ scope.row.Trans }}</span>
        </template>
      </el-table-column>
      <el-table-column label="SplitWordDesc" width="150px">
        <template slot-scope="scope">
          <span>{{ scope.row.SplitWordDesc }}</span>
        </template>
      </el-table-column>

      <el-table-column label="EtymaWord" width="150px">
        <template slot-scope="scope">
          <span>{{ scope.row.EtymaWord }}</span>
        </template>
      </el-table-column>

      <el-table-column label="phoneticSymbolUS" width="160px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.PhoneticSymbolUS }}</span>
        </template>
      </el-table-column>

      <el-table-column label="phoneticSymbolUK" width="160px" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.PhoneticSymbolUK }}</span>
        </template>
      </el-table-column>

      <el-table-column label="desc" align="center" width="150">
        <template slot-scope="scope">
          <span>{{ scope.row.Desc }}</span>
        </template>
      </el-table-column>

      <el-table-column label="zhDesc" align="center" width="150">
        <template slot-scope="scope">
          <span>{{ scope.row.ZhDesc }}</span>
        </template>
      </el-table-column>

      <el-table-column label="frequency" align="left" min-width="60">
        <template slot-scope="scope">
          <span>{{ scope.row.Frequency }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('table.actions')" align="center" width="230" class-name="small-padding fixed-width">
        <template slot-scope="scope">
					
          <el-button type="primary" size="mini" @click="handleUpdate(scope.row)">{{ $t('table.edit') }}</el-button>
					 <el-button type="primary" size="mini" @click="handleDelete(scope.row)">{{ $t('table.delete') }}</el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageIndex" :limit.sync="listQuery.pageSize" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="left" label-width="150px" style="width: 700px; margin-left:50px;">

        <el-form-item label="Word" prop="Word">
          <el-input v-model="temp.Word" />
        </el-form-item>
				
				 <el-form-item label="Tags">
				  <el-input v-model="temp.Tags" />
				</el-form-item>
				
        <el-form-item label="Trans" prop="Trans">
          <el-input v-model="temp.Trans" />
        </el-form-item>
				
        <el-form-item label="SplitWord">
          <el-input v-model="temp.SplitWord" />
        </el-form-item>

        <el-form-item label="SplitWordDesc">
          <el-input :autosize="{ minRows: 2, maxRows: 4}" v-model="temp.SplitWordDesc" type="textarea" placeholder="Please input" />
        </el-form-item>

        <el-form-item label="EtymaId" prop="etymaId">
          <el-input v-model="temp.EtymaId" />
        </el-form-item>

        <el-form-item label="Level">
          <el-input v-model="temp.Level" />
        </el-form-item>

        <el-form-item label="PhoneticSymbolUK" prop="PhoneticSymbolUK">
          <el-input v-model="temp.PhoneticSymbolUK" />
        </el-form-item>
        <el-form-item label="PhoneticSymbolUS" prop="phoneticSymbolUS">
          <el-input v-model="temp.PhoneticSymbolUS" />
        </el-form-item>
        <el-form-item label="Desc">
          <el-input :autosize="{ minRows: 2, maxRows: 4}" v-model="temp.Desc" type="textarea" placeholder="Please input" />
        </el-form-item>

        <el-form-item label="ZhDesc">
          <el-input :autosize="{ minRows: 2, maxRows: 4}" v-model="temp.ZhDesc" type="textarea" placeholder="Please input" />
        </el-form-item>

        <el-form-item label="FullDesc">
          <el-input :autosize="{ minRows: 2, maxRows: 6}" v-model="temp.FullDesc" type="textarea" placeholder="Please input" />
        </el-form-item>

        <el-form-item label="Frequency">
          <el-input v-model="temp.Frequency" />
        </el-form-item>
        <el-form-item label="Frequency2">
          <el-input v-model="temp.Frequency2" />
        </el-form-item>
        <el-form-item label="Examination">
          <el-input v-model="temp.Examination" />
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
        sort: 'Id'
      },
      importanceOptions: [1, 2, 3],
      sortOptions: [{ label: 'ID Ascending', key: '+Id' }, { label: 'ID Descending', key: '-Id' }],
      showReviewer: false,
      temp: {
        Id: undefined,
				Trans:'',
				Tags:'',
        Word: '',
        EtymaId: 0,
        Desc: '',
        Extention: '',
        PhoneticSymbolUK: '',
        PhoneticSymbolUS: '',
        FullDesc: '',
        ZhDesc: '',
        Frequency: 0,
        Frequency2: '',
        Examination: '',
        SplitWord: '',
        SplitWordDesc: '',
        Level: '',
        Status: 1
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
        Word: [{ required: true, message: 'word is required', trigger: 'change' }],
        Extention: [{ required: true, message: 'extention is required', trigger: 'change' }]
      },
      downloadLoading: false
    }
  },
  created() {
    const id = this.$route.params && this.$route.params.id
		this.listQuery.EtymaId = id;
    this.getList()
  },
  methods: {
    getList() {
      this.loading = true;
      baseapi.get(api.wordAPI,this.listQuery).then(response => {
        const items = response.data.Result.Results;
				this.total = response.data.Result.TotalCount;
        this.list = items.map(v => {
          this.$set(v, 'edit', false) // https://vuejs.org/v2/guide/reactivity.html
					v.originalTags = v.Tags;
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
      if (prop === 'Id') {
        this.sortByID(order)
      }
    },
    sortByID(order) {
      if (order === 'ascending') {
        this.listQuery.sort = 'Id'
      } else {
        this.listQuery.sort = 'Id'
      }
      this.handleFilter()
    },
    resetTemp() {
      this.temp = {
        Id: undefined,
				Trans:'',
        Word: '',
				Tags:'',
        EtymaId: 0,
        Desc: '',
        Extention: '',
        PhoneticSymbolUK: '',
        PhoneticSymbolUS: '',
        FullDesc: '',
        ZhDesc: '',
        Frequency: 0,
        Frequency2: '',
        Examination: '',
        plitWord: '',
        SplitWordDesc: '',
        Level: '',
        Status: 1
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
          baseapi.post(api.wordAPI,this.temp).then(response => {
            if (response.data.Id > 0) {
              this.list.unshift(response.data);
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
		 cancelEdit(row) {
		  row.Tags = row.originalTags;
		  row.edit = false;
		  this.$message({
		    message: 'The title has been restored to the original value',
		    type: 'warning'
		  })
		},
		confirmEdit(row){
			row.edit = false;
			baseapi.post(api.wordAPI,row).then(response => {
			  if (response.data.Id > 0) {
			    row.originalTags = row.Tags;
			    this.$notify({
			      title: '成功',
			      message: '更新成功',
			      type: 'success',
			      duration: 2000
			    })
			  }
			})
		
		},
    updateData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          baseapi.post(api.wordAPI,this.temp).then(response => {
            if (response.data.Id > 0) {
              for (const v of this.list) {
                if (v.Id === this.temp.Id) {
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
			debugger;
		 this.$confirm('确认是否删除?', '提示', {
						confirmButtonText: '确定',
						cancelButtonText: '取消',
						type: 'warning'
					}).then(() => {
						 const index = this.list.indexOf(row)
						this.list.splice(index, 1)
					  this.$notify({
					     title: '成功',
					     message: '删除成功',
					     type: 'success',
					     duration: 2000
					   })
					}).catch(() => {
						 this.$notify({
						   title: '失败',
						   message: '删除失败',
						   type: 'error',
						   duration: 2000
						 })      
					});
			// baseapi.delete(api.wordAPI,row.Id).then(response => {
			//   this.$notify({
			//      title: '成功',
			//      message: '删除成功',
			//      type: 'success',
			//      duration: 2000
			//    })
			// 	 const index = this.list.indexOf(row)
			// 	 this.list.splice(index, 1)
			// })
			     
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
