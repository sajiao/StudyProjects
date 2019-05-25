'use strict'
import testjsService from '@/server/testjsService'
import { rejects } from 'assert';
import { resolve } from 'url';
export default {
    Guid() {
        var d = new Date().getTime();
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (d + Math.random() * 16) % 16 | 0;
            d = Math.floor(d / 16);
            return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
        });
        return uuid;
    },
    getNowFormatDate() {
        var date = new Date();
        var seperator1 = "-";
        var seperator2 = ":";
        var month = date.getMonth() + 1;
        var strDate = date.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds();
        return currentdate;
    },
    getItem(items, id) {

        var temps = items.filter(i => i.id === id);
        if (temps != null && temps.length > 0)
            return temps[0];
        else
            return null;
    },
    getFirstItemIndex(items, id) {
        var index = -1;
        for (var i = 0; i < items.length; i++) {
            if (items[i].id === id) {
                index = i;
                break;
            }
        }

        return index;
    },
    checkJs(variables, str) {
        var result = true;
        var tempVar = "var ";

        if (variables.length > 0) {
            for (var i = 0; i < variables.length; i++) {
                tempVar += variables[i].variableName + ",";
            }

            tempVar = tempVar.substr(0, tempVar.length - 1) + "; " + str;
        } else {
            tempVar = str;
        }

        //var p = new Promise(function (resolve, reject) {
        try {
            eval(tempVar);
            //resolve();
        }
        catch (exception) {
            result = false;
            console.log(exception);
            //this.$notify.error({
            //    title: '错误',
            //    message: "脚本验证不合法： " + exception
            //});
            //reject("脚本验证不合法： " + exception);
        }
        //});

        return result;
    },
    testJs(variables, str) {
        var obj = {};
        obj.Variables = variables;
        obj.Func = str;
        var p = new Promise(function (resolve, reject) {
            testjsService.post(obj).then((res) => {
                if (!res.data.result) {
                    reject(res.data.message);
                } else {
                    resolve();
                }
            });
        });
        return p;
    },
    valueIsNotNullOrEmpty(value) {
        if (value == undefined || value == null || value == "") {
            return false;
        } else {
            return true;
        }
    },
    getSystemTime() {
        var myDate = new Date();//获取系统当前时间        
        myDate.getFullYear(); //获取完整的年份(4位,1970-????)
        myDate.getMonth(); //获取当前月份(0-11,0代表1月)
        myDate.getDate(); //获取当前日(1-31)
        myDate.getDay(); //获取当前星期X(0-6,0代表星期天)
        myDate.getTime(); //获取当前时间(从1970.1.1开始的毫秒数)
        var hours = myDate.getHours(); //获取当前小时数(0-23)
        var minutes = myDate.getMinutes(); //获取当前分钟数(0-59)
        myDate.getSeconds(); //获取当前秒数(0-59)
        myDate.getMilliseconds(); //获取当前毫秒数(0-999)
        myDate.toLocaleDateString(); //获取当前日期
        console.log(myDate);

        return myDate;
    },
    isNumber(val) {
        var regPos = /^\d+(\.\d+)?$/; //非负浮点数
        var regNeg = /^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$/; //负浮点数
        if (regPos.test(val) || regNeg.test(val)) {
            return true;
        } else {
            return false;
        }
    },
}


