'use strict'

import http from '@/server/http'
import api from '@/server/api'
export default {
    post(params) {
        return http.post(api.opcClientAPI, params
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
        return http.get(api.opcClientAPI, params
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
        return http.put(api.opcClientAPI, params
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
        return http.delete(api.opcClientAPI, params
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


