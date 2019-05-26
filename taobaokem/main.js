import Vue from 'vue'
import App from './App'
import httpApi from './common/httpApi'
import HttpRequest from './common/httpRequest'
import HttpCache from './common/cache'

import loadMore from './components/uni-pro/load-more'
import moment from 'moment'

Vue.config.productionTip = false
Vue.prototype.$api = httpApi
Vue.prototype.$Request = HttpRequest
Vue.prototype.$Sysconf = HttpRequest.config
Vue.prototype.$SysCache = HttpCache

App.mpType = 'app'

Vue.filter('dateformat', function(dataStr, pattern = 'YYYY-MM-DD') {
    return moment(dataStr).format(pattern)
})

Vue.component('load-more',loadMore)
const app = new Vue({
    ...App
})
app.$mount()
