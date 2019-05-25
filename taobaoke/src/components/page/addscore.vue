<template>
    <div class="">
        <div class="crumbs">
        </div>
        <div class="container">
            <div class="form-box">
                <el-form ref="form">
                    <el-form-item label="评分模块名称">
                        <el-input v-model="module.moduleName" style="width:500px;"></el-input>
                    </el-form-item>
                </el-form>
            </div>
            <div>
                <el-tabs v-model="message">
                    <el-tab-pane :label="`变量(${module.moduleEntity.variables.length})`" name="first">
                        <div class="handle-row" style="float:right;">
                            <el-button type="default" @click="addVariable" title="随机生成变量名">生成变量名</el-button>
                        </div>
                        <el-table :data="module.moduleEntity.variables" border class="tab-table" style="overflow-y: auto;">
                            <el-table-column prop="title" label="名称"></el-table-column>
                            <el-table-column prop="createdDate" width="180" label="创建时间"></el-table-column>
                            <el-table-column width="120" label="操作">
                                <template slot-scope="scope">
                                    <el-button type="danger" @click="handleDelVariable(scope.$index)">删除</el-button>
                                </template>
                            </el-table-column>
                        </el-table>
                    </el-tab-pane>
                    <el-tab-pane :label="`触发条件(${module.moduleEntity.triggerConditions.length})`" name="second" style="overflow-y:auto;max-height:600px;">
                        <el-collapse v-model="triggerConditionActiveNames" style="width:99%">
                            <el-collapse-item title="触发条件" name="1">
                                <template v-if="message === 'second'">
                                    <el-table :data="module.moduleEntity.triggerConditions" border class="tab-table">
                                        <el-table-column label="名称" width="300">
                                            <template slot-scope="scope">
                                                <span>{{scope.row.name}}</span>
                                            </template>
                                        </el-table-column>
                                        <el-table-column width="300" label="条件">
                                            <template slot-scope="scope">
                                                <span>{{scope.row.conditionItemValue}}</span>
                                            </template>
                                        </el-table-column>

                                        <el-table-column width="150" label="变量">
                                            <template slot-scope="scope">
                                                <span>{{scope.row.variableName}}</span>
                                            </template>
                                        </el-table-column>

                                        <el-table-column label="表达式">
                                            <template slot-scope="scope">
                                                <span>{{scope.row.expressionText}}</span>
                                            </template>
                                        </el-table-column>
                                        <el-table-column prop="createdDate" width="150" label="创建时间"></el-table-column>
                                        <el-table-column width="200" label="操作">
                                            <template slot-scope="scope">
                                                <el-button type="primary" @click="editTriggerCondition(scope.$index)">编辑</el-button>
                                                <el-button type="danger" @click="delTriggerCondition(scope.$index)">删除</el-button>
                                            </template>
                                        </el-table-column>
                                    </el-table>

                                </template>
                                <div class="handle-row">
                                    <el-input v-model="triggerCondition.name" placeholder="请输入触发条件名" style="width:150px;"></el-input>

                                    <el-select v-model="triggerCondition.conditionItem" placeholder="选择条件类型">
                                        <el-option v-for="(item,index) in triggerItems" :key="index" :label="item.value" :value="item">
                                        </el-option>
                                    </el-select>


                                    <el-input v-model="triggerCondition.key1SymbolValue1" type="number" v-if="triggerCondition.conditionItem.key == 1" placeholder="请输入值" style="width:150px;" :required="true"></el-input>
                                    <el-select v-model="triggerCondition.key1Symbol1" v-if="triggerCondition.conditionItem.key == 1" placeholder="请选择">
                                        <el-option v-for="(item, index) in symbolItemsGreaterThan" :key="index" :label="item.value" :value="item.key">
                                        </el-option>
                                    </el-select>

                                    <el-select v-model="triggerCondition.variable.id"
                                               v-if="triggerCondition.conditionItem.key == 1 || triggerCondition.conditionItem.key == 2 || triggerCondition.conditionItem.key == 3"
                                               @change="variableChange(1)" placeholder="请选择变量">
                                        <el-option v-for="(item, index) in module.moduleEntity.variables" :key="index" :label="item.title" :value="item.id">
                                        </el-option>
                                    </el-select>
                                    <el-select v-model="triggerCondition.key1Symbol2" v-if="triggerCondition.conditionItem.key == 1" value-key="key" placeholder="请选择">
                                        <el-option v-for="(item, index) in symbolItemsLessThan" :key="item.key" :label="item.value" :value="item.key">
                                        </el-option>
                                    </el-select>
                                    <el-input v-model="triggerCondition.key1SymbolValue2" type="number" v-if="triggerCondition.conditionItem.key == 1" placeholder="请输入值" style="width:150px;"></el-input>

                                    <el-input v-model="triggerCondition.key2Value" type="number" v-if="triggerCondition.conditionItem.key == 2" placeholder="请输入判定值" style="width:150px;"></el-input>

                                    <el-select v-model="triggerCondition.key3Symbol1" v-if="triggerCondition.conditionItem.key == 3" placeholder="请选择">
                                        <el-option v-for="(item, index) in symbolItemsAll" :key="index" :label="item.value" :value="item.key">
                                        </el-option>
                                    </el-select>

                                    <el-input v-model="triggerCondition.key3SymbolValue1" placeholder="请输入值" v-if="triggerCondition.conditionItem.key == 3" type="number" style="width:150px;"></el-input>
                                    <span v-if="triggerCondition.conditionItem.key == 3">，之后</span>
                                    <el-input v-model="triggerCondition.key3Value" v-if="triggerCondition.conditionItem.key == 3" placeholder="请输入时间" type="number" style="width:150px;"></el-input>
                                    <span v-if="triggerCondition.conditionItem.key == 3">秒启动</span>

                                    <el-input v-if="triggerCondition.conditionItem.key == 5" type="textarea"
                                              :autosize="{ minRows: 3, maxRows: 5}"
                                              placeholder="请输入自定义条件"
                                              v-model="triggerCondition.key5Value" style="width:500px">
                                    </el-input>

                                    <el-button type="primary" @click="addTriggerCondition">暂 存</el-button>
                                    <el-button type="primary" @click="cancelSaveTriggerCondition">取 消</el-button>
                                </div>
                                <br />
                                <div v-if="triggerCondition.conditionItem.key == 5">
                                    <span v-if="module.moduleEntity.variables.length>0"><span style="color:red">*</span>自定义规则可使用变量有：</span>
                                    <el-button type="primary" plain v-for="(item, index) in module.moduleEntity.variables" :label="item.title" :key="index" :value="item.variableName">{{item.variableName}}</el-button>
                                    <br />
                                    <span><span style="color:red">*</span>已有变量不需要定义可直接使用，若自定义其他变量，请按照正确的JavaScript语法定义</span>
                                </div>
                            </el-collapse-item>
                            <el-collapse-item title="触发组合条件" name="2" v-if="module.moduleEntity.triggerConditions.length >= 2">
                                <template v-if="message === 'second'">
                                    <el-form>
                                        <el-form-item label="触发条件" :label-width="formLabelWidth">
                                            <el-row>
                                                <el-button type="primary" plain v-for="(item, index) in module.moduleEntity.triggerConditions" @click="selectedCondition(item)" :key="index">{{item.name}}</el-button>
                                            </el-row>

                                        </el-form-item>
                                        <el-form-item label="关系" :label-width="formLabelWidth">
                                            <el-row>
                                                <el-button type="primary" plain v-for="(item, index) in operationRelationships" @click="selectedRelation(item)" :key="index">{{item.title}}</el-button>
                                            </el-row>
                                        </el-form-item>
                                        <el-form-item label="表达式" :label-width="formLabelWidth">
                                            <el-tag :key="index"
                                                    v-for="(item, index) in module.moduleEntity.combinTriggerCondition.expressions"
                                                    closable
                                                    :disable-transitions="false"
                                                    @close="handleDelRelation(item)">
                                                {{item.text}}
                                            </el-tag>
                                        </el-form-item>
                                        <el-form-item label="表达式" :label-width="formLabelWidth">
                                            <span>{{this.module.moduleEntity.combinTriggerCondition.expressionText}}</span>
                                        </el-form-item>
                                        <el-form-item :label-width="formLabelWidth">
                                            <el-row>
                                                <el-button type="primary" @click="saveCombinCondition">暂 存</el-button>
                                            </el-row>
                                        </el-form-item>
                                    </el-form>
                                </template>
                            </el-collapse-item>
                        </el-collapse>
                    </el-tab-pane>
                    <el-tab-pane :label="`结束条件(${module.moduleEntity.endConditions.length})`" name="third" style="overflow-y:auto;max-height:600px;">
                        <el-collapse v-model="endConditionActiveNames" style="width:99%">
                            <el-collapse-item title="结束条件" name="1">
                                <template>
                                    <el-table :data="module.moduleEntity.endConditions" border class="tab-table">
                                        <el-table-column label="名称" width="300">
                                            <template slot-scope="scope">
                                                <span>{{scope.row.name}}</span>
                                            </template>
                                        </el-table-column>
                                        <el-table-column width="300" label="条件">
                                            <template slot-scope="scope">
                                                <span>{{scope.row.conditionItemValue}}</span>
                                            </template>
                                        </el-table-column>

                                        <el-table-column width="150" label="变量">
                                            <template slot-scope="scope">
                                                <span>{{scope.row.variableName}}</span>
                                            </template>
                                        </el-table-column>

                                        <el-table-column label="表达式">
                                            <template slot-scope="scope">
                                                <span>{{scope.row.expressionText}}</span>
                                            </template>
                                        </el-table-column>
                                        <el-table-column prop="createdDate" width="150" label="创建时间"></el-table-column>
                                        <el-table-column width="200" label="操作">
                                            <template slot-scope="scope">
                                                <el-button type="primary" @click="editEndCondition(scope.$index)">编辑</el-button>
                                                <el-button type="danger" @click="delEndCondition(scope.$index)">删除</el-button>
                                            </template>
                                        </el-table-column>
                                    </el-table>

                                </template>
                                <div class="handle-row">
                                    <el-input v-model="endCondition.name" placeholder="请输入结束条件名" style="width:150px;"></el-input>

                                    <el-select v-model="endCondition.conditionItem" placeholder="选择条件类型">
                                        <el-option v-for="(item, index) in endItems" :key="index" :label="item.value" :value="item">
                                        </el-option>
                                    </el-select>

                                    <el-input v-model="endCondition.key1SymbolValue1" type="number" v-if="endCondition.conditionItem.key == 1" placeholder="请输入值" style="width:150px;"></el-input>

                                    <el-select v-model="endCondition.key1Symbol1" v-if="endCondition.conditionItem.key == 1" placeholder="请选择">
                                        <el-option v-for="(item, index) in symbolItemsGreaterThan" :key="index" :label="item.value" :value="item.key">
                                        </el-option>
                                    </el-select>

                                    <el-select v-model="endCondition.variable.id" v-if="endCondition.conditionItem.key == 1 || endCondition.conditionItem.key == 2 || endCondition.conditionItem.key == 3" @change="variableChange(2)" placeholder="请选择变量">
                                        <el-option v-for="(item, index) in module.moduleEntity.variables" :key="index" :label="item.title" :value="item.id">
                                        </el-option>
                                    </el-select>

                                    <el-select v-model="endCondition.key1Symbol2" v-if="endCondition.conditionItem.key == 1" placeholder="请选择">
                                        <el-option v-for="(item, index) in symbolItemsLessThan" :key="index" :label="item.value" :value="item.key">
                                        </el-option>
                                    </el-select>
                                    <el-input v-model="endCondition.key1SymbolValue2" type="number" v-if="endCondition.conditionItem.key == 1" placeholder="请输入值" style="width:150px;"></el-input>
                                    <el-input v-model="endCondition.key2Value" type="number" v-if="endCondition.conditionItem.key == 2" placeholder="请输入判定值" style="width:150px;"></el-input>

                                    <el-select v-model="endCondition.key3Symbol1" v-if="endCondition.conditionItem.key == 3" placeholder="请选择">
                                        <el-option v-for="(item, index) in symbolItemsAll" :key="index" :label="item.value" :value="item.key">
                                        </el-option>
                                    </el-select>
                                    <el-input v-model="endCondition.key3SymbolValue1" type="number" v-if="endCondition.conditionItem.key == 3" style="width:150px;"></el-input>
                                    <span v-if="endCondition.conditionItem.key == 3">之后，</span>
                                    <el-input v-model="endCondition.key3Value" v-if="endCondition.conditionItem.key == 3" type="number" style="width:150px;"></el-input>
                                    <span v-if="endCondition.conditionItem.key == 3">秒结束</span>

                                    <el-input v-if="endCondition.conditionItem.key == 5" type="textarea"
                                              :autosize="{ minRows: 3, maxRows: 5}"
                                              placeholder="请输入自定义条件"
                                              v-model="endCondition.key5Value" style="width:500px">
                                    </el-input>
                                    <el-button type="primary" @click="addEndCondition">暂 存</el-button>
                                    <el-button type="primary" @click="cancelSaveEndCondition">取 消</el-button>
                                </div>
                                <br />
                                <div v-if="endCondition.conditionItem.key == 5">
                                    <span v-if="module.moduleEntity.variables.length>0"><span style="color:red">*</span>自定义规则可使用变量有：</span>
                                    <el-button type="primary" plain v-for="(item, index) in module.moduleEntity.variables" :label="item.title" :key="index" :value="item.variableName">{{item.variableName}}</el-button>
                                    <br />
                                    <span><span style="color:red">*</span>已有变量不需要定义可直接使用，若自定义其他变量，请按照正确的JavaScript语法定义</span>
                                </div>
                            </el-collapse-item>
                            <el-collapse-item title="结束组合条件" name="2" v-if="module.moduleEntity.endConditions.length >= 2">
                                <template>
                                    <el-form>
                                        <el-form-item label="结束条件" :label-width="formLabelWidth">
                                            <el-row>
                                                <el-button type="primary" plain v-for="(item, index) in module.moduleEntity.endConditions" @click="addConditionToEnd(item)" :key="index">{{item.name}}</el-button>
                                            </el-row>

                                        </el-form-item>
                                        <el-form-item label="关系" :label-width="formLabelWidth">
                                            <el-row>
                                                <el-button type="primary" plain v-for="(item, index) in operationRelationships" @click="selectedEndRelation(item)" :key="index">{{item.title}}</el-button>
                                            </el-row>
                                        </el-form-item>
                                        <el-form-item label="表达式" :label-width="formLabelWidth">
                                            <el-tag :key="index"
                                                    v-for="(item, index) in module.moduleEntity.combinEndCondition.expressions"
                                                    closable
                                                    :disable-transitions="false"
                                                    @close="delEndExpress(item)">
                                                {{item.text}}
                                            </el-tag>
                                        </el-form-item>
                                        <el-form-item label="表达式" :label-width="formLabelWidth">
                                            <span>{{this.module.moduleEntity.combinEndCondition.expressionText}}</span>
                                        </el-form-item>
                                        <el-form-item :label-width="formLabelWidth">
                                            <el-row>
                                                <el-button type="primary" @click="saveCombinEndCondition">暂 存</el-button>
                                            </el-row>
                                        </el-form-item>
                                    </el-form>
                                </template>
                            </el-collapse-item>
                        </el-collapse>
                    </el-tab-pane>
                    <el-tab-pane :label="`规则`" name="four" style="overflow-y:auto;max-height:600px;">
                        <el-dialog width="30%" :title="`${isEditScoring == true ? '编辑' : '新增'}区间`" :visible.sync="showScoringDialog" append-to-body @close="showScoringDialog=false">
                            <el-card class="is-always-shadow">
                                <div class="demo-input-suffix">
                                    区间：<el-input v-model="scoringObj.scoringSection" type="number" placeholder="请输入区间" style="width:150px;"></el-input>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    计分：<el-input v-model="scoringObj.scoringValue" type="number" placeholder="请输入计分值" style="width:150px;"></el-input>
                                </div>
                            </el-card>
                            <div slot="footer" class="dialog-footer">
                                <el-button @click="showScoringDialog=false">取 消</el-button>
                                <el-button type="primary" @click="saveScoringDialog">确 定</el-button>
                            </div>
                        </el-dialog>
                        <el-form>
                            <el-form-item></el-form-item>
                            <el-form-item>
                                <el-row>
                                    <el-col :span="8">
                                        <div>
                                            &nbsp;规则名称: &nbsp;
                                            <el-input v-model="module.moduleEntity.rule.ruleName" placeholder="请输入规则名" auto-complete="off" style="width:180px;"></el-input>
                                        </div>
                                    </el-col>
                                    <el-col :span="8">
                                        <div>
                                            规则类型: &nbsp;
                                            <el-select v-model="module.moduleEntity.rule.ruleType" clearable placeholder="选择类型">
                                                <el-option v-for="(item, index) in ruleTypeArray" :key="index" :label="item.label" :value="item.value"></el-option>
                                            </el-select>
                                        </div>
                                    </el-col>
                                    <el-col :span="8">
                                        <div>
                                            总分: &nbsp;
                                            <el-input v-model="module.moduleEntity.rule.ruleTotalScore" type="number" placeholder="请输入总分" auto-complete="off" style="width:180px;"></el-input>
                                        </div>
                                    </el-col>
                                </el-row>
                            </el-form-item>
                            <el-form-item v-show="module.moduleEntity.rule.ruleType==1 || module.moduleEntity.rule.ruleType==4 || module.moduleEntity.rule.ruleType==5">
                                <el-card class="is-always-shadow">
                                    <span>选择变量: </span>
                                    <el-button type="primary" v-if="module.moduleEntity.variables.length>0" plain v-for="(item, index) in module.moduleEntity.variables" @click="addInExpression(item.variableName)" :label="item.title" :key="index" :value="item.variableName">{{item.title}}</el-button>
                                    <span v-if="module.moduleEntity.variables.length == 0" style="color:red">请先生成变量</span>
                                </el-card>
                            </el-form-item>
                            <el-form-item v-show="module.moduleEntity.rule.ruleType==1 || module.moduleEntity.rule.ruleType==4 || module.moduleEntity.rule.ruleType==5">
                                <el-card class="is-always-shadow">
                                    <span>选择运算符: </span>
                                    <el-button type="primary" plain v-for="(item, index) in operationSymbols" @click="addInExpression(item.value)" :label="item.title" :key="index" :value="item.value">{{item.title}}</el-button>
                                </el-card>
                            </el-form-item>
                            <el-form-item v-show="module.moduleEntity.rule.ruleType==1 || module.moduleEntity.rule.ruleType==4 || module.moduleEntity.rule.ruleType==5">
                                <el-card class="is-always-shadow">
                                    <span>快捷设置1: </span>
                                    <el-input v-model="quickSetting1.firstValue" type="number" style="width:150px;"></el-input>
                                    <el-select v-model="quickSetting1.firstSymbol">
                                        <el-option v-for="(item,index) in symbolItemsGreaterThan" :key="index" :label="item.value" :value="item.key"></el-option>
                                    </el-select>
                                    <el-select v-model="quickSetting1.centerVariable" placeholder="选择变量">
                                        <el-option v-for="(item, index) in module.moduleEntity.variables" :key="index" :label="item.title" :value="item.variableName"></el-option>
                                    </el-select>
                                    <el-select v-model="quickSetting1.secondSymbol">
                                        <el-option v-for="(item,index) in symbolItemsLessThan" :key="index" :label="item.value" :value="item.key"></el-option>
                                    </el-select>
                                    <el-input v-model="quickSetting1.secondValue" type="number" style="width:150px;"></el-input>
                                    &nbsp;&nbsp;&nbsp;<el-button type="primary" circle icon="el-icon-plus" @click="addQuickSetting(1)" title="添加快捷设置到表达式"></el-button>
                                </el-card>
                            </el-form-item>
                            <el-form-item v-show="module.moduleEntity.rule.ruleType==1 || module.moduleEntity.rule.ruleType==4 || module.moduleEntity.rule.ruleType==5">
                                <el-card class="is-always-shadow">
                                    <span>快捷设置2: </span>
                                    <el-select v-model="quickSetting2.centerVariable" placeholder="选择变量">
                                        <el-option v-for="(item, index) in module.moduleEntity.variables" :key="index" :label="item.title" :value="item.variableName"></el-option>
                                    </el-select>
                                    <el-select v-model="quickSetting2.firstSymbol">
                                        <el-option v-for="(item,index) in symbolItemsGreaterThan" :key="index" :label="item.value" :value="item.key"></el-option>
                                    </el-select>
                                    <el-input v-model="quickSetting2.firstValue" type="number" style="width:150px;"></el-input>
                                    <el-tag style="margin-left:15px;">&nbsp;或&nbsp;</el-tag>&nbsp;&nbsp;&nbsp;
                                    <el-select v-model="quickSetting2.secondSymbol">
                                        <el-option v-for="(item,index) in symbolItemsLessThan" :key="index" :label="item.value" :value="item.key"></el-option>
                                    </el-select>
                                    <el-input v-model="quickSetting2.secondValue" type="number" style="width:150px;"></el-input>
                                    &nbsp;&nbsp;&nbsp;<el-button type="primary" circle icon="el-icon-plus" @click="addQuickSetting(2)" title="添加快捷设置到表达式"></el-button>
                                </el-card>
                            </el-form-item>
                            <el-form-item v-show="module.moduleEntity.rule.ruleType==1 || module.moduleEntity.rule.ruleType==4 || module.moduleEntity.rule.ruleType==5">
                                <el-card class="is-always-shadow">
                                    <span>快捷设置3: </span>
                                    <el-select v-model="quickSetting3.centerVariable" placeholder="选择变量">
                                        <el-option v-for="(item, index) in module.moduleEntity.variables" :key="index" :label="item.title" :value="item.variableName"></el-option>
                                    </el-select>
                                    <el-select v-model="quickSetting3.firstSymbol">
                                        <el-option v-for="(item,index) in symbolItemsAll" :key="index" :label="item.value" :value="item.key"></el-option>
                                    </el-select>
                                    <el-input v-model="quickSetting3.firstValue" type="number" style="width:150px;"></el-input>
                                    &nbsp;&nbsp;&nbsp;<el-button type="primary" circle icon="el-icon-plus" @click="addQuickSetting(3)" title="添加快捷设置到表达式"></el-button>
                                </el-card>
                            </el-form-item>

                            <el-form-item v-show="module.moduleEntity.rule.ruleType == 1">
                                <!--变量维持-->
                                <el-card class="is-always-shadow">
                                    <el-input type="textarea" :rows="3" v-model="module.moduleEntity.rule.variableKeepExpression" placeholder="请输入内容或选择变量和运算符" style="width:600px;">
                                    </el-input>
                                    &nbsp;&nbsp;时，每&nbsp;&nbsp;
                                    <el-input v-model="module.moduleEntity.rule.variableKeepSeconds" type="number" style="width:150px;"></el-input>
                                    &nbsp;&nbsp;秒，扣&nbsp;&nbsp;
                                    <el-input v-model="module.moduleEntity.rule.variableKeepScores" type="number" style="width:150px;"></el-input>
                                    &nbsp;&nbsp;分
                                    &nbsp;&nbsp;<el-button type="primary" @click="saveModuleRule">暂 存</el-button>
                                </el-card>
                            </el-form-item>
                            <el-form-item v-show="module.moduleEntity.rule.ruleType == 2">
                                <!--变量检查-->
                                <el-card class="is-always-shadow">
                                    <el-select v-model="module.moduleEntity.rule.variableCheckName" placeholder="选择变量">
                                        <el-option v-for="(item,index) in module.moduleEntity.variables" :key="index" :label="item.title" :value="item.variableName"></el-option>
                                    </el-select>
                                    &nbsp;&nbsp; = &nbsp;&nbsp;
                                    <el-select v-model="module.moduleEntity.rule.variableCheckResult" placeholder="选择检查结果">
                                        <el-option v-for="(item,index) in checkValues" :key="index" :label="item.value" :value="item.key"></el-option>
                                    </el-select>
                                    &nbsp;&nbsp;<el-button type="primary" @click="saveModuleRule">暂 存</el-button>
                                </el-card>
                            </el-form-item>
                            <el-form-item v-show="module.moduleEntity.rule.ruleType == 3">
                                <!--按时间取值检查-->
                                <el-card class="is-always-shadow">
                                    每&nbsp;<el-input placeholder="输入时间" v-model="module.moduleEntity.rule.checkByTimeSeconds" type="number" style="width:150px;"></el-input>&nbsp;秒，取&nbsp;
                                    <el-select v-model="module.moduleEntity.rule.checkByTimeVariableName" placeholder="选择变量" @change="checkByTimeVariableChange()">
                                        <el-option v-for="(item,index) in module.moduleEntity.variables" :key="index" :label="item.title" :value="item.variableName"></el-option>
                                    </el-select>
                                    &nbsp;与标准组&nbsp;
                                    <el-input v-model="module.moduleEntity.rule.checkByTimeCompareArray" type="textarea" :rows="2" placeholder="输入标准组 格式为 1,2,3,4...8,9,10 且必须由数字组成标准组" style="width:500px;"></el-input>
                                    &nbsp;进行比较，到评分结束。
                                    <br /><br />
                                    允许最大偏差为&nbsp;<el-input v-model="module.moduleEntity.rule.checkByTimeMaxDeviation" type="number" placeholder="输入偏差" style="width:150px;"></el-input>
                                    &nbsp;，若在最大偏差范围内得分，否则使用系数&nbsp;
                                    <el-input v-model="module.moduleEntity.rule.checkByTimeCalcFormula" type="number" placeholder="输入计算系数" style="width:150px;"></el-input>
                                    &nbsp;计算得分。
                                    <!--<el-input v-model="module.moduleEntity.rule.checkByTimeCalcFormula" type="textarea" :rows="2" placeholder="输入计算公式" style="width:500px;"></el-input>&nbsp;公式计算评分。-->
                                    &nbsp;&nbsp;<el-button type="primary" @click="saveModuleRule">暂 存</el-button>
                                </el-card>
                            </el-form-item>
                            <el-form-item v-show="module.moduleEntity.rule.ruleType == 4">
                                <!--取值公式检查-->
                                <el-card class="is-always-shadow">
                                    <el-input v-model="module.moduleEntity.rule.checkByFormula" type="textarea" :rows="3" placeholder="请输入内容或选择变量和运算符" style="width:600px;"></el-input>
                                    &nbsp;&nbsp;时计0分，否则计满分。
                                    &nbsp;&nbsp;<el-button type="primary" @click="saveModuleRule">暂 存</el-button>
                                </el-card>
                            </el-form-item>
                            <el-form-item v-show="module.moduleEntity.rule.ruleType == 5">
                                <!--自定义规则-->
                                <el-card class="is-always-shadow">
                                    <el-input v-model="module.moduleEntity.rule.customExpression" type="textarea" :rows="4" placeholder="请输入内容或选择变量和运算符" style="width:900px;">
                                    </el-input>
                                    &nbsp;&nbsp;<el-button type="primary" @click="saveModuleRule">暂 存</el-button>
                                </el-card>
                            </el-form-item>
                        </el-form>
                    </el-tab-pane>
                </el-tabs>
                <div class="handle-row">
                    <el-button type="primary" @click="saveAll" v-loading.fullscreen.lock="fullScreenLoading">保存全部</el-button>
                    <el-button type="primary" @click="goBackList">返回列表</el-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { formatDate } from "@/filters/date"
    import $ from 'jquery'
    import moduleService from '@/server/moduleService'
    import session from '@/store/storage'
    import common from '@/common/common'
