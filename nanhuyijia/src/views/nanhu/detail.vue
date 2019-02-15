<template>
  <div>
    <div id="sideleft">
      <div id="news_main" style="width:100%">
        <div id="news_title"> {{entity.title}}</div>
        <div id="news_info">

          <span class="news_poster">投递人 {{entity.createdByName}}</span>
          <span class="time">发布于 {{entity.showStartTime | parseTime('{y}-{m}-{d} {h}:{i}')}}</span>
          <span class="comment">{{entity.commentCount}}</span>
          <span class="view">有{{entity.readingCount}}人阅读</span>
          <blockquote>
            <p id="news_summary">
              {{entity.summary}}
            </p>
          </blockquote>

        </div>
        <!--end: news_info -->
        <div id="news_content">
          <div id="news_body">

            <p style="text-align: center;">


            </p>
            <div id="ArcitleContent" v-html="entity.content">

            </div>
          </div><!--end: news_body -->
          <div id="news_otherinfo">
            <div id="up_down">
              <div class="diggit" onclick="DiggOppose(1)">
                <span class="diggnum" id="digg_num"><asp:Literal runat="server" ID="ltdigg"></asp:Literal></span>
              </div>
              <div class="buryit" onclick="DiggOppose(2)">
                <span class="burynum" id="bury_num"><asp:Literal runat="server" ID="ltoppse"></asp:Literal></span>
              </div>
              <div class="clear"></div>
              <div id="digg_tip">&nbsp;</div>
            </div>
            <div id="come_from">
              来自: {{entity.title}}
            </div>
            <br />

            <!--end: come_from -->
            <div class="clear"></div>


          </div>
          <p style="text-align: center; height:110px;">

          </p>
          <!--end: news_otherinfo -->
        </div>
        <!--end: news_content 新闻的主体 -->
        <div class="prevnext_block">
          &laquo;上一篇：{{entity.title}}<br>
          &raquo;下一篇:
          {{entity.title}} <br>
        </div>


      </div>
    </div>
    <div id="sideright" v-if="!isMobile" width="300px" style="margin-top:10px">
      <div class="side_block">
        <h3 class="title_yellow">阅读排行</h3>
        <ul id="navlist" class="topnews block_list bt">
          <li>aaaaaaaaaa</li>
          <li>aaaaaaaaaa</li>
          <li>aaaaaaaaaa</li>
          <li>aaaaaaaaaa</li>
          <li>aaaaaaaaaa</li>
        </ul>
      </div>
      </div>
 </div>

</template>

<script>
  import waves from '@/directive/waves' // Waves directive
  import { parseTime } from '@/utils'
  import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
  import api from '@/api/api'
  import baseapi from '@/api/baseapi'
  export default {
    name: 'ComplexTable',
    components: { Pagination },
    directives: { waves },
    data() {
      return {
        isMobile: this.$store.state.app.device === 'mobile',
        tableKey: 0,
        list: null,
        total: 0,
        loading: true,
        temp: {
          id: 0,
          name: '',
          alias: '',
          moduleId: undefined,
          moduleAlias: '',
          ifOpen: false,
          isClient: false,
          openNeedPlayLv: 0,
          openNeedPlayIsVip: false,
          advancedOpenVIPLv: 0,
          needTaskID: 0
        },
        entity:{
          id: undefined,
          categoryId: 0,
          title: '',
          content: '',
          summary: '',
          isStick: false,
          stickEndTime: 0,
          stickEndTime: 0,
          showStartTime: undefined,
          showEndTime: 0,
          showObject: 0,
          readingCount: 0,
          commentCount: 0,
          status: 1
        }
      }
    },
    created() {
      const id = this.$route.params && this.$route.params.id

      baseapi.getById(api.nanhuarticleAPI, id).then(response => {
        this.entity = response.data.result;
      });

    },
    methods: {
    }
  }
