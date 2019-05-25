<template>
    <div class="">
        <div class="crumbs">
        </div>
        <div class="container" v-loading.fullscreen.lock="fullloading" :element-loading-text="loadingText"
             element-loading-spinner="el-icon-loading" element-loading-background="rgba(255, 255, 255, 0.8)">

            <div>
                <el-tabs v-model="message">
                    <el-tab-pane :label="`题库`" name="first">
                        <template>
                            <el-form>
                                <el-form-item label="题库名称" :label-width="formLabelWidth">
                                    <el-row>
                                        <el-input v-model="entity.testName" style="width:200px"></el-input>
                                    </el-row>
                                </el-form-item>
                                <el-form-item label="考试时间" :label-width="formLabelWidth">
                                    <el-row>
                                        <el-select v-model="tempExaminationTimeObj" placeholder="选择考试时间">
                                            <el-option v-for="(item, index) in examinationTimeArray" :key="item.key" :label="item.title" :value="item"></el-option>
                                        </el-select>
                                        &nbsp;&nbsp;<el-input v-if="tempExaminationTimeObj.key==5" v-model="entity.testEntity.examinationTime" type="number" placeholder="请输入考试时间"></el-input><span v-if="tempExaminationTimeObj.key==5">分钟</span>
                                    </el-row>
                                </el-form-item>
                                <el-form-item label="信号源" :label-width="formLabelWidth">
                                    <el-row>
                                        <el-select v-model="entity.testEntity.signalSource.source" placeholder="请选择" @change="singalSourceChange()">
                                            <el-option v-for="item in servers" :key="item" :label="item" :value="item">
                                            </el-option>
                                        </el-select>
                                        &nbsp;&nbsp;<el-button v-if="servers.length==0" type="primary" @click="getLocalServers" title="连接OPC服务器获取变量列表">重新加载</el-button>
                                        <el-button type="primary" @click="connectOPCServer" title="连接OPC服务器获取变量列表">连接</el-button>
                                    </el-row>
                                </el-form-item>

                                <el-form-item label="变量名" :label-width="formLabelWidth" v-if="entity.testEntity.triggerCondition.conditionItem.key == 3 || entity.testEntity.endCondition.conditionItem.key == 3">
                                    <el-row>
                                        <el-button v-if="entity.testEntity.variables.length == 0" type="primary" @click="addVariable" title="随机生成变量名">生成变量</el-button>
                                        <el-button v-if="entity.testEntity.variables.length > 0" type="primary" @click="removeVariable" title="删除变量">删除变量</el-button>
                                    </el-row>
                                    <el-row v-for="(item,index) in entity.testEntity.variables" :key="index">
                                        <el-tag>
                                            {{item.variableName}}
                                        </el-tag>
                                        <el-button type="primary" @click="bindTestVariable(item)" title="绑定变量到数据源信号">绑定信号</el-button>
                                        <span>{{item.bindSignalName}}</span>
                                    </el-row>
                                </el-form-item>
                                <el-form-item label="启动条件" :label-width="formLabelWidth">
                                    <el-row>
                                        <el-select v-model="entity.testEntity.triggerCondition.conditionItem" placeholder="选择条件类型">
                                            <el-option v-for="(item, index) in triggerItems" :key="index" :label="item.value" :value="item"></el-option>
                                        </el-select>
                                        <span v-if="entity.testEntity.triggerCondition.conditionItem.key == 1">手动点击开始考试按钮</span>
                                        <span v-if="entity.testEntity.triggerCondition.conditionItem.key == 2">时间为</span>
                                        <el-select v-model="entity.testEntity.triggerCondition.value" placeholder="选择启动时间" v-if="entity.testEntity.triggerCondition.conditionItem.key == 2">
                                            <el-option v-for="(item, index) in timeItems" :key="index" :label="item" :value="item"></el-option>
                                        </el-select>
                                        <span v-if="entity.testEntity.triggerCondition.conditionItem.key == 2">点自动启动</span>
                                        <el-select v-if="entity.testEntity.triggerCondition.conditionItem.key == 3" v-model="entity.testEntity.triggerCondition.variable" placeholder="选择变量" value-key="title">
                                            <el-option v-for="(item, index) in entity.testEntity.variables" :key="index" :label="item.title" :value="item"></el-option>
                                        </el-select>
                                        <el-tag v-if="entity.testEntity.triggerCondition.conditionItem.key == 3 && entity.testEntity.variables.length==0" style="color:red">
                                            请生成变量
                                        </el-tag>
                                        <span v-if="entity.testEntity.triggerCondition.conditionItem.key == 3"> 等于 </span>
                                        <el-input v-if="entity.testEntity.triggerCondition.conditionItem.key == 3" v-model="entity.testEntity.triggerCondition.value" type="number" placeholder="请输入值" style="width:150px;"></el-input>
                                        <span v-if="entity.testEntity.triggerCondition.conditionItem.key == 3">时启动</span>
                                    </el-row>
                                </el-form-item>
                                <el-form-item label="结束条件" :label-width="formLabelWidth">
                                    <el-row>
                                        <el-select v-model="entity.testEntity.endCondition.conditionItem" placeholder="选择条件类型">
                                            <el-option v-for="(item, index) in endItems" :key="index" :label="item.value" :value="item"></el-option>
                                        </el-select>
                                        <span v-if="entity.testEntity.endCondition.conditionItem.key == 1">手动点击结束考试按钮</span>
                                        <span v-if="entity.testEntity.endCondition.conditionItem.key == 2">时间为</span>
                                        <el-select v-model="entity.testEntity.endCondition.value" placeholder="选择结束时间" v-if="entity.testEntity.endCondition.conditionItem.key == 2">
                                            <el-option v-for="(item, index) in timeItems" :key="index" :label="item" :value="item"></el-option>
                                        </el-select>
                                        <span v-if="entity.testEntity.endCondition.conditionItem.key == 2">点自动结束</span>
                                        <el-select v-model="entity.testEntity.endCondition.variable" placeholder="选择变量" v-if="entity.testEntity.endCondition.conditionItem.key == 3" value-key="title">
                                            <el-option v-for="(item, index) in entity.testEntity.variables" :key="index" :label="item.title" :value="item"></el-option>
                                        </el-select>
                                        <el-tag v-if="entity.testEntity.endCondition.conditionItem.key == 3 && entity.testEntity.variables.length==0" style="color:red">
                                            请生成变量
                                        </el-tag>
                                        <span v-if="entity.testEntity.endCondition.conditionItem.key == 3"> 等于 </span>
                                        <el-input v-if="entity.testEntity.endCondition.conditionItem.key == 3" v-model="entity.testEntity.endCondition.value" type="number" placeholder="请输入值" style="width:150px;"></el-input>
                                        <span v-if="entity.testEntity.endCondition.conditionItem.key == 3">时结束</span>
                                    </el-row>
                                </el-form-item>
                            </el-form>

                        </template>
                    </el-tab-pane>
                    <el-tab-pane :label="`评分模块`" name="second">
                        <template>
                            <div class="handle-row" style="float:left;">
                                <el-button type="default" @click="addModule">添加模块</el-button>
                            </div>
                            <el-table :data="entity.testEntity.modules" height="550px" style="width: 100%;max-height:550px;overflow-y:auto;">
                                <el-table-column label="名称">
                                    <template slot-scope="scope">
                                        <span>{{scope.row.moduleName}}</span>
                                    </template>
                                </el-table-column>
                                <el-table-column width="200" label="操作">
                                    <template slot-scope="scope">
                                        <el-button @click="bindModuleVariable(scope.$index,scope.row)">绑定变量</el-button>
                                        <el-button type="danger" @click="delModule(scope.$index)">删除</el-button>
                                    </template>
                                </el-table-column>
                            </el-table>
                        </template>
                        <el-dialog width="40%" :title="`评分模块`" :visible.sync="showDialog" append-to-body @close="showDialog=false" center>
                            <div class="search">
                                <el-input placeholder="请输入内容" v-model="queryKeyword" class="input-with-select">
                                    <el-button slot="append" icon="el-icon-search" @click="queryModule"></el-button>
                                </el-input>
                            </div>
                            <el-table ref="multipleTable"
                                      :data="tableData"
                                      v-loading="loading"
                                      tooltip-effect="dark"
                                      style="width: 100%; margin-top:10px;"
                                      border
                                      @selection-change="handleSelectionChange">
                                <el-table-column type="selection"
                                                 width="55">
                                </el-table-column>
                                <el-table-column prop="moduleName" label="模块名称">
                                </el-table-column>
                            </el-table>
                            <div class="pagination">
                                <el-pagination @size-change="handleSizeChange"
                                               @current-change="handleCurrentChange"
                                               :current-page.sync="currentPage"
                                               :page-size="pageSize" background
                                               layout="total, prev, pager, next"
                                               :total="totalCount">
                                </el-pagination>
                            </div>

                            <div slot="footer" class="dialog-footer">
                                <el-button @click="showDialog=false">取 消</el-button>
                                <el-button type="primary" @click="confirmModule">确 定</el-button>
                            </div>
                        </el-dialog>
                        <el-dialog width="40%" :title="`评分模块变量绑定`" :visible.sync="showModuleBindDialog" append-to-body @close="showModuleBindDialog=false" center>
                            <el-form>
                                <el-form-item :key="index" v-for="(item, index) in module.moduleEntity.variables">
                                    变量: <el-tag style="width:25px;text-align:center;"> {{item.variableName}} </el-tag>
                                    <el-button type="primary" @click="setModuleBindDetails(item)" title="绑定变量到数据源信号">绑定信号</el-button>
                                    <span e-if="item.bindSignalName != ''">{{item.bindSignalName}}</span>
                                </el-form-item>
                            </el-form>
                            <div slot="footer" class="dialog-footer">
                                <el-button @click="showModuleBindDialog=false">取 消</el-button>
                                <el-button type="primary" @click="bindModuleVariableConfirm">绑定</el-button>
                            </div>

                            <el-dialog width="50%" :visible.sync="moduleBindDetails" append-to-body @close="moduleBindDetails=false" center>
                                <el-card class="box-card" style="height:100%" shadow="never">
                                    <div style="height:500px;max-height:500px;overflow-y:auto;" width="100%">
                                        <el-tree :data="singleSource" accordion node-key="itemId" @node-click="handleNodeClick" :props="defaultProps"></el-tree>
                                    </div>
                                </el-card>
                                <div slot="footer" class="dialog-footer">
                                    <el-button @click="moduleBindDetails=false">取 消</el-button>
                                    <el-button type="primary" @click="moduleBindDetailsConfirm">绑定</el-button>
                                </div>
                            </el-dialog>

                        </el-dialog>
                    </el-tab-pane>
                </el-tabs>
                <el-dialog width="50%" :title="`题库变量绑定`" :visible.sync="showTestBindDialog" append-to-body @close="showTestBindDialog=false" center>
                    <el-card class="box-card" style="height:100%" shadow="never">
                        <div style="height:500px;max-height:500px;overflow-y:auto;" width="100%">
                            <el-tree :data="singleSource" accordion node-key="itemId" @node-click="handleNodeClick" :props="defaultProps"></el-tree>
                        </div>
                    </el-card>
                    <div slot="footer" class="dialog-footer">
                        <el-button @click="showTestBindDialog=false">取 消</el-button>
                        <el-button type="primary" @click="bindTestVariableConfirm">绑定</el-button>
                    </div>
                </el-dialog>
                <div class="handle-row">
                    <el-button type="primary" @click="saveall">保存全部</el-button>
                    <el-button type="primary" @click="goBackList">返回列表</el-button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import { formatDate } from "@/filters/date"
    import $ from 'jquery'
    import testService from '@/server/testService'
    import moduleService from '@/server/moduleService'
    import opcClientService from '@/server/opcClientService'
    import session from '@/store/storage'
    import common from '@/common/common'
    import eventBus from '@/components/common/bus'
    import { debug } from "util";
    export default {
        name: 'tabs',
        data() {
            return {
                message: 'first',
                loading: false,
                fullloading: false,
                loadingText:'',
                showDialog: false,
                queryKeyword: '',
                tableData: [],
                totalCount: 0,
                currentPage: 1,
                pageSize: 10,
                totalPage: 0,
                multipleSelection: [],
                showTestBindDialog: false,
                showModuleBindDialog: false,
                moduleBindDetails: false,
                currentBindModule: '',
                currentBindTest: '',
                bindSelectName: '',
                bindSelectIsItem: false,
                tempExaminationTimeObj: {key:0,value:1,title:''},
                testId: '',
                entity: {
                    testName: '',
                    testEntity: {
                        signalSource: {
                            source: ''
                        },
                        examinationTime: 0,
                        variables: [],
                        triggerCondition: {
                            variable: {
                                variableName: ''
                            },
                            conditionItem: {},
                            value:''
                        },
                        endCondition: {
                            variable: {
                                variableName: ''
                            },
                            conditionItem: {},
                            value:''
                        },
                        modules: [],
                    }
                },
                defaultProps: {
                    children: 'children',
                    label: 'name'
                },
                singleSource: [],
                module: {
                    moduleEntity: {
                        variables: [],
                    }
                },
                timeItems: [
                    7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22
                ],
                triggerItems: [
                    {
                        key: '1',
                        value: '手动启动'
                    },
                    {
                        key: '2',
                        value: '定时启动'
                    },
                    {
                        key: '3',
                        value: '参数达到设定值后启动'
                    }
                ],
                endItems: [
                    {
                        key: '1',
                        value: '手动结束'
                    },
                    {
                        key: '2',
                        value: '定时结束'
                    },
                    {
                        key: '3',
                        value: '参数达到设定值后结束'
                    },
                ],
                examinationTimeArray: [
                    {
                        key: 1, value: 60, title: '60分钟'
                    }, {
                        key: 2, value: 90, title: '90分钟'
                    }, {
                        key: 3, value: 120, title: '120分钟'
                    }, {
                        key: 4, value: 150, title: '150分钟'
                    }, {
                        key: 5, value: 0, title: '自定义'
                    }
                ],
                formLabelWidth: '150px',
                servers: [],
                strGetServersType: 'GETLOCALSERVERS',
                strGetServerItemsType:'GETSERVERITEMS',
            }
        },
        created() {

            this.testId = session.get("edit_testId");
            if (this.testId != undefined && this.testId != '') {
                this.getData(this.testId);
            } else {
                this.testId = common.Guid();
                this.entity.id = this.testId;
            }

            this.getLocalServers();
            eventBus.$on('get_message_from_socket_server_GETLOCALSERVERS', target => {
                this.setLocalServers(target);
            });
            eventBus.$on("get_message_from_socket_server_GETSERVERITEMS", target => {
                this.setServerItems(target);
            });
        },
        methods: {
            getData(testId) {
                testService.get({ id: testId }).then((res) => {
                    if (res.data) {
                        this.entity = res.data;
                        var obj = this.examinationTimeArray.filter(c => c.value == this.entity.testEntity.examinationTime);
                        if (obj.length > 0) {
                            this.tempExaminationTimeObj = obj[0];
                        } else {
                            this.tempExaminationTimeObj = this.examinationTimeArray[4];
                        }
                    }
                })
            },
            connectOPCServer() {
                let sourceName = this.entity.testEntity.signalSource.source;
                if (!common.valueIsNotNullOrEmpty(sourceName)) {
                    return this.reject('请先选择数据源!');
                }

                this.fullloading = true;
                this.loadingText = '连接中...';

                eventBus.$emit("send_message", this.strGetServerItemsType, this.strGetServerItemsType + "-" + sourceName);

                //opcClientService.get({ opcServerName: sourceName }).then((res) => {
                //    if (res.data && res.data.success) {
                //        this.singleSource = res.data.results;
                //        this.$notify.success({
                //            type: '提示',
                //            message: '连接成功'
                //        });
                //    } else {
                //        this.reject('信号源连接失败，请稍后再试');
                //        this.singleSource = ['测试信号源1', '测试信号源2', '测试信号源3', '测试信号源4', '测试信号源5',];
                //    }
                //});
            },
            setServerItems(event) {
                if (event != null && event.data) {
                    this.fullloading = false;
                    this.loadingText = '';
                    if (event.data.indexOf(this.strGetServerItemsType) >= 0) {
                        let data = event.data;
                        if (data.indexOf(this.strGetServerItemsType) >= 0) {
                            data = data.substr(this.strGetServerItemsType.length);
                        }

                        if (data.indexOf('ERROR') == 0) {
                            this.$notify.error({
                                title: '错误',
                                message: data
                            });
                            return;
                        }

                        let jsonObj = JSON.parse(data);
                        this.singleSource = jsonObj;

                        this.$notify.success({
                            type: '提示',
                            message: '连接成功'
                        });
                    }
                }
            },
            randomWord(randomFlag, min, max) {
                var str = "",
                    range = min,
                    arr = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

                // 随机产生
                if (randomFlag) {
                    range = Math.round(Math.random() * (max - min)) + min;
                }
                for (var i = 0; i < range; i++) {
                    var pos = Math.round(Math.random() * (arr.length - 1));
                    str += arr[pos];
                }
                return str;
            },
            addVariable() {
                if (this.entity.testEntity.variables.length == 0) {
                    var name = "A";
                    this.entity.testEntity.variables.push({ id: common.Guid(), title: '变量' + name, variableName: name, isDelete: 0, createdDate: common.getNowFormatDate(), isChecked: false, bindSignalName: '' });
                    name = "B";
                    this.entity.testEntity.variables.push({ id: common.Guid(), title: '变量' + name, variableName: name, isDelete: 0, createdDate: common.getNowFormatDate(), isChecked: false, bindSignalName: '' });
                }
            },
            removeVariable() {
                this.entity.testEntity.variables = [];
            },
            delModule(index) {
                this.$confirm('确定删除该评分模块?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    this.entity.testEntity.modules.splice(index, 1);
                    this.$message({
                        type: 'success',
                        message: '删除成功!'
                    });
                }).catch(() => {
                    this.$message({
                        type: 'info',
                        message: '已取消删除'
                    });
                });
            },
            handleCurrentChange(val) {
                this.currentPage = val;
                this.queryModule();
            },
            handleSizeChange(v) {
                this.currentPage = 1;
                this.pageSize = v;
                this.queryModule();
            },
            queryModule() {
                let param = {
                    pageIndex: this.currentPage,
                    pageSize: this.pageSize,
                    ModuleName: this.queryKeyword
                };
                moduleService.get(param).then((res) => {
                    if (res && res.statusText == "OK") {
                        if (res.data && res.data.results) {
                            this.tableData = res.data.results;
                        }
                        this.totalCount = Math.ceil(res.data.totalCount);
                        this.totalPage = Math.ceil(this.totalCount / this.pageSize);
                    }
                    this.loading = false;
                })
            },
            handleSelectionChange(val) {
                this.multipleSelection = val;
            },
            toggleSelection(rows) {
                if (rows) {
                    rows.forEach(row => {
                        this.$refs.multipleTable.toggleRowSelection(row);
                    });
                } else {
                    this.$refs.multipleTable.clearSelection();
                }
            },
            addModule() {
                this.showDialog = true;
                this.queryModule();
            },
            confirmModule() {
                if (this.multipleSelection.length > 0) {
                    for (var i = 0; i < this.multipleSelection.length; i++) {
                        var item = this.multipleSelection[i];
                        //var temps = this.entity.testEntity.modules.filter(m => m.id == item.id);
                        //if (temps == null || temps.length == 0) {
                            this.entity.testEntity.modules.push(item);
                        //}
                    }
                    this.multipleSelection = [];
                    this.toggleSelection();
                }
                this.showDialog = false;
            },
            //bind Varaibles from Source
            bindModuleVariable(index, item) {
                this.showModuleBindDialog = true;
                this.module = item;
            },
            moduleBindDetailsConfirm() {
                if (!common.valueIsNotNullOrEmpty(this.bindSelectName)) {
                    this.$notify.info({
                        type: '消息',
                        message: '请选择需要绑定的信号!'
                    });
                    return;
                }
                if (!this.bindSelectIsItem) {
                    return this.reject('选中的信号不是有效的Item值，请重新选择!');
                }

                if (this.module != null && this.module.moduleEntity != null && this.module.moduleEntity.variables != null) {
                    for (var i = 0; i < this.module.moduleEntity.variables.length; i++) {
                        if (this.module.moduleEntity.variables[i].variableName == this.currentBindModule) {
                            this.module.moduleEntity.variables[i].bindSignalName = this.bindSelectName;
                        }
                    }
                }
                this.moduleBindDetails = false;
                this.bindSelectName = "";
            },
            bindModuleVariableConfirm() {
                var result = true;
                if (this.module != null && this.module.moduleEntity != null && this.module.moduleEntity.variables != null) {
                    for (var i = 0; i < this.module.moduleEntity.variables.length; i++) {
                        var temp = this.module.moduleEntity.variables[i];
                        if (temp.bindSignalName == null || temp.bindSignalName == '') {
                            result = false;
                            break;
                        }
                    }
                }

                if (result == false) {
                    return this.reject('所有变量必须绑定信号!');
                }
                this.showModuleBindDialog = false;
            },
            setModuleBindDetails(item) {
                if (this.singleSource == null || this.singleSource == undefined || this.singleSource.length == 0) {
                    this.$notify.info({
                        type: '消息',
                        message: '请先连接信号源!'
                    });
                    return;
                }

                this.moduleBindDetails = true;
                this.currentBindModule = item.variableName;
            },
            bindTestVariable(item) {
                if (this.entity.testEntity.variables.length == 0) {                    
                    return this.reject('请先生成变量!');
                }

                if (this.singleSource == null || this.singleSource == undefined || this.singleSource.length == 0) {
                    return this.reject('请先连接信号源!');
                }

                this.currentBindTest = item.variableName;
                this.showTestBindDialog = true;
            },
            bindTestVariableConfirm() {
                if (!common.valueIsNotNullOrEmpty(this.bindSelectName)) {
                    this.$notify.info({
                        type: '消息',
                        message: '请选择需要绑定的信号!'
                    });
                    return;
                }
                if (!this.bindSelectIsItem) {
                    this.$notify.error({
                        type: '消息',
                        message: '选中的信号不是有效的Item值，请重新选择!'
                    });
                    return;
                }

                for (var i = 0; i < this.entity.testEntity.variables.length; i++) {
                    if (this.entity.testEntity.variables[i].variableName == this.currentBindTest) {
                        this.entity.testEntity.variables[i].bindSignalName = this.bindSelectName;
                    }
                }

                this.showTestBindDialog = false;
                this.bindSelectName = "";
            },
            handleNodeClick(data) {
                if (data == null || data == undefined) {
                    this.$notify.info({
                        type: '消息',
                        message: '请选择需要绑定的信号!'
                    });
                    return;
                }                

                this.bindSelectIsItem = data.isItem;
                this.bindSelectName = data.itemId;
            },
            goBackList() {
                this.$router.push('/test');
            },
            reject(errMessage) {
                this.$notify.error({
                    title: '消息',
                    message: errMessage
                });
                return false;
            },
            saveall() {
                if (!common.valueIsNotNullOrEmpty(this.entity.testName)) {
                    return this.reject("请输入题库名称");
                }

                if (this.tempExaminationTimeObj.key == 0) {
                    return this.reject("请选择考试时间");
                }

                if (this.tempExaminationTimeObj.key == 5 && this.entity.testEntity.examinationTime <= 0) {
                    return this.reject("考试时间必须大于0");
                }

                if (!common.valueIsNotNullOrEmpty(this.entity.testEntity.signalSource.source)) {
                    return this.reject("请选择信号源");
                }

                if (this.entity.testEntity.variables.length > 0) {
                    var allbind = true;
                    for (var i = 0; i < this.entity.testEntity.variables.length; i ++){
                        if (!common.valueIsNotNullOrEmpty(this.entity.testEntity.variables[i].bindSignalName)) {
                            allbind = false;
                            break;
                        }
                    }
                    if (!allbind) {
                        return this.reject("所有变量必须绑定信号");
                    }
                }
            
                if (this.entity.testEntity.triggerCondition.conditionItem == null || this.entity.testEntity.triggerCondition.conditionItem.key == null) {
                    return this.reject("必须选择开始条件");
                }
                if (this.entity.testEntity.endCondition.conditionItem == null || this.entity.testEntity.endCondition.conditionItem.key == null) {
                    return this.reject("必须选择结束条件");
                }
                if ((this.entity.testEntity.triggerCondition.conditionItem.key == 3 || this.entity.testEntity.endCondition.conditionItem.key == 3) && this.entity.testEntity.variables.length == 0) {
                    return this.reject("请生成变量并绑定信号");
                }
                if (this.entity.testEntity.triggerCondition.conditionItem.key == 3) {
                    if (this.entity.testEntity.triggerCondition.variable.variableName == "") {
                        return this.reject("请选择启动条件变量");
                    }
                    if (this.entity.testEntity.triggerCondition.value == '') {
                        return this.reject("请输入启动条件判定值");
                    }
                }
                if (this.entity.testEntity.endCondition.conditionItem.key == 3) {
                    if (this.entity.testEntity.endCondition.variable.variableName == "") {
                        return this.reject("请选择结束条件变量");
                    }
                    if (this.entity.testEntity.endCondition.value == '') {
                        return this.reject("请输入结束条件判定值");
                    }
                }
                if (this.entity.testEntity.triggerCondition.conditionItem.key == 2 && (this.entity.testEntity.triggerCondition.value < 7 || this.entity.testEntity.triggerCondition.value > 22)) {
                    return this.reject("考试启动时间必须在7点-22点之间");
                }
                if (this.entity.testEntity.endCondition.conditionItem.key == 2 && (this.entity.testEntity.endCondition.value < 7 || this.entity.testEntity.endCondition.value > 22)) {
                    return this.reject("考试结束时间必须在7点-22点之间");
                }

                if (this.entity.testEntity.modules == null || this.entity.testEntity.modules.length == 0) {
                    return this.reject("请添加评分模块");
                }
                var checkResult = true;
                this.entity.testEntity.modules.forEach(m => {
                    for (var i = 0; i < m.moduleEntity.variables.length; i++) {
                        var temp = m.moduleEntity.variables[i];
                        if (temp.bindSignalName == null || temp.bindSignalName == '') {
                            checkResult = false;
                            break;
                        }
                    }
                });
                if (checkResult == false) {
                    this.$notify.error({
                        title: '消息',
                        message: '所有评分模块变量必须绑定信号！'
                    });
                    return;
                }

                this.fullloading = true;
                this.loadingText = '正在保存...';

                if (this.entity.testEntity.triggerCondition.conditionItem.key == 3) {
                    this.entity.testEntity.triggerCondition.variableId = this.entity.testEntity.triggerCondition.variable.id;
                    this.entity.testEntity.triggerCondition.variableName = this.entity.testEntity.triggerCondition.variable.variableName;
                }

                if (this.entity.testEntity.endCondition.conditionItem.key == 3) {
                    this.entity.testEntity.endCondition.variableId = this.entity.testEntity.endCondition.variable.id;
                    this.entity.testEntity.endCondition.variableName = this.entity.testEntity.endCondition.variable.variableName;
                }

                if (this.tempExaminationTimeObj.key != 5) {
                    this.entity.testEntity.examinationTime = this.tempExaminationTimeObj.value;
                }

                testService.post(this.entity).then((res) => {
                    this.fullloading = false;
                    this.loadingText = '';

                    console.log(res);
                    if (res && res.data) {
                        this.$notify({
                            type: 'success',
                            title: '成功',
                            message: '保存成功'
                        });
                        this.$router.push('/test');
                    } else {
                        this.$notify.error({
                            title: '错误',
                            message: "服务访问错误，请检查网络"
                        });
                    }
                })
            },
            getLocalServers() {
                //this.fullloading = true;
                //this.loadingText = "正在加载OPC服务器列表...";

                eventBus.$emit("send_message", this.strGetServersType, this.strGetServersType);
            },
            setLocalServers(event) {
                if (event != null && event.data) {
                    this.fullloading = false;
                    this.loadingText = "";

                    if (event.data.indexOf(this.strGetServersType) >= 0) {
                        var data = event.data;
                        if (data.indexOf(this.strGetServersType) >= 0) {
                            data = data.substr(this.strGetServersType.length);
                        }
                        if (data.indexOf('ERROR') >= 0) {
                            this.$notify.error({
                                title: '错误',
                                message: data
                            });
                            return;
                        }
                        
                        var serverList = data.split(',');
                        this.servers = serverList;
                    }
                }
            },
            singalSourceChange() {
                this.singleSource = [];

                for (var i = 0; i < this.entity.testEntity.variables.length; i++) {
                    if (common.valueIsNotNullOrEmpty(this.entity.testEntity.variables[i].bindSignalName)) {
                        this.entity.testEntity.variables[i].bindSignalName = "";
                    }
                } 

                this.entity.testEntity.modules.forEach(m => {
                    for (var i = 0; i < m.moduleEntity.variables.length; i++) {
                        var temp = m.moduleEntity.variables[i];
                        if (common.valueIsNotNullOrEmpty(m.moduleEntity.variables[i].bindSignalName)) {
                            m.moduleEntity.variables[i].bindSignalName = '';
                        }
                    }
                });
            },
        },
        computed: {

        },
        mounted() {

        }
    }
</script>

<style>
    .el-tag + .el-tag {
        margin-left: 10px;
    }

    .search .el-input__inner {
        width: 300px;
    }

    .el-table {
        overflow: auto;
    }

    .message-title {
        cursor: pointer;
    }

    .handle-row {
        margin-top: 30px;
    }

    .tab-table {
        width: 100%;
        max-height: 500px;
        overflow-y: auto;
    }

    .el-message-box {
        width: 450px;
    }
</style>

