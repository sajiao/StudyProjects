'use strict'

import axios from 'axios'
//import { getToken } from '@/utils/auth'

// 超时时间
axios.defaults.timeout = 5000

function checkStatus(response) {
  // loading
  // 如果http状态码正常，则直接返回数据
  if (response && (response.status === 200 || response.status === 304 || response.status === 400)) {
    return response
    // 如果不需要除了data之外的数据，可以直接 return response.data
  }
  // 异常状态下，把错误信息返回去
  return {
    status: -404,
    msg: '网络异常'
  }
}

function checkCode(res) {
  // 如果code异常(这里已经包括网络错误，服务器错误，后端抛出的错误)，可以弹出一个错误提示，告诉用户
  if (res.status === -404) {
    alert(res.msg)
  }
  if (res.data === null) {
    // alert(res.data)
  }
  return res
}

export default {
  post(url, data) {
    return axios({
      method: 'post',
      baseURL: process.env.BASE_API,
      url,
      data: data,
      headers: {
        // 'Content-Type': 'application/json;charset=UTF-8'
      }
    }).then(
      (response) => {
        return checkStatus(response)
      }
    ).then(
      (res) => {
        return checkCode(res)
      }
    )
  },
  put(url, data) {
    return axios({
      method: 'put',
      baseURL: process.env.BASE_API,
      url,
      data: data,
      headers: {
        // 'X-Requested-With': 'XMLHttpRequest',
        //  'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
      }
    }).then(
      (response) => {
        return checkStatus(response)
      }
    ).then(
      (res) => {
        return checkCode(res)
      }
    )
  },
  get(url, params) {
    return axios({
      method: 'get',
      baseURL: process.env.BASE_API,
      url,
      params, // get 请求时带的参数
      headers: {
      }
    }).then(
      (response) => {
        return checkStatus(response)
      }
    ).then(
      (res) => {
        return checkCode(res)
      }
    )
  },
  delete(url, params) {
    return axios({
      method: 'delete',
      baseURL: process.env.BASE_API,
      url,
      params,
      headers: {
      }
    }).then(
      (response) => {
        return checkStatus(response)
      }
    ).then(
      (res) => {
        return checkCode(res)
      }
    )
  }
}

