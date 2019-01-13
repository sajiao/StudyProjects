using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
  public  interface IBLL
    {
        /// <summary>
        /// 检查数据
        /// </summary>
        List<String> Check();

        /// <summary>
        /// 转换数据
        /// </summary>
        void ConvertData();
    }
}
