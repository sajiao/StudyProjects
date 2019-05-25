'use strict'

import http from '@/server/http'
import api from '@/server/api'
export default {
    post(params) {
        return http.post(api.testjsAPI, params
        ).then(
            (response) => {
                return response;
            }
        ).then(
            (res) => {
                return res;
            }
        )
    },
    
}


