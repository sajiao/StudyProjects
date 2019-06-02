<template>
	

    <ul class="slider" :style="styleObject">
        <li  v-for="(item,index) in images" :class="[move[index]]"  :key="index">
          <template v-if="typeof item === 'object'">
           <a target="_blank" :href='item.url' :title="item.title">
			   <img :src="item.src" :style="imgStyle"  @click="target(index)" /> 
			</a>
            <span :class="{tag:item.tagName}" :style="item.tagStyle" v-if="item.tagName.length > 0">{{item.tagName}}</span>
          </template>
          <template v-if="typeof item === 'string'">
            <img :src="item" :style="imgStyle"   @click="target(index)">
          </template>
        </li>

        <li  class="button">
            <em  v-for="(item,index) in images" @click="target(index)" :class="[move[index]]"></em>
        </li>
        <li class="control">
            <em @click="prePic"></em>
            <em @click="nextPic"></em>
        </li>
    </ul>
</template>
<script>
export default{
  name: 'slider',
  props: {
    styleObject: {
      default: {
        width: '750',
        height: '250'
      }
    },
    images: {
      require: true,
      type: Array,
      validator: function (value) {
        return value.length >= 3
      }
    },
    interval: {
      default: 2000
    },
    imgStyle: {
      default: {}
    },
    autoRoll: {
      default: true
    },
    direction: {
      default: 'left'
    }
  },
  mounted: function () {
    const width = parseInt(this.styleObject.width)
    const height = parseInt(this.styleObject.height)
    this.imgStyle.width = width * 0.7 + 'px'
    this.imgStyle.height = height * 0.9 + 'px'
    this.styleObject.width += 'px'
    this.styleObject.height += 'px'
    for (let i = 3; i < this.images.length; i++) {
      this.move[i] = 'wait'
    }
    if (this.autoRoll) {
      if (this.direction === 'left') {
        setInterval(this.nextPic, this.interval)
      }
      else {
        setInterval(this.prePic, this.interval)
      }
    }
  },
  data () {
    return {
      move: ['left', 'center', 'right']
    }
  },
  methods: {
    nextPic: function (event) {
      let temp = this.move.pop()
      this.move.unshift(temp)
    },
    prePic: function (event) {
      let temp = this.move.shift()
      this.move.push(temp)
    },
    target: function (pos) {
      const num = this.images.length
      for (let i = 0; i < num; i++) {
        this.move[i] = 'wait'
      }
      this.move[pos] = 'center'
      this.move[pos + 1 < num ? pos + 1 : 0] = 'right'
      this.move[pos - 1 === -1 ? num - 1 : pos - 1] = 'left'
      this.move = this.move.concat()
    }
  }
}
</script>
<style >

.slider{
    perspective:1000px;
    position: relative;
}
.slider .control em:first-child,
.slider .control em:last-child{
    position:absolute;
    display: block;
    width: 30px;
    height: 30px;
    background-size: cover;
    z-index: 100;
    cursor: pointer;
}
.slider .control em:first-child{
    left: 0px;
     background-image: url(@'/assets/last.png'); 
}
.slider .control em:last-child{
    right: 0px;
     background-image: url(@'/assets/next.png'); 
} 
.slider li{
    list-style-type: none;
    position: absolute;
    top: 0px;
    transition:transform 0.5s,scale 1s,left 1s,right 1s;
}
.slider li.left,
.slider li.right{
    transform: scale(0.9,0.9);
    z-index: 10;
}
.slider li.left{
    left: -3%;
}
.slider li.center{
    left: 15%;
    z-index: 100;
}
.slider li.right{
    left: 33%;
}
.slider li.wait{
    transform: scale(0,0);
}
.slider .button{
    text-align: center;
    width: 100%;
    height: 20px;
    top: calc(100% - 20px);
}
.slider .control{
    text-align: center;
    width: 100%;
    height: 30px;
    top: calc(50% - 20px);
    display: none;
    left: 0;
}
.slider:hover .control{
    display: inline-block;
}
.slider .button em{
    display: inline-block;
    width: 20px;
    height: 5px;
    background-color: rgb(225,225,226);
    border-radius: 10px;
    margin: 0 2px;
    cursor: pointer;
}
.slider em.center{
  background-color: rgb(198,47,47);
}
.slider .tag{
  display: inline-block;
  padding: 5px;
  background-color: rgb(198,47,47);
  position: absolute;
  right: 0px;
  bottom: 15px;
  border-top-left-radius: 10px;
  font-size: 0.5rem;
  border-bottom-left-radius: 10px;
  color: white;
}
</style>