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
            meta: { title: '主页' },
            children:[
                {
                    path: '/main',
                    component: resolve => require(['../components/page/main.vue'], resolve),
                    meta: { title: '主页' }
                },
                {
                    path: '/list',
                    component: resolve => require(['../components/page/itemlist.vue'], resolve),
                    meta: { title: '分类' }
                },
                {
                    path: '/list/:tag',
                    component: resolve => require(['../components/page/itemlist.vue'], resolve),
                    meta: { title: '分类' },
					props: true 
                }
            ]
        },
        {
            path: '/m',
            component: resolve => require(['../components/common/MHome.vue'], resolve),
            meta: { title: '主页' },
            children:[
                {
                    path: '/m/main',
                    component: resolve => require(['../components/page/main.vue'], resolve),
                    meta: { title: '主页' }
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
