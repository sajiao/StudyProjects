<template>
	<view class="s-page-wrapper">
		你好啊！
	</view>
</template>

<script>
export default {
	data() {
		return {};
	},
	onNavigationBarButtonTap: function(index) {
		let cmr = plus.camera.getCamera();
		let res = cmr.supportedVideoResolutions[0];
		let fmt = cmr.supportedVideoFormats[0];
		cmr.startVideoCapture(
			function(path) {
				plus.io.resolveLocalFileSystemURL(
					path,
					entry => {
						console.log(entry.fullPath);
						entry.file(function(file) {
							console.log('size==' + file.size);
							console.log('name==' + file.name);
						});
					},
					e => {}
				);
			},
			function(error) {
				console.log('Resolve file URL failed: ' + e.message);
			},
			{
				resolution: res,
				format: fmt,
				videoMaximumDuration: 10
			}
		);
	}
};
</script>

<style>
</style>
