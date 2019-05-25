/**
 * 本地存储
 * */
let s = window.localStorage;
function setStorage(k, v, t) {
    if (!t) sessionStorage.setItem(k, v);
    else {
        let type = t[t.length - 1], num = t.substring(0, t.length - 1);
        let ot = new Date(Date.now() + getTime(type, num));//过期时间
        v = encodeURIComponent(v + "#%#" + ot);
        localStorage.setItem(k, v);
    }
    return this
}
function getStorage(k) {
    let v = decodeURIComponent(sessionStorage.getItem(k) || localStorage.getItem(k) || "");
    let n = v.indexOf('#%#');
    if (n === -1) {
        if (v.length) return v;
        return undefined;
    }
    let t = Date.parse(v.substr(n));
    let ct = Date.now();
    if (ct > t) {
        localStorage.removeItem(k);
        return undefined;
    }
    return v.substring(0, n);
}
function delStorage(k) {
    sessionStorage.removeItem(k);
    localStorage.removeItem(k);
    return this
}
function setCookie(k, v, t) {
    if (!t) document.cookie = k + "=" + encodeURIComponent(v);
    else {
        let type = t[t.length - 1], num = t.substring(0, t.length - 1);
        let ot = new Date(Date.now() + getTime(type, num));//过期时间
        let expires = "expires=" + ot.toUTCString();
        document.cookie = k + "=" + encodeURIComponent(v) + ";" + expires;
    }
    return this
}
function getCookie(k) {
    let num = document.cookie.indexOf(k + "=");
    if (num === -1) return undefined;
    let s = document.cookie.substr(num).split(';')[0];
    return s.substr(s.indexOf('=') + 1);
}
function delCookie(k) {
    setCookie(k, "", -1);
    return this
}
function getTime(type, num) {
    switch (type) {
        case 'seconds':
            return num * 1000;
        case 'minutes':
            return num * 60000;
        case 'hours':
            return num * 3600000;
        case 'days':
            return num * 86400000;
        case 'weeks':
            return num * 604800000;
        case 's':
            return num * 1000;
        case 'm':
            return num * 60000;
        case 'h':
            return num * 3600000;
        case 'd':
            return num * 86400000;
        case 'w':
            return num * 604800000;
    }
}
function Storage() {
    this.set = s ? setStorage : setCookie;
    this.get = s ? getStorage : getCookie;
    this.del = s ? delStorage : delCookie;
}
export default new Storage()
