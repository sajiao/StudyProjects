import Vue from 'vue';
import Router from 'vue-router';
import { resolve } from 'path';

Vue.use(Router);

export default new Router({
    mode: 'history',
    routes: [
        {
            path: '/',
            redirect: '/main'
        },
        {
            path: '/',
            component: resolve => require(['../components/common/Home.vue'], resolve),
            meta: { title: '自述文件' },
            children:[
                {
                    path: '/main',
                    component: resolve => require(['../components/page/main.vue'], resolve),
                    meta: { title: '评分模块' }
                },
                {
                    path: '/register',
                    component: resolve => require(['../components/page/register.vue'], resolve),
                    meta: { title: '注册模块' }
                },
                {
                    path: '/list',
                    component: resolve => require(['../components/page/itemlist.vue'], resolve),
                    meta: { title: '分类' }
                },
                {
                    path: '/list/:keyword',
                    component: resolve => require(['../components/page/itemlist.vue'], resolve),
                    meta: { title: '分类' }
                },
                {
                    path: '/addtest',
                    component: resolve => require(['../components/page/addtest.vue'], resolve),
                    meta: { title: '添加题库' }
                },
                {
                    path: '/testdetail',
                    component: resolve => require(['../components/page/testdetail.vue'], resolve),
                    meta: { title: '题库详情' }
                },
                {
                    // 富文本编辑器组件
                    path: '/socket',
                    component: resolve => require(['../components/page/socket.vue'], resolve),
                    meta: { title: 'socket' }
                },
                //{
                //    // markdown组件
                //    path: '/markdown',
                //    component: resolve => require(['../components/page/Markdown.vue'], resolve),
                //    meta: { title: 'markdown编辑器' }    
                //},
                {
                    // 图片上传组件
                    path: '/upload',
                    component: resolve => require(['../components/page/Upload.vue'], resolve),
                    meta: { title: '文件上传' }   
                },
                {
                    // vue-schart组件
                    path: '/charts',
                    component: resolve => require(['../components/page/BaseCharts.vue'], resolve),
                    meta: { title: 'schart图表' }
                },
                {
                    // 拖拽列表组件
                    path: '/drag',
                    component: resolve => require(['../components/page/DragList.vue'], resolve),
                    meta: { title: '拖拽列表' }
                },
                {
                    // 权限页面
                    path: '/permission',
                    component: resolve => require(['../components/page/Permission.vue'], resolve),
                    meta: { title: '权限测试', permission: true }
                }
            ]
        },
        {
            path: '/m',
            component: resolve => require(['../components/common/MHome.vue'], resolve),
            meta: { title: '自述文件' },
            children:[
                {
                    path: '/m/main',
                    component: resolve => require(['../components/page/main.vue'], resolve),
                    meta: { title: '评分模块' }
                },
                {
                    path: '/m/list',
                    component: resolve => require(['../components/page/itemlist.vue'], resolve),
                    meta: { title: '分类' }
                },
                {
                    path: '/m/list/:keyword',
                    component: resolve => require(['../components/page/itemlist.vue'], resolve),
                    meta: { title: '分类' }
                },
            ]
        },
        {
            path: '/login',
            component: resolve => require(['../components/page/Login.vue'], resolve)
        },
        {
            path: '/404',
            component: resolve => require(['../components/page/404.vue'], resolve)
        },
        {
            path: '/403',
            component: resolve => require(['../components/page/403.vue'], resolve)
        },
        {
            path: '*',
            redirect: '/404'
        }
    ]
})
