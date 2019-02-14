<template>
  <div style="margin-top:10px">
    <div id="sideleft">
      <el-table v-loading="loading" :data="list" highlight-current-row style="width: 100%">
        <el-table-column align="left" label="标题">
          <template slot-scope="scope">
            [{{scope.row.categoryName}}]
            <router-link :to="'/nanhu/detail/'+scope.row.id" class="link-type">
              <span v-if="scope.row.isStick">置顶</span> <span> {{ scope.row.title }}</span>
            </router-link>
          </template>
        </el-table-column>


        <!--<el-table-column v-if="!isMobile" width="100px" align="center" label="阅读量">
    <template slot-scope="scope">
      <span>{{ scope.row.readingCount }}</span>
    </template>
  </el-table-column>

  <el-table-column v-if="!isMobile" width="100px" align="center" label="评论数">
    <template slot-scope="scope">
      <span>{{ scope.row.commentCount }}</span>
    </template>
  </el-table-column>-->

        <el-table-column v-if="!isMobile" width="180px" align="center" label="发布日期">
          <template slot-scope="scope">
            <span>{{ scope.row.showStartTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
          </template>
        </el-table-column>

      </el-table>

      <pagination v-show="total>0" :total="total" :page-index.sync="listQuery.pageIndex" :page-size.sync="listQuery.pageSize" @pagination="getList" />

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
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
import api from '@/api/api'
import baseapi from '@/api/baseapi'

export default {
  name: 'ArticleList',
  components: { Pagination },
  directives: { waves },
  filters: {
    statusFilter(status) {
      const statusMap = {
        published: 'success',
        draft: 'info',
        deleted: 'danger'
      }
      return statusMap[status]
    }
  },
  data() {
    return {
      list: null,
      isMobile: this.$store.state.app.device === 'mobile',
      total: 0,
      loading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 10,
        title: '',
        sort: 'id'
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.loading = true
      baseapi.get(api.nanhuarticleAPI, this.listQuery).then(response => {
        this.total = response.data.result.totalCount
        this.list = response.data.result.results
        this.loading = false
      })
    },
    handleFilter() {
      this.listQuery.pageIndex = 1
      this.getList()
    },
    handleSizeChange(val) {
      this.listQuery.pageSize = val
      this.getList()
    },
    handleCurrentChange(val) {
      this.listQuery.page = val
      this.getList()
    }
  }
}
</script>

<style scoped>
.edit-input {
  padding-right: 100px;
}
.cancel-btn {
  position: absolute;
  right: 15px;
  top: 10px;
}

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



  .bt {
    padding: 2px
  }

    .bt li {
      padding: 2px;
      border-bottom: 1px dotted #ddd
    }


    #news_body th, #news_body td {
      border: 1px solid silver;
      border-collapse: collapse;
      padding: 3px
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
