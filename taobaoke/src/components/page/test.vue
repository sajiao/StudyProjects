<template>
    <div class="table">
        <div class="container">
            <div class="handle-box">
                <el-input v-model="queryKeyword" placeholder="筛选关键词" class="handle-input mr10" id="query" style="width:300px"></el-input>
                <el-button type="primary" icon="el-icon-search" @click="search">搜索</el-button>
                <div style="float:right;">
                    <el-button type="primary" icon="delete" class="handle-del mr10" @click="addTest">添加题库</el-button>
                    <el-button type="primary" icon="delete" class="handle-del mr10" @click="delAll">批量删除</el-button>
                    <el-button type="primary" icon="delete" class="handle-del mr10" @click="exportTest">导出题库<i class="el-icon-download el-icon--right"></i></el-button>
                    <a href="" id="hideLink" hidden></a>
                    <el-button type="primary" icon="delete" class="handle-del mr10" @click="importDialog = true">导入题库<i class="el-icon-upload el-icon--right"></i></el-button>
                   
                </div>
            </div>
            <el-table v-loading="loading" :data="tableData" @selection-change="handleSelectionChange" height="650px" border style="width: 100%;max-height:650px;overflow-y:auto;" ref="multipleTable">
                <el-table-column type="selection" width="55"></el-table-column>
                <el-table-column prop="testName" label="题库名称">
                </el-table-column>
                <el-table-column prop="createdDate" label="创建时间" width="190">
                </el-table-column>
                <el-table-column label="操作" width="300">
                    <template slot-scope="scope">
                        <el-button size="small" @click="detail(scope.$index, scope.row)">详情</el-button>
                        <el-button size="small" @click="editTest(scope.$index, scope.row)">编辑</el-button>
                        <el-button size="small" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <div class="pagination">
                <el-pagination @size-change="handleSizeChange"
                               @current-change="handleCurrentChange"
                               :current-page.sync="currentPage"
                               :page-sizes="[10, 20, 30, 40, 50]"
                               :page-size="pageSize" background
                               layout="total, sizes, prev, pager, next"
                               :total="totalCount">
                </el-pagination>
            </div>
        </div>
        <!-- 删除提示框 -->
        <el-dialog title="提示" :visible.sync="delVisible" width="300px" center>
            <div class="del-dialog-cnt">删除不可恢复，是否确定删除？</div>
            <span slot="footer" class="dialog-footer">
                <el-button @click="delVisible = false">取 消</el-button>
                <el-button type="primary" @click="deleteRow">确 定</el-button>
            </span>
        </el-dialog>
        <el-dialog title="上传题库" :visible.sync="importDialog" width="600px" center>
            <div style="text-align:center">
                <el-upload class="upload-demo"
                           drag
                           :action="actionAddress"
                           :on-success="uploadSuccess"
                           :on-error="uploadError"
                           accept="zip"
                           :before-upload="beforeAvatarUpload"
                           >
                    <i class="el-icon-upload"></i>
                    <div class="el-upload__text"><em>点击上传</em></div>
                    <div class="el-upload__tip" slot="tip">只能上传zip文件</div>
                </el-upload>
            </div>
        </el-dialog>
    </div>
</template>

<script>
    import moduleService from '@/server/moduleService'
    import testService from '@/server/testService'
    import session from '@/store/storage'
    import api from '@/server/api'
    export default {
        name: 'basetable',
        data() {
            return {
                queryKeyword:'',
                tableData: [],
                totalCount: 0,
                currentPage: 1,
                pageSize: 10,
                totalPage: 0,
                multipleSelection: [],
                delVisible: false,
                loading: true,
                importDialog: false,
                actionAddress: api.baseAPI + api.importExportTestAPI,
            
            }
        },
        created() {
            this.getData();
        },
        computed: {
            data() {
                return this.tableData;
            }
        },
        methods: {
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
                this.currentPage = 1;
                this.getData();
            },
            getData() {
                this.loading = true;
                let param = {
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    testName: this.queryKeyword
                };

                testService.get(param).then((res) => {                    
                    if (res.data && res.data.results) {
                        this.tableData = res.data.results;
                        this.totalCount = Math.ceil(res.data.totalCount);
                        this.totalPage = Math.ceil(this.totalCount / this.pageSize);
                    } else {
                        this.tableData = [];
                    }                    
                    this.loading = false;
                })
            },
            addTest() {
                session.set("edit_testId", '');
                this.$router.push('/addtest');
            },
            editTest(index, row) {
                if (row) {
                    session.set("edit_testId", row.id);
                    this.$router.push('/addtest');
                }
            },
            detail(index, row) {
                if (row) {
                    session.set("detail_testId", row.id);
                    this.$router.push('/testdetail');
                }
            },
            handleDelete(index, row) {
                this.idx = index;
                this.delVisible = true;
            },
            delAll() {
                console.log(this.multipleSelection);
                this.$confirm('确定批量删除所选题库?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    this.loading = true;
                    const length = this.multipleSelection.length;

                    for (let i = 0; i < length; i++) {
                        testService.delete({ id: this.multipleSelection[i].id }).then((res) => {
                            if (res && res.data) {
                                this.$message.success('删除成功');
                                this.loading = false;
                                this.getData();           
                            }
                        });
                    }    
                }).catch(() => {
                    this.$message({
                        type: 'info',
                        message: '已取消删除'
                    });
                });                
            },
            handleSelectionChange(val) {
                this.multipleSelection = val;
            },
            deleteRow() {
                this.loading = true;
                this.delVisible = false;

                let param = this.tableData[this.idx];
                testService.delete({ id: param.id}).then((res) => {
                    console.log(res);
                    this.loading = false;
                    this.tableData.splice(this.idx, 1);
                    this.$message.success('删除成功');
                    this.getData();
                });
            },
            exportTest() {
                let ids = "";
                 if (this.multipleSelection.length > 0) {
                        ids = "Id in (";
                        for (let i = 0; i < this.multipleSelection.length; i++) {
                            ids += "'" + this.multipleSelection[i].id + "',";
                        }
                    } else {
                        this.$notify.error({
                            title: '错误',
                            message: '请选择需要导出的题库'
                        });
                        return;
                    }
                    if (ids.length > 0) {
                        ids = ids.substr(0, ids.length - 1) + ")";
                    }
                

                document.getElementById("hideLink").href = api.baseAPI + "ImportExportTest/get?ids=" + ids;
                document.getElementById("hideLink").click();
            },

            uploadSuccess(response, file, fileList) {
               
                this.$notify.success({
                    title: '成功',
                    message: '导入题库成功'
                });
                this.queryKeyword = "";
                this.search();
            },
            uploadError(error, file, fileList) {
                this.$notify.error({
                    title: '错误',
                    message: error.message
                });
            },
            beforeAvatarUpload(file) {
                const isZip = file.type === 'application/x-zip-compressed';

                if (!isZip) {
                    this.$notify.error({
                        title: '错误',
                        message: '上传文件必须是Zip格式!'
                    });
                }
              
                return isZip;
            },
        }
    }

</script>

<style scoped>
    .handle-box {
        margin-bottom: 20px;
    }
    .handle-select {
        width: 120px;
    }

    .handle-input {
        display: inline-block;
    }
   
    .del-dialog-cnt {
        font-size: 16px;
        text-align: center
    }
</style>
