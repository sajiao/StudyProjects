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
  delete(apiname,id) {
    return http.delete(apiname+"/"+id, null
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
