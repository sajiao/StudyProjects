<template>
	<view class="s-page-wrapper">
		<view class="">
			<swiper class="swiper is-100vh" @change="swiperChange">
				<swiper-item @click="toHome">
					<image
						src="../../static/img/nview/1.PNG"
						mode="widthFix"
						class="is-response"
					></image>
				</swiper-item>
				<swiper-item @click="toHome">
					<image
						src="../../static/img/nview/2.PNG"
						mode="widthFix"
						class="is-response"
					></image>
				</swiper-item>
				<swiper-item @click="toHome">
					<image
						src="../../static/img/nview/3.PNG"
						mode="widthFix"
						class="is-response"
					></image>
				</swiper-item>
			</swiper>
		</view>
	</view>
</template>

<script>
export default {
	data() {
		return {
			maxCurrent: 3,
			current: 0
		};
	},
	methods: {
		swiperChange: function(e) {
			var current = e.detail.current;
			this.current = current;
		},
		toHome: function() {
			if (this.current + 1 == this.maxCurrent) {
				uni.reLaunch({
					url: '/pages/index/index'
				});
			}
		}
	},
	onLoad: function() {
		setTimeout(function() {
			uni.reLaunch({
				url: '/pages/index/index'
			});
		}, 5000);
	}
};
</script>

<style></style>
