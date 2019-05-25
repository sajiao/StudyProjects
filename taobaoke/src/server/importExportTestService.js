'use strict'

import http from '@/server/http'
import api from '@/server/api'
export default {
    post(params) {
        return http.post(api.importExportTestAPI, params
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
    get(params) {
        return http.get(api.importExportTestAPI, params
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


