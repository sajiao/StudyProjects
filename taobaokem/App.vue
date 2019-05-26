<script>
	export default {
		onLaunch: function() {
			console.log('App Launch')
			//#ifdef APP-PLUS
			plus.screen.lockOrientation('portrait-primary'); //锁定屏幕方向 
			//#endif
			// 部分配置需要从服务器加载
			this.$Request.post(this.$api.common.config).then((res) => {
				if (res.code == 200) {
					this.$SysCache.put('web_config', res.data)
				}
			});

			//#ifdef APP-PLUS
			// APP检测更新 具体可以参考：https://ask.dcloud.net.cn/article/35667
			plus.runtime.getProperty(plus.runtime.appid, widgetInfo => {
				this.$Request.post(this.$api.common.update, {
					appid: widgetInfo.appid, //当前应用的APPID
					version: widgetInfo.version, // 当前应用的版本  
					name: widgetInfo.name, // 当前应用的名称 
					imei: plus.device.imei, 
					os: plus.os.name // OS环境 ios / android 
				}).then((res) => {
					if (res.code == 200) {
						uni.downloadFile({
							url: res.data.wgtUrl,
							success: (downloadResult) => {
								if (downloadResult.statusCode === 200) {
									plus.runtime.install(downloadResult.tempFilePath, {
										force: false
									}, (d) => {
										console.log('install success...');
										plus.runtime.restart();
									}, (e) => {
										console.error('install fail...');
									})
								}
							}
						})
					}
				})
			});
			//#endif
		},
		onShow: function() {
			console.log('App Show')
		},
		onHide: function() {
			console.log('App Hide')
		}
	}
</script>

<style>
	/*每个页面公共css */
	@import "./static/css/common.css";
	@import "./static/css/simplepro.css";
</style>