import { isNumber } from "util";
    export default {
        name: 'tabs',
        data() {
            return {
                message: 'first',
                fullScreenLoading: false,
                moduleId: '',
                module: {
                    id: '',
                    moduleName: '',
                    createdDate: '',
                    updatedDate: '',
                    isDelete: false,
                    moduleEntity: {
                        isStartScoring: false,
                        isEndScoring: false,
                        scoringTimer: 0,
                        moduleScore:0,
                        variables: [],
                        triggerConditions: [],
                        combinTriggerCondition: {
                            id: '',
                            moduleId: '',
                            name: '',
                            expressions: [],
                            expressionText: '',
                        },
                        endConditions: [],
                        combinEndCondition: {
                            id: '',
                            moduleId: '',
                            name: '',
                            expressions: [],
                            expressionText: '',
                        },
                        rule: {
                            moduleId: '',
                            ruleName: '',
                            ruleType: '',
                            ruleTotalScore: '',
                            variableKeepExpression: '',
                            variableKeepSeconds: '',
                            variableKeepScores: '',
                            checkVariableId: '',
                            checkVariableTitle: '',
                            checkVariableName: '',
                            variableCheckResult: 0,
                            checkByTimeSeconds: '',
                            checkByTimeCompareArray: '',
                            checkByTimeMaxDeviation: '',
                            checkByTimeCalcFormula: '',
                            checkByFormula: '',
                            customExpression: '',
                        },
                    },
                },
                formLabelWidth: '150px',
                operationRelationships: [
                    {
                        id: '1',
                        key: '1',
                        value: '&&',
                        title: '且',
                        type: 1

                    },
                    {
                        id: '2',
                        key: '2',
                        value: '||',
                        title: '或',
                        type: 1
                    },
                    {
                        id: '3',
                        key: '3',
                        value: '^',//相同取0，相异取1
                        title: '异或',
                        type: 1
                    },
                    {
                        id: '4',
                        key: '4',
                        value: '(',
                        title: '(',
                        type: 2
                    },
                    {
                        id: '5',
                        key: '5',
                        value: ')',
                        title: ')',
                        type: 2
                    }
                ],
                operationSymbols: [
                    {
                        key: '1',
                        value: '+',
                        title: '加'
                    }, {
                        key: '2',
                        value: '-',
                        title: '减'
                    }, {
                        key: '3',
                        value: '*',//相同取0，相异取1
                        title: '乘'
                    }, {
                        key: '4',
                        value: '/',
                        title: '除'
                    }, {
                        key: '5',
                        value: '>',
                        title: '大于'
                    }, {
                        key: '6',
                        value: '>=',
                        title: '大于等于'
                    }, {
                        key: '7',
                        value: '==',
                        title: '等于'
                    }, {
                        key: '8',
                        value: '!=',
                        title: '不等于'
                    }, {
                        key: '9',
                        value: '<',
                        title: '小于'
                    }, {
                        key: '10',
                        value: '<=',
                        title: '小于等于'
                    }, {
                        key: '11',
                        value: '&&',
                        title: '且'
                    }, {
                        key: '12',
                        value: '||',
                        title: '或'
                    }, {
                        key: '13',
                        value: '^',//相同取0，相异取1
                        title: '异或'
                    }, {
                        key: '14',
                        value: '(',
                        title: '('
                    }, {
                        key: '15',
                        value: ')',
                        title: ')'
                    }
                ],
                quickSetting1: {
                    firstValue: '',
                    firstSymbol: '>=',
                    centerVariable: '',
                    secondValue: '',
                    secondSymbol: '<='
                },
                quickSetting2: {
                    firstValue: '',
                    firstSymbol: '>=',
                    centerVariable: '',
                    secondValue: '',
                    secondSymbol: '<='
                },
                quickSetting3: {
                    firstValue: '',
                    firstSymbol: '>=',
                    centerVariable: ''
                },
                triggerConditionActiveNames: ['1', '2'],
                triggerCondition: {
                    id: '',
                    moduleId: '',
                    type: 1,
                    conditionItem: {
                        key: '',
                        value: '',
                    },
                    expressions: [],
                    expressionText: '',
                    name: '',
                    title: '',
                    value: '',
                    createdDate: '',
                    conditionItemValue: '',
                    variable: {
                        variableName: '',
                        id: ''
                    },
                    variableName: '',
                    variableId: '',
                },
                checkValues: [
                    {
                        key: 1,
                        value: 'true'
                    }, {
                        key: 0,
                        value: 'false'
                    }
                ],
                symbolItemsGreaterThan: [
                    {
                        key: '>',
                        value: '大于'
                    },
                    {
                        key: '>=',
                        value: '大于等于'
                    },
                ],
                symbolItemsLessThan: [
                    {
                        key: '<',
                        value: '小于'
                    },
                    {
                        key: '<=',
                        value: '小于等于'
                    },
                ],
                symbolItemsAll: [
                    {
                        key: '<',
                        value: '小于'
                    },
                    {
                        key: '<=',
                        value: '小于等于'
                    },
                    {
                        key: '>',
                        value: '大于'
                    },
                    {
                        key: '>=',
                        value: '大于等于'
                    },
                    {
                        key: '==',
                        value: '等于'
                    },
                    {
                        key: '!=',
                        value: '不等于'
                    }
                ],
                triggerItems: [
                    {
                        key: '1',
                        value: '某个参数达到设定的范围后'
                    },
                    {
                        key: '2',
                        value: '某个开关信号达到设定的开关状态后'
                    },
                    {
                        key: '3',
                        value: '操作时间达到某个节点后'
                    }, {
                        key: '4',
                        value: '手动启动'
                    },
                    {
                        key: '5',
                        value: '自定义条件'
                    }
                ],
                endConditionActiveNames: ['1', '2'],
                endCondition: {
                    id: '',
                    moduleId: '',
                    type: 2,
                    conditionItem: {
                        key: '',
                        value: ''
                    },
                    expressions: [],
                    expressionText: '',
                    name: '',
                    title: '',
                    value: '',
                    createdDate: '',
                    conditionItemValue: '',
                    variable: {
                        variableName: '',
                        id: ''
                    },
                    variableName: '',
                    variableId: '',
                },
                endItems: [
                    {
                        key: '1',
                        value: '某个参数达到设定的范围后'
                    },
                    {
                        key: '2',
                        value: '某个开关信号达到设定的开关状态后'
                    },
                    {
                        key: '3',
                        value: '操作时间达到某个节点后'
                    },
                    {
                        key: '4',
                        value: '手动停止'
                    },
                    {
                        key: '5',
                        value: '自定义条件'
                    }
                ],
                ruleTypeArray: [{
                    label: '变量维持', value: '1'
                }, {
                    label: '变量检查', value: '2'
                }, {
                    label: '按时间取值检查', value: '3'
                }, {
                    label: '取值公式检查', value: '4'
                }, {
                    label: '自定义规则', value: '5'
                }],
                showScoringDialog: false,
                isEditScoring: false,
                editScoringIndex: -1,
                scoringDialogType: '',
                scoringObj: {
                    scoringSection: '',
                    scoringValue: ''
                },
                tableDataTimeTake: [],
                tableDataTakeValue: []
            }
        },
        created() {
            this.moduleId = session.get("edit_moduleId");
            if (this.moduleId != undefined && this.moduleId != '') {
                this.getData(this.moduleId);
            } else {
                this.moduleId = common.Guid();
            }
        },
        methods: {
            getData(moduleId) {
                moduleService.get({ Id: moduleId }).then((res) => {
                    if (res && res.statusText == "OK") {
                        if (res.data) {
                            this.module = res.data;
                        }
                    }
                })
            },
            goBackList() {
                this.$router.push('/score');
            },
            saveAll() {
                if (this.checkModule(this.module)) {
                    this.module.moduleEntity.rule.moduleId = this.moduleId;
                    this.module.moduleEntity.rule.createdDate = common.getNowFormatDate();
                    this.module.moduleEntity.moduleScore = this.module.moduleEntity.rule.ruleTotalScore;

                    this.module.id = this.moduleId;
                    this.fullScreenLoading = true;

                    moduleService.post(this.module).then((res) => {
                        this.fullScreenLoading = false;
                        if (res && res.data) {
                            this.$notify({
                                type: 'success',
                                title: '成功',
                                message: '保存成功'
                            });
                            this.$router.push('/score');
                        } else {
                            this.$notify.error({
                                title: '错误',
                                message: "数据库访问错误，请检查网络"
                            });
                        }
                    })
                }
            },

            randomWord(randomFlag, source, min, max) {
                var str = "",
                    range = min,
                    arr = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

                range = (source != null && source != undefined && source.length > 0) ? source.length : 0;

                str = arr[range];

                if (this.checkVariable(source, str)) {
                    str = this.randomWord(randomFlag, source, min, max);
                }

                return str;

                //// 随机产生
                //if (randomFlag) {
                //    range = Math.round(Math.random() * (max - min)) + min;
                //}
                //for (var i = 0; i < range; i++) {
                //    var pos = Math.round(Math.random() * (arr.length - 1));
                //    str += arr[pos];
                //}
                //if (this.checkVariable(source, str)) {
                //    str = this.randomWord(randomFlag, source, min, max);
                //}
                //return str;
            },
            addVariable() {
                var length = 1;
                if (this.module.moduleEntity.variables.length >= 26) return;

                var name = this.randomWord(false, this.module.moduleEntity.variables, length);
                this.module.moduleEntity.variables.push({ id: common.Guid(), moduleId: this.moduleId, title: '变量' + name, variableName: name, isDelete: 0, createdDate: common.getNowFormatDate(), isChecked: false });
            },
            checkVariable(vars, name) {
                var result = false;
                if (vars != null && vars.length > 0) {
                    for (var i = 0; i < vars.length; i++) {
                        if (vars[i].variableName === name) {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            },
            setConditionText(condition) {
                condition.expressionText = '';
                if (condition.conditionItem.key == 1) {
                    condition.expressionText += condition.variable.variableName + " " + condition.key1Symbol1 + " " + condition.key1SymbolValue1;
                    condition.expressionText += " && " + condition.variable.variableName + " " + condition.key1Symbol2 + " " + condition.key1SymbolValue2;
                } else if (condition.conditionItem.key == 2) {
                    condition.expressionText = condition.variable.variableName + " == " + condition.key2Value;
                }
                else if (condition.conditionItem.key == 3) {
                    //condition.expressionText = condition.key3Value;
                    condition.expressionText = condition.variable.variableName + " " + condition.key3Symbol1 + " " + condition.key3SymbolValue1;
                } else if (condition.conditionItem.key == 4) {
                    condition.expressionText = condition.key4Value;
                }
                else if (condition.conditionItem.key == 5) {
                    condition.expressionText = condition.key5Value;
                }
            },
            reject(message) {
                this.$notify.error({
                    title: '错误',
                    message: message
                });

                return false;
            },
            checkCondition(condtion, module, type) {
                var text = (type == "begin") ? '触发条件' : '结束条件';                

                if (!common.valueIsNotNullOrEmpty(condtion.name)) {
                    return this.reject("请输入" + text + "名称");
                }

                if (type == "begin") {
                    if (this.module.moduleEntity.triggerConditions.filter(c => c.name == condtion.name).length > 0 && condtion.id == "") {
                        return this.reject(text + condtion.name + " 已存在，请重新输入");
                    }
                } else {
                    if (this.module.moduleEntity.endConditions.filter(c => c.name == condtion.name).length > 0 && condtion.id == "") {
                        return this.reject(text + condtion.name + " 已存在，请重新输入");
                    }
                }

                if (!common.valueIsNotNullOrEmpty(condtion.conditionItem.key)) {
                    return this.reject("请选择" + text + "类型");
                }

                if (condtion.conditionItem.key == 1) {
                    if (!common.valueIsNotNullOrEmpty(condtion.key1SymbolValue1)) {
                        return this.reject("请输入" + text + "范围判定值1");
                    }
                    if (!common.valueIsNotNullOrEmpty(condtion.key1Symbol1)) {
                        return this.reject("请选择" + text + "范围符号1");
                    }
                    if (!common.valueIsNotNullOrEmpty(condtion.variable.id)) {
                        return this.reject("请选择" + text + "范围变量");
                    }
                    if (!common.valueIsNotNullOrEmpty(condtion.key1Symbol2)) {
                        return this.reject("请选择" + text + "符号范围2");
                    }
                    if (!common.valueIsNotNullOrEmpty(condtion.key1SymbolValue2)) {
                        return this.reject("请输入" + text + "范围判定值2");
                    }
                } else if (condtion.conditionItem.key == 2) {
                    if (!common.valueIsNotNullOrEmpty(condtion.variable.id)) {
                        return this.reject("请选择" + text + "开关状态变量");
                    }
                    if (!common.valueIsNotNullOrEmpty(condtion.key2Value)) {
                        return this.reject("请输入" + text + "开关状态判定值");
                    }
                } else if (condtion.conditionItem.key == 3) {
                    if (!common.valueIsNotNullOrEmpty(condtion.variable.id)) {
                        return this.reject("请选择" + text + "操作节点变量");
                    }
                    if (!common.valueIsNotNullOrEmpty(condtion.key3Symbol1)) {
                        return this.reject("请选择" + text + "操作节点判定符号");
                    }
                    if (!common.valueIsNotNullOrEmpty(condtion.key3SymbolValue1)) {
                        return this.reject("请选择" + text + "操作节点判定值");
                    }
                    if (!common.valueIsNotNullOrEmpty(condtion.key3Value)) {
                        return this.reject("请选择" + text + "操作节点判定时间");
                    }
                } else if (condtion.conditionItem.key == 4) {
                    condtion.key4Value = true;
                } else if (condtion.conditionItem.key == 5) {
                    if (!common.valueIsNotNullOrEmpty(condtion.key5Value)) {
                        return this.reject("请输入自定义表达式");
                    }
                    var tempvalue = condtion.key5Value;
                    if (!common.checkJs(module.moduleEntity.variables, tempvalue)) {
                        return this.reject("自定义" + text + "不合法，请重新输入");
                    }

                    condtion.key5Value = tempvalue;
                }

                condtion.conditionItemValue = condtion.conditionItem.value;

                if (condtion.variable != null && condtion.variable != undefined) {
                    condtion.variableName = condtion.variable.variableName;
                }

                return true;
            },
            cancelSaveTriggerCondition() {
                this.triggerCondition = {
                    id: '',
                    moduleId: '',
                    type: 1,
                    conditionItem: {
                        key: '',
                        value: '',
                    },
                    expressions: [],
                    expressionText: '',
                    name: '',
                    title: '',
                    value: '',
                    createdDate: '',
                    conditionItemValue: '',
                    variable: {
                        variableName: '',
                        id: ''
                    },
                    variableName: '',
                    variableId: '',
                };
            },
            addTriggerCondition() {

                if (this.checkCondition(this.triggerCondition, this.module, "begin")) {
                    if (this.triggerCondition.id != '') {
                        var index = common.getFirstItemIndex(this.module.moduleEntity.triggerConditions, this.triggerCondition.id);
                        if (index >= 0) {
                            this.setConditionText(this.triggerCondition);
                            this.module.moduleEntity.triggerConditions.splice(index, 1, $.extend(true, {}, this.triggerCondition));
                        }
                    } else {
                        this.triggerCondition.id = common.Guid();
                        this.triggerCondition.moduleId = this.moduleId;
                        this.triggerCondition.createdDate = common.getNowFormatDate();

                        this.triggerCondition.variableId = this.triggerCondition.variable.id;
                        this.triggerCondition.type = 1;
                        this.setConditionText(this.triggerCondition);

                        this.module.moduleEntity.triggerConditions.push($.extend(true, {}, this.triggerCondition));
                    }

                    this.triggerCondition.id = '';
                    this.triggerCondition.name = '';
                    this.triggerCondition.conditionItem = {};
                    this.triggerCondition.key1SymbolValue1 = '';
                    this.triggerCondition.key1Symbol1 = '';
                    this.triggerCondition.variableId = '';
                    this.triggerCondition.variable = {};
                    this.triggerCondition.variableName = '';
                    this.triggerCondition.key1Symbol2 = '';
                    this.triggerCondition.key1SymbolValue2 = '';
                    this.triggerCondition.key2Value = '';
                    this.triggerCondition.key3Value = '';
                    this.triggerCondition.key5Value = '';
                    this.$message({
                        type: 'success',
                        message: '保存成功,请记得点击保存全部按钮保存到数据库'
                    });
                }
            },

            checkSaveCombin(expressions) {
                let left = 0, right = 0;
                let msg = '';
                let result = true;
                expressions.forEach(e => {
                    if (e.type == 2 && e.text === '(') {
                        left += 1;
                    } else if (e.type == 2 && e.text === ')') {
                        right += 1;
                    }
                });
                if (left != right) {
                    msg = '括号没有成对';
                    result = false;
                }
                var first = expressions[0];
                var last = expressions[expressions.length - 1];
                if (first.type == 1 || first.text === ')' || last.type == 1 || last.text === '(') {
                    msg = '起始位置错误';
                    result = false;
                }

                if (result == false) {
                    this.$notify.error({
                        title: '错误',
                        message: msg
                    });
                }
                return result;
            },
            checkExpression(expressions, item) {
                var msg = '';
                var result = true;

                if (expressions != null && expressions.length == 0 && (item.type == 1 || item.text === ')')) {
                    msg = '请先选择条件';
                    result = false;
                } else if (expressions.length > 0) {
                    let left = 0, right = 0, typezero = 0;
                    expressions.forEach(e => {
                        if (e.type == 2 && e.text === '(') {
                            left += 1;
                        } else if (e.type == 2 && e.text === ')') {
                            right += 1;
                        } else if (e.type == 0) {
                            typezero += 1;
                        }
                    });

                    var temp = expressions[expressions.length - 1];
                    if (item.type == 1 && typezero == 0) {
                        msg = '请先选择触发条件';
                        result = false;
                    }
                    else if (temp.type == 0 && (item.type == 0 || item.text === '(')) {//type=0 触发条件
                        msg = '请先选择关系';
                        result = false;
                    }
                    else if (temp.type == 1) {
                        if (item.type == 1) {
                            msg = '请先选择条件';
                            result = false;
                        }
                    }
                    else if (temp.type == 2) {
                        if (temp.text === '(' && (item.type == 1 || item.text === ')')) {
                            msg = '括号错误';
                            result = false;
                        }
                        else if (temp.text === ')' && (item.type == 2 && item.text === '(' || item.type == 0)) {
                            msg = '括号错误';
                            result = false;
                        } else if (left <= right && item.text === ')') {
                            msg = '括号错误';
                            result = false;
                        }
                    }
                }
                if (result == false) {
                    this.$notify.error({
                        title: '错误',
                        message: msg
                    });
                }

                return result;
            },
            selectedCondition(param) {
                let item = { id: common.Guid(), text: param.name, typeId: param.Id, expressionText: param.expressionText, moduleId: this.moduleId, type: 0, key: param.conditionItem.key };
                console.log("bbbbbbbbb");
                console.log(item);
                if (this.module.moduleEntity.combinTriggerCondition.expressions == null) this.module.moduleEntity.combinTriggerCondition.expressions = [];
                if (this.checkExpression(this.module.moduleEntity.combinTriggerCondition.expressions, item)) {
                    this.module.moduleEntity.combinTriggerCondition.expressions.push(item);
                    this.setExpressionText(this.module.moduleEntity.combinTriggerCondition);
                }
            },

            selectedRelation(item) {
                let temp = { id: common.Guid(), text: item.title, expressionText: item.value, typeId: item.Id, moduleId: this.moduleId, type: item.type, key: 0 };
                console.log("aaaaaaaaa");
                console.log(temp);
                if (this.checkExpression(this.module.moduleEntity.combinTriggerCondition.expressions, temp)) {
                    this.module.moduleEntity.combinTriggerCondition.expressions.push(temp);
                    this.setExpressionText(this.module.moduleEntity.combinTriggerCondition);
                }

            },

            handleDelRelation(item) {
                this.module.moduleEntity.combinTriggerCondition.expressions.splice(this.module.moduleEntity.combinTriggerCondition.expressions.indexOf(item), 1);
                this.setExpressionText(this.module.moduleEntity.combinTriggerCondition);
            },
            setExpressionText(condition) {
                condition.expressionText = '';
                if (condition.expressions.length > 0) {
                    condition.expressions.forEach(e => {
                        if (e.type == 0) {
                            condition.expressionText += "(" + e.expressionText + ")";
                        } else {
                            condition.expressionText += e.expressionText;
                        }
                    });
                }
            },

            saveCombinCondition() {
                if (this.module.moduleEntity.combinTriggerCondition.name == '') {
                    this.module.moduleEntity.combinTriggerCondition.name = "触发组合条件";
                }
                if (this.module.moduleEntity.combinTriggerCondition.id == '')
                    this.module.moduleEntity.combinTriggerCondition.id = common.Guid();

                if (this.module.moduleEntity.combinTriggerCondition.expressions.length == 0) {
                    return this.reject("请选择触发条件并合并为组合条件");
                }

                if (this.checkSaveCombin(this.module.moduleEntity.combinTriggerCondition.expressions)) {
                    if (common.checkJs(this.module.moduleEntity.variables, this.module.moduleEntity.combinTriggerCondition.expressionText)) {
                        this.module.moduleEntity.combinTriggerCondition.createdDate = common.getNowFormatDate();
                        this.module.moduleEntity.combinTriggerCondition.moduleId = this.moduleId;
                        this.$message({
                            type: 'success',
                            message: '确认成功,请记得点击保存全部按钮保存到数据库'
                        });
                    } else {
                        this.reject("触发组合条件表达式不合法，请检查");
                    }
                }
            },

            handleDelVariable(index) {
                let selectVariable = this.module.moduleEntity.variables[index].variableName;
                var variableIsUsing = false;
                if (this.module.moduleEntity.rule.variableKeepExpression != null && this.module.moduleEntity.rule.variableKeepExpression.indexOf(selectVariable) >= 0) variableIsUsing = true;
                else if (this.module.moduleEntity.rule.variableCheckName == selectVariable) variableIsUsing = true;
                else if (this.module.moduleEntity.rule.checkByTimeVariableName == selectVariable) variableIsUsing = true;
                else if (this.module.moduleEntity.rule.customExpression != null && this.module.moduleEntity.rule.customExpression.indexOf(selectVariable) >= 0) variableIsUsing = true;

                if (!variableIsUsing) {
                    this.module.moduleEntity.triggerConditions.forEach(c => {
                        if (c.variable.variableName == selectVariable) {
                            variableIsUsing = true;
                        }
                    });
                }

                if (!variableIsUsing) {
                    if (this.module.moduleEntity.combinTriggerCondition.ExpressionText != null && this.module.moduleEntity.combinTriggerCondition.ExpressionText.indexOf(selectVariable) >= 0) {
                        variableIsUsing = true;
                    }
                }

                if (!variableIsUsing) {
                    this.module.moduleEntity.endConditions.forEach(c => {
                        if (c.variable.variableName == selectVariable) {
                            variableIsUsing = true;
                        }
                    });
                }

                if (!variableIsUsing) {
                    if (this.module.moduleEntity.combinEndCondition.ExpressionText != null && this.module.moduleEntity.combinEndCondition.ExpressionText.indexOf(selectVariable) >= 0) {
                        variableIsUsing = true;
                    }
                }

                if (variableIsUsing) {
                    this.$notify.error({
                        title: '错误',
                        message: "变量" + selectVariable + "在其他地方有引用，不可删除，请先删除引用。"
                    });
                    return;
                }
                this.$confirm('此操作将删除引用变量的所有项, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    var item = this.module.moduleEntity.variables.splice(index, 1)[0];
                    this.removeTriggerCondition(item);
                    this.removeEndCondition(item);
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

            editTriggerCondition(index) {
                var item = this.module.moduleEntity.triggerConditions[index];
                this.triggerCondition = $.extend(true, {}, item);
            },

            delTriggerCondition(index) {
                this.$confirm('此操作将删除引用该触发条件的所有组合条件, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {

                    var item = this.module.moduleEntity.triggerConditions.splice(index, 1);
                    this.removeTriggerCombin(item[0]);

                    this.module.moduleEntity.endConditions = item.concat(this.module.moduleEntity.endConditions);

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
            removeTriggerCondition(item) {
                if (this.module.moduleEntity.triggerConditions.length > 0) {
                    for (var i = this.module.moduleEntity.triggerConditions.length - 1; i >= 0; i--) {
                        var temp = this.module.moduleEntity.triggerConditions[i];
                        var isRemove = false;
                        if (item.variableName === temp.variable.variableName) {
                            isRemove = true;
                        }
                        if (isRemove == true) {
                            var delItem = this.module.moduleEntity.triggerConditions.splice(i, 1);
                            this.removeTriggerCombin(delItem[0]);
                        }
                    }
                }
            },
            typeChange(number) {
                if (number == 1) {
                    let tempItem = this.triggerItems.filter(c => c.key == this.triggerCondition.conditionItem.key);
                    if (tempItem.length > 0) {
                        this.triggerCondition.conditionItem.value = tempItem[0].value;
                    }
                } else if (number == 2) {
                    let tempItem = this.endItems.filter(c => c.key == this.endCondition.conditionItem.key);
                    if (tempItem.length > 0) {
                        this.endCondition.conditionItem.value = tempItem[0].value;
                    }
                }
            },
            variableChange(number) {
                if (number == 1) {
                    let tempVariable = this.module.moduleEntity.variables.filter(c => c.id == this.triggerCondition.variable.id);
                    if (tempVariable.length > 0) {
                        this.triggerCondition.variable.variableName = tempVariable[0].variableName;
                    }
                } else if (number == 2) {
                    let tempVariable = this.module.moduleEntity.variables.filter(c => c.id == this.endCondition.variable.id);
                    if (tempVariable.length > 0) {
                        this.endCondition.variable.variableName = tempVariable[0].variableName;
                    }
                }
            },
            cancelSaveEndCondition() {
                this.endCondition = {
                    id: '',
                    moduleId: '',
                    type: 2,
                    conditionItem: {
                        key: '',
                        value: ''
                    },
                    expressions: [],
                    expressionText: '',
                    name: '',
                    title: '',
                    value: '',
                    createdDate: '',
                    conditionItemValue: '',
                    variable: {
                        variableName: '',
                        id: ''
                    },
                    variableName: '',
                    variableId: '',
                };
            },
            addEndCondition() {
                if (this.checkCondition(this.endCondition, this.module, "end")) {
                    if (this.endCondition.id != '') {
                        var index = common.getFirstItemIndex(this.module.moduleEntity.endConditions, this.endCondition.id);
                        if (index >= 0) {
                            this.setConditionText(this.endCondition);
                            this.module.moduleEntity.endConditions.splice(index, 1, $.extend(true, {}, this.endCondition));
                        }
                    } else {
                        this.endCondition.id = common.Guid();
                        this.endCondition.moduleId = this.moduleId;
                        this.endCondition.createdDate = common.getNowFormatDate();

                        this.endCondition.variableId = this.endCondition.variable.id;
                        this.endCondition.type = 2;
                        this.setConditionText(this.endCondition);

                        this.module.moduleEntity.endConditions.push($.extend(true, {}, this.endCondition));
                    }

                    this.endCondition.id = '';
                    this.endCondition.name = '';
                    this.endCondition.conditionItem = {};
                    this.endCondition.key1SymbolValue1 = '';
                    this.endCondition.key1Symbol1 = '';
                    this.endCondition.variableId = '';
                    this.endCondition.variable = {};
                    this.endCondition.variableName = '';
                    this.endCondition.key1Symbol2 = '';
                    this.endCondition.key1SymbolValue2 = '';
                    this.endCondition.key2Value = '';
                    this.endCondition.key3Value = '';
                    this.endCondition.key5Value = '';
                    this.$message({
                        type: 'success',
                        message: '保存成功,请记得点击保存全部按钮保存到数据库'
                    });
                }

            },
            addConditionToEnd(item) {
                let temp = { id: common.Guid(), text: item.name, typeId: item.conditionItem.Id, expressionText: item.expressionText, moduleId: this.moduleId, type: 0, key: item.conditionItem.key };
                if (this.module.moduleEntity.combinEndCondition.expressions == null) this.module.moduleEntity.combinEndCondition.expressions = [];
                if (this.checkExpression(this.module.moduleEntity.combinEndCondition.expressions, temp)) {
                    this.module.moduleEntity.combinEndCondition.expressions.push(temp);
                    this.setExpressionText(this.module.moduleEntity.combinEndCondition);
                }
            },
            saveCombinEndCondition() {
                if (this.module.moduleEntity.combinEndCondition.name == '') {
                    this.module.moduleEntity.combinEndCondition.name = "结束组合条件";
                }

                if (this.module.moduleEntity.combinEndCondition.id == '')
                    this.module.moduleEntity.combinEndCondition.id = common.Guid();

                if (this.module.moduleEntity.combinEndCondition.expressions.length == 0) {
                    return this.reject("请选择结束条件并合并为组合条件");
                }

                if (this.checkSaveCombin(this.module.moduleEntity.combinEndCondition.expressions)) {
                    if (common.checkJs(this.module.moduleEntity.variables, this.module.moduleEntity.combinEndCondition.expressionText)) {
                        //common.testJs(this.module.moduleEntity.variables, this.module.moduleEntity.combinEndCondition.expressionText).then(() => {
                        this.module.moduleEntity.combinEndCondition.createdDate = common.getNowFormatDate();
                        this.module.moduleEntity.combinEndCondition.moduleId = this.moduleId;
                        this.$message({
                            type: 'success',
                            message: '确认成功,请记得点击保存全部按钮保存到数据库'
                        });
                    } else {
                        this.reject("结束组合条件表达式不合法，请检查");
                    }
                }
            },
            selectedEndRelation(item) {
                let temp = { id: common.Guid(), text: item.title, typeId: item.Id, expressionText: item.value, moduleId: this.moduleId, type: item.type };
                if (this.checkExpression(this.module.moduleEntity.combinEndCondition.expressions, temp)) {
                    this.module.moduleEntity.combinEndCondition.expressions.push(temp);
                    this.setExpressionText(this.module.moduleEntity.combinEndCondition);
                }
            },
            editEndCondition(index) {
                var item = this.module.moduleEntity.endConditions[index];
                this.endCondition = $.extend(true, {}, item);
            },
            removeEndCondition(item) {
                if (this.module.moduleEntity.endConditions.length > 0) {
                    for (var i = this.module.moduleEntity.endConditions.length - 1; i >= 0; i--) {
                        var temp = this.module.moduleEntity.endConditions[i];
                        var isRemove = false;
                        if (item.variableName === temp.variable.variableName) {
                            isRemove = true;
                        }
                        if (isRemove == true) {
                            var delItem = this.module.moduleEntity.endConditions.splice(i, 1);
                            this.removeEndCombin(delItem[0]);
                        }
                    }
                }
            },
            removeTriggerCombin() {
                this.module.moduleEntity.triggerCombinCondition = {};
            },
            removeEndCombin() {
                this.module.moduleEntity.combinEndCondition = {};
            },
            delEndCondition(index) {
                var item = this.module.moduleEntity.endConditions.splice(index, 1);

            },
            delEndExpress(item) {
                this.module.moduleEntity.combinEndCondition.expressions.splice(this.module.moduleEntity.combinEndCondition.expressions.indexOf(item), 1);
                this.setExpressionText(this.module.moduleEntity.combinEndCondition);
            },

            addInExpression(value) {
                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.variableKeepExpression)) {
                    this.module.moduleEntity.rule.variableKeepExpression = "";
                }
                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.checkByTimeCalcFormula)) {
                    this.module.moduleEntity.rule.checkByTimeCalcFormula = "";
                }
                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.checkByFormula)) {
                    this.module.moduleEntity.rule.checkByFormula = "";
                }
                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.customExpression)) {
                    this.module.moduleEntity.rule.customExpression = "";
                }

                let tempValue = " " + value;
                switch (this.module.moduleEntity.rule.ruleType) {
                    case '1':
                        this.module.moduleEntity.rule.variableKeepExpression += tempValue;
                        break;
                    case '3':
                        this.module.moduleEntity.rule.checkByTimeCalcFormula += tempValue;
                        break;
                    case '4':
                        this.module.moduleEntity.rule.checkByFormula += tempValue;
                        break;
                    case '5':
                        this.module.moduleEntity.rule.customExpression += tempValue;
                        break;
                    default:
                        break;
                }
            },
            addQuickSetting(number) {
                let expression = '';
                if (number == 1) {
                    if (!common.valueIsNotNullOrEmpty(this.quickSetting1.centerVariable)
                        || !common.valueIsNotNullOrEmpty(this.quickSetting1.firstSymbol)
                        || !common.valueIsNotNullOrEmpty(this.quickSetting1.firstValue)
                        || !common.valueIsNotNullOrEmpty(this.quickSetting1.secondSymbol)
                        || !common.valueIsNotNullOrEmpty(this.quickSetting1.secondValue)) {
                        this.$notify.error({
                            title: '错误',
                            message: '表达式不完整,请填写完整的表达式再添加'
                        });
                        return;
                    }
                    expression = "(" + this.quickSetting1.centerVariable + " " + this.quickSetting1.firstSymbol + " " + this.quickSetting1.firstValue + " && ";
                    expression += this.quickSetting1.centerVariable + " " + this.quickSetting1.secondSymbol + " " + this.quickSetting1.secondValue + ")";
                } else if (number == 2) {
                    if (!common.valueIsNotNullOrEmpty(this.quickSetting2.centerVariable)
                        || !common.valueIsNotNullOrEmpty(this.quickSetting2.firstSymbol)
                        || !common.valueIsNotNullOrEmpty(this.quickSetting2.firstValue)
                        || !common.valueIsNotNullOrEmpty(this.quickSetting2.secondSymbol)
                        || !common.valueIsNotNullOrEmpty(this.quickSetting2.secondValue)) {
                        this.$notify.error({
                            title: '错误',
                            message: '表达式不完整,请填写完整的表达式再添加'
                        });
                        return;
                    }
                    expression = "(" + this.quickSetting2.centerVariable + " " + this.quickSetting2.firstSymbol + " " + this.quickSetting2.firstValue + " || ";
                    expression += this.quickSetting2.centerVariable + " " + this.quickSetting2.secondSymbol + " " + this.quickSetting2.secondValue + ")";
                } else if (number == 3) {
                    if (!common.valueIsNotNullOrEmpty(this.quickSetting3.centerVariable)
                        || !common.valueIsNotNullOrEmpty(this.quickSetting3.firstSymbol)
                        || !common.valueIsNotNullOrEmpty(this.quickSetting3.firstValue)) {
                        this.$notify.error({
                            title: '错误',
                            message: '表达式不完整,请填写完整的表达式再添加'
                        });
                        return;
                    }
                    expression = "(" + this.quickSetting3.centerVariable + " " + this.quickSetting3.firstSymbol + " " + this.quickSetting3.firstValue + ")";
                }

                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.variableKeepExpression)) {
                    this.module.moduleEntity.rule.variableKeepExpression = "";
                }
                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.checkByTimeCalcFormula)) {
                    this.module.moduleEntity.rule.checkByTimeCalcFormula = "";
                }
                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.checkByFormula)) {
                    this.module.moduleEntity.rule.checkByFormula = "";
                }
                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.customExpression)) {
                    this.module.moduleEntity.rule.customExpression = "";
                }

                if (expression != '') {
                    switch (this.module.moduleEntity.rule.ruleType) {
                        case '1':
                            this.module.moduleEntity.rule.variableKeepExpression += expression;
                            break;
                        case '3':
                            this.module.moduleEntity.rule.checkByTimeCalcFormula += expression;
                            break;
                        case '4':
                            this.module.moduleEntity.rule.checkByFormula += expression;
                            break;
                        case '5':
                            this.module.moduleEntity.rule.customExpression += expression;
                            break;
                        default:
                            break;
                    }
                }
            },
            setModuleRuleEmpty() {
                switch (this.module.moduleEntity.rule.ruleType) {
                    case '1':
                        this.setType2Empty();
                        this.setType3Empty();
                        this.setType4Empty();
                        this.setType5Empty();
                        break;
                    case '2':
                        this.setType1Empty();
                        this.setType3Empty();
                        this.setType4Empty();
                        this.setType5Empty();
                        break;
                    case '3':
                        this.setType1Empty();
                        this.setType2Empty();
                        this.setType4Empty();
                        this.setType5Empty();
                        break;
                    case '4':
                        this.setType1Empty();
                        this.setType2Empty();
                        this.setType3Empty();
                        this.setType5Empty();
                        break;
                    case '5':
                        this.setType1Empty();
                        this.setType2Empty();
                        this.setType3Empty();
                        this.setType4Empty();
                        break;
                    default:
                        break;
                }
            },
            setType1Empty() {
                this.module.moduleEntity.rule.variableKeepExpression = '';
                this.module.moduleEntity.rule.variableKeepSeconds = '';
                this.module.moduleEntity.rule.variableKeepScores = '';
            },
            setType2Empty() {
                this.module.moduleEntity.rule.variableCheckName = '';
                this.module.moduleEntity.rule.variableCheckResult = '';
            },
            setType3Empty() {
                this.module.moduleEntity.rule.checkByTimeSeconds = '';
                this.module.moduleEntity.rule.checkByTimeVariableId = '';
                this.module.moduleEntity.rule.checkByTimeVariableName = '';
                this.module.moduleEntity.rule.checkByTimeCompareArray = '';
                this.module.moduleEntity.rule.checkByTimeMaxDeviation = '';
                this.module.moduleEntity.rule.checkByTimeCalcFormula = '';
            },
            setType4Empty() {
                this.module.moduleEntity.rule.checkByFormula = '';
            },
            setType5Empty() {
                this.module.moduleEntity.rule.customExpression = '';
            },
            checkByTimeVariableChange() {
                if (this.module.moduleEntity.rule.checkByTimeVariableName != '') {
                    this.module.moduleEntity.variables.forEach(c => {
                        if (c.variableName == this.module.moduleEntity.rule.checkByTimeVariableName) {
                            this.module.moduleEntity.rule.checkByTimeVariableId = c.id;
                        }
                    });
                }
            },
            saveModuleRule() {
                debugger;
                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.ruleName)) {
                    return this.reject("请输入规则名");
                }
                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.ruleType)) {
                    return this.reject("请选择规则类型");
                }
                if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.ruleTotalScore)) {
                    return this.reject("请输入规则总分");
                }

                if (this.module.moduleEntity.rule.ruleType == 1) {
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.variableKeepSeconds)) {
                        return this.reject("请输入变量维持规则时间");
                    }
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.variableKeepScores)) {
                        return this.reject("请输入变量维持规则扣分值");
                    }
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.variableKeepExpression)) {
                        return this.reject("请输入变量维持规则表达式");
                    }
                    if (!common.checkJs(this.module.moduleEntity.variables, this.module.moduleEntity.rule.variableKeepExpression)) {
                        return this.reject("按时间取值规则表达式不合法");
                    }

                } else if (this.module.moduleEntity.rule.ruleType == 2) {
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.variableCheckName)) {
                        return this.reject("请选择变量检查规则的变量");
                    }
                    //console.log(this.module.moduleEntity.rule.variableCheckResult + "    " + common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.variableCheckResult));
                    //if (this.module.moduleEntity.rule.variableCheckResult == '') {
                    //    return this.reject("请选择变量检查规则的取值");
                    //}
                } else if (this.module.moduleEntity.rule.ruleType == 3) {
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.checkByTimeSeconds)) {
                        return this.reject("请输入按时间取值规则取值时间");
                    }
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.checkByTimeVariableName)) {
                        return this.reject("请选择按时间取值规则变量");
                    }
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.checkByTimeCompareArray)) {
                        return this.reject("请输入按时间取值规则标准值组");
                    } else {
                        var tempArray = this.module.moduleEntity.rule.checkByTimeCompareArray.split(',');
                        var allIsNumber = true;
                        for (var i = 0; i < tempArray.length; i++) {
                            if (isNaN(Number(tempArray[i]))) {
                                allIsNumber = false;
                                break;
                            }
                        }

                        if (!allIsNumber) {
                            return this.reject("所有的标准值必须都是数字,请检查");
                        }
                    }
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.checkByTimeMaxDeviation)) {
                        return this.reject("请输入按时间取值规则允许最大偏差");
                    }
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.checkByTimeCalcFormula)) {
                        return this.reject("请输入按时间取值规则计算表达式");
                    }
                    if (!common.checkJs(this.module.moduleEntity.variables, this.module.moduleEntity.rule.checkByTimeCalcFormula)) {
                        return this.reject("按时间取值规则表达式不合法");
                    }
                } else if (this.module.moduleEntity.rule.ruleType == 4) {
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.checkByFormula)) {
                        return this.reject("请输入取值公式检查规则表达式");
                    }
                    if (!common.checkJs(this.module.moduleEntity.variables, this.module.moduleEntity.rule.checkByFormula)) {
                        return this.reject("取值公式检查表达式不合法，请重新输入");
                    }
                } else if (this.module.moduleEntity.rule.ruleType == 5) {
                    if (!common.valueIsNotNullOrEmpty(this.module.moduleEntity.rule.customExpression)) {
                        return this.reject("请输入自定义规则表达式");
                    }
                    if (!common.checkJs(this.module.moduleEntity.variables, this.module.moduleEntity.rule.customExpression)) {
                        return this.reject("自定义规则表达式不合法，请重新输入");
                    }
                }

                this.$message({
                    type: 'success',
                    message: '保存成功,请记得点击保存全部按钮保存到数据库'
                });

                return true;
            },
            checkModule(module) {
                this.setModuleRuleEmpty();
                var expressText = "";

                if (!common.valueIsNotNullOrEmpty(module.moduleName)) {
                    return this.reject("请输入模块名称");
                }
                if (module.moduleEntity.variables.length == 0) {
                    return this.reject("请先生成变量");
                }
                if (module.moduleEntity.triggerConditions.length == 0) {
                    return this.reject("没有触发条件，请先创建");
                }
                if (module.moduleEntity.triggerConditions.length >= 2 && (!common.valueIsNotNullOrEmpty(module.moduleEntity.combinTriggerCondition.moduleId))) {
                    return this.reject("多个触发条件下，必须使用组合触发条件");
                }
                if (module.moduleEntity.endConditions.length == 0) {
                    return this.reject("没有结束条件，请先创建");
                }
                if (module.moduleEntity.endConditions.length >= 2 && (!common.valueIsNotNullOrEmpty(module.moduleEntity.combinEndCondition.moduleId))) {
                    return this.reject("多个结束条件下，必须使用组合结束条件");
                }
                if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.ruleName)) {
                    return this.reject("请输入规则名");
                }
                if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.ruleType)) {
                    return this.reject("请选择规则类型");
                }

                if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.ruleTotalScore)) {
                    return this.reject("请输入规则总分");
                }

                if (module.moduleEntity.rule.ruleType == 1) {
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.variableKeepSeconds)) {
                        return this.reject("请输入变量维持规则时间");
                    }
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.variableKeepScores)) {
                        return this.reject("请输入变量维持规则扣分值");
                    }
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.variableKeepExpression)) {
                        return this.reject("请输入变量维持规则表达式");
                    }
                    if (!common.checkJs(module.moduleEntity.variables, module.moduleEntity.rule.checkByTimeCalcFormula)) {
                        return this.reject("变量维持规则表达式不合法，请重新输入");
                    }
                } else if (module.moduleEntity.rule.ruleType == 2) {
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.variableCheckName)) {
                        return this.reject("请选择变量检查规则的变量");
                    }
                    //if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.variableCheckResult)) {
                    //    return this.reject("请选择变量检查规则的取值");
                    //}
                } else if (module.moduleEntity.rule.ruleType == 3) {
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.checkByTimeSeconds)) {
                        return this.reject("请输入按时间取值规则取值时间");
                    }
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.checkByTimeVariableName)) {
                        return this.reject("请选择按时间取值规则变量");
                    }
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.checkByTimeCompareArray)) {
                        return this.reject("请输入按时间取值规则标准值组");
                    } else {
                        var tempArray = this.module.moduleEntity.rule.checkByTimeCompareArray.split(',');
                        var allIsNumber = true;
                        for (var i = 0; i < tempArray.length; i++) {
                            if (isNaN(Number(tempArray[i]))) {
                                allIsNumber = false;
                                break;
                            }
                        }

                        if (!allIsNumber) {
                            return this.reject("所有的标准值必须都是数字,请检查");
                        }
                    }
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.checkByTimeMaxDeviation)) {
                        return this.reject("请输入按时间取值规则允许最大偏差");
                    }
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.checkByTimeCalcFormula)) {
                        return this.reject("请输入按时间取值规则计算表达式");
                    }
                    if (!common.checkJs(module.moduleEntity.variables, module.moduleEntity.rule.checkByTimeCalcFormula)) {
                        return this.reject("按时间取值规则表达式不合法，请重新输入");
                    }
                } else if (module.moduleEntity.rule.ruleType == 4) {
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.checkByFormula)) {
                        return this.reject("请输入取值公式检查规则表达式");
                    }
                    if (!common.checkJs(module.moduleEntity.variables, module.moduleEntity.rule.checkByFormula)) {
                        return this.reject("取值公式检查表达式不合法，请重新输入");
                    }
                } else if (module.moduleEntity.rule.ruleType == 5) {
                    if (!common.valueIsNotNullOrEmpty(module.moduleEntity.rule.customExpression)) {
                        return this.reject("请输入自定义规则表达式");
                    }
                    if (!common.checkJs(module.moduleEntity.variables, module.moduleEntity.rule.customExpression)) {
                        return this.reject("自定义规则表达式不合法，请重新输入");
                    }
                }

                return true;
            },
            setScoringValueDefault() {
                this.tableDataTakeValue = [];
                this.tableDataTimeTake = [];
            },
            setScoring(showing, type, scoringSection, scoringValue, isEdit) {
                this.showScoringDialog = showing;
                this.scoringDialogType = type;
                this.scoringObj.scoringSection = scoringSection;
                this.scoringObj.scoringValue = scoringValue;
                this.isEditScoring = isEdit;
            },
            saveScoringDialog() {
                if (this.scoringDialogType == "TimeTake") {
                    this.saveTimeTakeRow();
                } else if (this.scoringDialogType == "TakeValue") {
                    this.saveTakeValueRow();
                }
                this.setScoringValueDefault();
            },
            newTimeTakeScoring() {
                this.setScoring(true, "TimeTake", "", "", false);
            },
            editTimeTakeRow(index, rows) {
                this.editScoringIndex = index;
                let tempSection = rows[index];
                this.setScoring(true, "TimeTake", tempSection.scoringSection, tempSection.scoringValue, true);
            },
            saveTimeTakeRow() {
                let tempObj = {
                    id: common.Guid(),
                    ruleType: this.module.moduleEntity.rule.ruleType,
                    scoringSection: this.scoringObj.scoringSection,
                    scoringValue: this.scoringObj.scoringValue
                };

                if (!this.isEditScoring) {
                    this.tableDataTimeTake.push(tempObj);
                } else {
                    this.tableDataTimeTake.splice(this.editScoringIndex, 1, tempObj);
                }
                this.setScoring(false, "", "", "", false);
            },
            deleteSectionRow(index, rows) {
                this.$confirm('确认删除此区间?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    rows.splice(index, 1);
                    this.$notify({
                        title: '成功',
                        message: '删除成功!',
                        type: 'success'
                    });
                }).catch(() => {
                    this.$notify.info({
                        title: '消息',
                        message: '已取消删除!'
                    });
                });
            },
            newTakeValueScoring() {
                this.setScoring(true, "TakeValue", "", "", false);
            },
            editTakeValueRow(index, rows) {
                this.editScoringIndex = index;
                let tempSection = rows[index];
                this.setScoring(true, "TakeValue", tempSection.scoringSection, tempSection.scoringValue, true);
            },
            saveTakeValueRow() {
                let tempObj = {
                    ruleType: this.module.moduleEntity.rule.ruleType,
                    scoringSection: this.scoringObj.scoringSection,
                    scoringValue: this.scoringObj.scoringValue
                };

                if (!this.isEditScoring) {
                    this.tableDataTakeValue.push(tempObj);
                } else {
                    this.tableDataTakeValue.splice(this.editScoringIndex, 1, tempObj);
                }
                this.setScoring(false, "", "", "", false);
            },
        },
        computed: {
            variablesNum() {
                return this.module.moduleEntity.variables.length;
            }
        }
    }

</script>

<style>
    .el-tag + .el-tag {
        margin-left: 10px;
    }

    .el-input {
        width: 150px;
    }

    .el-input__inner {
        width: 150px;
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

