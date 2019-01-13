import http from '@/utils/http'
import api from '@/api/api'

export default {
  post(params) {
    return http.post(api.wordAPI, params
    ).then(
      (response) => {
        return response
      }
    ).then(
      (res) => {
        return res
      }
    )
  },
  get(params) {
    return http.get(api.wordAPI, params
    ).then(
      (response) => {
        return response
      }
    ).then(
      (res) => {
        return res
      }
    )
  },
  put(params) {
    return http.put(api.wordAPI, params
    ).then(
      (response) => {
        return response
      }
    ).then(
      (res) => {
        return res
      }
    )
  },
  delete(params) {
    return http.delete(api.wordAPI, params
    ).then(
      (response) => {
        return response
      }
    ).then(
      (res) => {
        return res
      }
    )
  }
}