</script>
<style>
  * {
    margin: 0;
    padding: 0;
    font-family: Verdana,Arial,Helvetica,sans-serif;
    font-size: 12px;
    line-height: 1.5em
  }

  body {
    color: #494949
  }

  h1 {
    font-size: 16px;
    font-weight: bold;
    margin: 3px
  }

  img {
    border: none
  }


  #sideleft {
    float: left;
    width: 68%
  }

  #sideright {
    float: right;
    width: 31%
  }

  #guide h3 {
    color: #005fa9;
    font-size: 14px
  }

  #news_main {
    margin-top: 15px;
    background-color: #fafafa;
    padding: 0 10px 20px 10px;
    border: solid 1px #e8e8e8;
  }

  #news_summary {
    padding: 10px 0;
  }

  blockquote {
    border: 1px dotted #98A549;
    padding-left: 15px;
    padding-right: 15px;
    margin-bottom: 15px;
    margin-top: 15px;
    text-align: left;
  }

  #news_title {
    clear: both;
    text-align: center;
    padding-top: 25px;
    padding-bottom: 25px;
    font-size: 20px;
    border-bottom: 1px solid #e1e1e1;
    margin-left: 5px;
    margin-right: 5px;
    font-weight: bold;
    font-family: "ms song","Microsoft Yahei",SimHei,Verdana,Helvetica,SimSun,Arial,"Arial Unicode MS",MingLiu,PMingLiu,"MS Gothic",sans-serief
  }

    #news_title a:link, #news_title a:active, #news_title a:visited, #news_title a:hover {
      color: #494949;
      text-decoration: none;
      font-size: 20px;
      font-family: "ms song","Microsoft Yahei",SimHei,Verdana,Helvetica,SimSun,Arial,"Arial Unicode MS",MingLiu,PMingLiu,"MS Gothic",sans-serief
    }

  h1 a:link, h1 a:visited, h1 a:active {
    font-size: 20px;
    font-weight: bold;
    text-decoration: none;
    color: #494949
  }

  h1 a:hover {
    color: red
  }

  #news_info {
    text-align: center;
    margin-top: 15px;
    margin-bottom: 30px
  }

    #news_info .news_poster {
      margin-right: 5px
    }

    #news_info .comment, #news_info .time {
      margin-right: 10px
    }



  .white {
    color: #fff
  }

  .red {
    color: red
  }

  .gray {
    color: gray
  }

  .green {
    color: green
  }

  .clear {
    clear: both
  }

  .pfs {
    height: 16px;
    width: 16px;
    border: 1px solid gray;
    vertical-align: middle;
    padding: 0;
    margin: 0
  }

  a:link {
    color: #075db3;
    text-decoration: underline
  }

  a:visited {
    color: #075db3;
    text-decoration: underline
  }

  a:hover {
    color: #f60;
    text-decoration: underline
  }

  a:active {
    color: red;
    text-decoration: underline
  }

  a.blue:link {
    color: #005fa9;
    text-decoration: underline
  }

  a.blue:visited {
    color: #005fa9;
    text-decoration: underline
  }

  a.blue:hover {
    color: #fff;
    text-decoration: none;
    background: #039
  }

  a.blue:active {
    color: #fff;
    text-decoration: none;
    background: #ff6240
  }

  a.skyblue:link {
    color: #075db3;
    text-decoration: underline
  }

  a.skyblue:visited {
    color: #075db3;
    text-decoration: underline
  }

  a.skyblue:hover {
    color: red;
    text-decoration: underline
  }

  a.skyblue:active {
    color: red;
    text-decoration: none
  }

  a.redlink:link {
    color: red;
    text-decoration: underline
  }

  a.redlink:visited {
    color: red;
    text-decoration: underline
  }

  a.redlink:hover {
    color: red;
    text-decoration: underline
  }

  a.redlink:active {
    color: red;
    text-decoration: underline
  }

  a.graylink:link {
    color: gray;
    text-decoration: none;
    background: #f0f0f0
  }

  a.graylink:visited {
    color: gray;
    text-decoration: none;
    background: #f0f0f0
  }

  a.graylink:hover {
    color: #fff;
    text-decoration: none;
    background: #039
  }

  a.graylink:active {
    color: red;
    text-decoration: none
  }

  a.gray:link {
    color: gray;
    text-decoration: none
  }

  a.gray:visited {
    color: gray;
    text-decoration: none
  }

  a.gray:hover {
    color: gray;
    text-decoration: underline
  }

  a.gray:active {
    color: red;
    text-decoration: none
  }

  a.otherpage:link {
    color: #005fa9;
    text-decoration: none;
    font-size: 14px;
    font-weight: bold
  }

  a.otherpage:visited {
    color: #005fa9;
    text-decoration: none;
    font-size: 14px;
    font-weight: bold
  }

  a.otherpage:hover {
    color: #fff;
    text-decoration: none;
    font-size: 14px;
    font-weight: bold;
    background: #36c
  }

  a.otherpage:active {
    color: #fff;
    text-decoration: none;
    font-size: 14px;
    font-weight: bold;
    background: #ff6240
  }

  .bt {
    padding: 2px
  }

    .bt li {
      padding: 2px;
      border-bottom: 1px dotted #ddd
    }

  #news_content {
    font-size: 14px;
    border-bottom: 0 solid #b2b2b2;
    line-height: 1.5em
  }

    #news_content * {
      font-size: 14px
    }

    #news_content .news_pic {
      margin: 0 5px 5px 5px
    }

    #news_content .diggword {
      margin-top: 30px;
      float: left;
      margin-left: 10px;
      font-size: 12px
    }

    #news_content .share_block {
      padding-top: 6px;
      font-size: 12px;
      color: gray
    }

      #news_content .share_block img {
        margin: 0 3px;
        border: none;
        position: relative;
        top: 2px
      }

  #news_body {
    margin-left: 10px;
    margin-right: 10px;
    line-height: 180%;
    overflow: hidden;
    color: #404040
  }

    #news_body .indent {
      margin-left: 0
    }

    #news_body span {
      line-height: 180%;
      color: #404040
    }

    #news_body dl {
      line-height: 180%
    }

      #news_body dl dt {
        line-height: 180%
      }

      #news_body dl dd {
        line-height: 180%
      }

    #news_body p {
      margin-top: 10px;
      margin-bottom: 10px;
      line-height: 180%;
      color: #404040
    }

    #news_body table {
      margin: 0 auto;
      border: 1px solid silver;
      border-collapse: collapse
    }

    #news_body th, #news_body td {
      border: 1px solid silver;
      border-collapse: collapse;
      padding: 3px
    }

      #news_body td p {
        margin-top: 0;
        margin-bottom: 0
      }

    #news_body li {
      line-height: 180%
    }

    #news_body h3 {
      margin-top: 10px;
      margin-bottom: 10px;
      line-height: 180%
    }

    #news_body br {
      line-height: 10px
    }

    #news_body a:link, #news_body a:visited, #news_body a:active {
      color: #005fa9;
      text-decoration: none
    }

    #news_body a:hover {
      text-decoration: underline
    }

    #news_body a b {
      color: #005fa9
    }

    #news_body a strong {
      color: #005fa9
    }

    #news_body strong {
      color: #404040
    }

    #news_body ul {
      list-style-type: auto;
      margin: 0 2px 0 45px
    }

    #news_body .noindent {
      margin-left: 0;
      padding-left: 20px
    }

    #news_body ul li {
      line-height: 180%
    }

    #news_body ol {
      list-style-type: decimal;
      margin: 5px 2px 5px 50px
    }

    #news_body div {
      line-height: 180%
    }

    #news_body blockquote {
      line-height: 180%;
      background: none;
      border: 1px dotted #98A549;
      color: #555;
      margin-left: 25px;
      padding-left: 10px;
      padding-right: 10px;
      padding-top: 5px;
      padding-bottom: 5px;
      margin-top: 10px;
      margin-bottom: 10px
    }

      #news_body blockquote p {
        color: #555
      }

    #news_body h1 {
      margin: 5px auto 5px auto;
      padding: 0;
      font-size: 14px
    }

    #news_body h2 {
      margin: 5px auto 5px auto;
      padding: 0;
      font-size: 14px
    }

    #news_body h3 {
      margin: 5px auto 5px auto;
      padding: 0;
      font-size: 14px
    }

    #news_body h4 {
      margin: 5px auto 5px auto;
      padding: 0;
      font-size: 14px
    }

  #news_info .news_poster {
    margin-right: 5px
  }

  #news_info .comment, #news_info .time {
    margin-right: 10px
  }

  #news_otherinfo {
    margin: 25px
  }

  #up_down {
    float: right;
    text-align: right;
    margin-bottom: 10px;
    font-size: 12px
  }

    #up_down .diggnum {
      color: #f60
    }

  .diggit {
    TEXT-ALIGN: center;
    MARGIN-TOP: 2px;
    WIDTH: 46px;
    BACKGROUND: url(/images/upup.gif) no-repeat;
    HEIGHT: 52px;
    CURSOR: pointer;
    float: left;
  }

  .diggnum {
    LINE-HEIGHT: 2.2em;
    COLOR: #075db3;
    FONT-SIZE: 14px
  }

  .buryit {
    TEXT-ALIGN: center;
    MARGIN-TOP: 2px;
    WIDTH: 46px;
    BACKGROUND: url(/images/downdown.gif) no-repeat;
    HEIGHT: 52px;
    CURSOR: pointer;
    float: left;
  }

  .burynum {
    LINE-HEIGHT: 2.2em;
    COLOR: #075db3;
    FONT-SIZE: 14px
  }

  .digg-tip {
    WIDTH: 50px;
    COLOR: red;
    FONT-SIZE: 12px !important
  }

  .buryit {
    float: left;
    margin-left: 20px
  }

  #news_feedback {
    margin-top: 15px;
    padding-bottom: 5px;
    border-bottom: 1px solid #b2b2b2
  }

  #digg_tip {
    font-size: 12px !important;
    text-align: center;
    color: red
  }

    #digg_tip a {
      font-size: 12px !important
    }

  #come_from {
    font-size: 12px;
    text-align: left;
    color: gray;
    float: left;
    margin-top: 0;
  }

    #come_from a {
      font-size: 12px
    }

  .fenxiang {
    margin-left: 15px;
    margin-top: 10px;
  }

  #news_more_info {
    margin-left: 0;
    font-size: 13px;
    color: #666;
    margin-bottom: 5px
  }

    #news_more_info a:link, #news_more_info a:active, #news_more_info a:visited {
      font-size: 13px;
      text-decoration: none
    }

    #news_more_info a:hover {
      text-decoration: underline
    }

    #news_more_info span {
      font-size: 13px
    }

  .news_tags {
    margin-top: 10px
  }

  .prevnext_block {
    border-top: 1px dotted #ccc;
    line-height: 2em;
    padding: 10px 0
  }

  a.common_link:link, a.common_link:visited, a.common_link:active {
    color: #005fa9;
    text-decoration: none
  }

  a.common_link:hover {
    color: #f60;
    text-decoration: underline
  }

  #comment_tips {
    border-bottom: 1px dotted silver;
    border-top: 1px dotted silver;
    color: #333;
    font-family: verdana,arial,times new roman;
    font-size: 14px;
    font-weight: bold;
    margin: 0 0 10px;
    padding: 5px 0;
    text-transform: uppercase
  }

  .comment_main {
    margin-top: 5px;
    padding-left: 40px;
    padding-top: 10px;
    padding-right: 1em;
    padding-bottom: 10px;
    background: url('/images/comment.gif') no-repeat
  }

  #comment_main {
    padding-left: 15px;
    padding-right: 10px
  }

  .comment_option {
    padding-left: 40px;
    line-height: 2em
  }

    .comment_option .inneroption {
      float: right
    }

    .comment_option a:link, .comment_option a:visited, .comment_option a:hover, .comment_option a:active {
      text-decoration: none
    }

    .comment_option .redlink {
      margin-left: 10px
    }

  .comment_feedback {
    margin-right: 20px;
    margin-left: 30px
  }



  .block_list li {
    margin-bottom: 3px;
    padding-left: 5px;
    padding-right: 5px
  }

  .block_list a {
    text-decoration: none
  }

  .topnews li {
    -o-text-overflow: ellipsis;
    text-overflow: ellipsis;
    overflow: hidden;
    word-break: break-all;
    color: gray
  }

  ul.bt li {
    white-space: nowrap;
    text-overflow: ellipsis;
    -o-text-overflow: ellipsis;
    overflow: hidden;
    width: 290px
  }
  .side_block {
    margin-top: 20px
  }

  .title_yellow {
    margin: 0 auto 20px auto;
    color: #413a0c;
    font-size: 14px;
    font-weight: bold;
    background: url(/images/bt_title_yellow.gif) no-repeat;
    border: solid 0 gray;
    height: 25px;
    line-height: 25px;
    padding-left: 10px
  }


</style>
