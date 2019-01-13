import http from '@/utils/http'

export default {
  post(apiname,params) {
    return http.post(apiname, params
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
  get(apiname,params) {
    return http.get(apiname, params
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
  getById(apiname,id) {
    return http.get(apiname+"/"+id, null
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
  put(apiname,params) {
    return http.put(apiname, params
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
  delete(apiname,params) {
    return http.delete(apiname, params
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
