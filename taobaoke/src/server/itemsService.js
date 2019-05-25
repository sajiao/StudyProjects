'use strict'

import http from '@/server/http'
import api from '@/server/api'
export default {
    post(params) {
        return http.post(api.itemsAPI, params
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
        return http.get(api.itemsAPI, params
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
    put(params) {
        return http.put(api.itemsAPI, params
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
    delete(params) {
        return http.delete(api.itemsAPI, params
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


