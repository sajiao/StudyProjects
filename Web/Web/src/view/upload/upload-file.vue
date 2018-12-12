<template>
  <Row :gutter="10">
    <i-col span="6">
      <Card>
        <Upload action="" :before-upload="beforeUpload">
          <Button icon="ios-cloud-upload-outline">上传文件</Button>
          &nbsp;&nbsp;&nbsp;&nbsp;点击上传文件
        </Upload>
       
      </Card>
    </i-col>
    <i-col span="18">
      <Table :height="500" :columns="columns" :data="tableData" />
    </i-col>
  </Row>
</template>

<script>
import { getArrayFromFile, getTableDataFromArray } from '@/libs/util'
export default {
  name: 'upload_file_page',
  data () {
    return {
      columns: [],
      tableData: []
    }
  },
  methods: {
    beforeUpload (file) {
      getArrayFromFile(file).then(data => {
        let { columns, tableData } = getTableDataFromArray(data)
        this.columns = columns
        this.tableData = tableData
      }).catch(() => {
        this.$Notice.warning({
          title: '只能上传Csv文件',
          desc: '只能上传Csv文件，请重新上传'
        })
      })
      return false
    }
  }
}
</script>

<style>
  .update-table-intro {
    margin-top: 10px;
  }

  .code-high-line {
    color: #2d8cf0;
  }
</style>
