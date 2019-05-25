<template>
    <div class="">
        <div class="crumbs">
        </div>
        <div class="container">

            <div>
                <el-tabs v-model="message">
                    <el-tab-pane :label="`题库`" name="first">
                        <template>
                            <el-form>
                                <el-form-item label="题库名称" :label-width="formLabelWidth">
                                    <el-row>
                                        {{entity.testName}}
                                    </el-row>
                                </el-form-item>
                                <el-form-item label="信号源" :label-width="formLabelWidth">
                                    <el-row>
                                        {{entity.testEntity.signalSource.source}}

                                    </el-row>

                                </el-form-item>

                                <el-form-item label="变量名" :label-width="formLabelWidth">
                                    <el-row v-for="(item,index) in entity.testEntity.variables" :key="index">
                                        <el-tag>
                                            {{item.title}}
                                        </el-tag>
                                        <span>绑定信号为:  <el-tag>{{item.bindSignalName}}</el-tag></span>
                                    </el-row>
                                </el-form-item>
                                <el-form-item label="触发条件" :label-width="formLabelWidth">
                                    <el-row>
                                        <span v-if="entity.testEntity.triggerCondition.conditionItem.key == 1">  手动启动 </span>
                                        <span v-if="entity.testEntity.triggerCondition.conditionItem.key == 2">  {{entity.testEntity.triggerCondition.value}} 点自动启动 </span>
                                        <el-tag v-if="entity.testEntity.triggerCondition.conditionItem.key == 3">
                                            {{entity.testEntity.triggerCondition.variable.title}}
                                        </el-tag>
                                        <span v-if="entity.testEntity.triggerCondition.conditionItem.key == 3"> 等于 {{entity.testEntity.triggerCondition.value}} 之后启动 </span>
                                    </el-row>
                                </el-form-item>
                                <el-form-item label="结束条件" :label-width="formLabelWidth">
                                    <el-row>
                                        <span v-if="entity.testEntity.endCondition.conditionItem.key == 1"> 手动结束 </span>
                                        <span v-if="entity.testEntity.endCondition.conditionItem.key == 2">  {{entity.testEntity.endCondition.value}} 点自动结束 </span>
                                        <el-tag v-if="entity.testEntity.endCondition.conditionItem.key == 3">
                                            {{entity.testEntity.endCondition.variable.title}}
                                        </el-tag>
                                        <span v-if="entity.testEntity.endCondition.conditionItem.key == 3"> 等于 {{entity.testEntity.endCondition.value}}  之后结束 </span>
                                    </el-row>
                                </el-form-item>
                                <el-form-item label="总分" :label-width="formLabelWidth">
                                    <el-row>
                                        {{totalScore}}
                                    </el-row>
                                </el-form-item>
                            </el-form>
                        </template>
                    </el-tab-pane>
                    <el-tab-pane :label="`评分模块`" name="second">
                        <template>
                            <el-table :data="entity.testEntity.modules"
                                      style="width: 100%; max-height:700px; overflow-y:auto;">
                                <el-table-column type="expand">
                                    <template slot-scope="props">
                                        <el-table :data="props.row.moduleEntity.variables"
                                                  style="width: 100%">
                                            <el-table-column label="变量名"
                                                             prop="variableName">
                                            </el-table-column>
                                            <el-table-column label="信号名"
                                                             prop="bindSignalName">
                                            </el-table-column>
                                        </el-table>
                                    </template>
                                </el-table-column>
                                <el-table-column label="评分模块名"
                                                 prop="moduleName">
                                </el-table-column>
                                <el-table-column label="模块分数"
                                                 prop="moduleEntity.moduleScore">
                                </el-table-column>

                            </el-table>
                        </template>
                    </el-tab-pane>
                </el-tabs>
                <div class="handle-row">
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
    import session from '@/store/storage'
    import common from '@/common/common'

    export default {
        name: 'tabs',
        data() {
            return {
                message: 'first',
                fullScreenLoading: false,
                loading: false,
                tableData: [],
                testId: '',
                totalScore:0,
                entity: {
                    testName: '',
                    testEntity: {
                        signalSource: {
                            source: ''
                        },
                        variables: [],
                        triggerCondition: {
                            variable: {
                                variableName: ''
                            },
                            value:'',
                            conditionItem: {}
                        },
                        endCondition: {
                            variable: {
                                variableName: ''
                            },
                            value: '',
                            conditionItem: {}
                        },
                        modules:[],
                    }
                },
                module: {
                    moduleEntity: {
                        variables:[],
                    }
                },
                triggerItems: [
                    {
                        key: '1',
                        value: '某个参数达到设定值之后'
                    },
                    {
                        key: '2',
                        value: '手动启动'
                    },

                ],
                endItems: [
                    {
                        key: '1',
                        value: '某个参数达到设定值之后'
                    },
                    {
                        key: '2',
                        value: '手动结束'
                    },

                ],
                formLabelWidth: '150px',
                servers: [
                    {
                        label: 'server1',
                        value:'1'
                    },
                    {
                        label: 'server2',
                        value: '2'
                    }
                ],
            }
        },
        created() {
            this.testId = session.get("detail_testId");
            if (this.testId != undefined && this.testId != '') {
                this.getData(this.testId);
            } else {
                this.testId = common.Guid();
                this.entity.id = this.testId;
            }
        },
        methods: {

            getData(testId) {
                testService.get({ id: testId }).then((res) => {
                     if (res.data) {
                         this.entity = res.data;
                         this.totalScore = 0;
                         if (this.entity) {
                             if (this.entity.testEntity) {
                                 if (this.entity.testEntity.modules && this.entity.testEntity.modules.length > 0) {
                                     this.entity.testEntity.modules.forEach(c => {
                                         if (c.moduleEntity.moduleScore) {
                                             this.totalScore += c.moduleEntity.moduleScore;
                                         }
                                     })
                                 }
                             }
                         }
                     }
                    console.log(this.entity);
                    console.log(this.totalScore);
                })
            },
            goBackList() {
                this.$router.push('/test');
            },
        },
        computed: {

        }
    }

</script>

<style>
    .el-tag + .el-tag {
        margin-left: 10px;
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

