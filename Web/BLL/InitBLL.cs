using DotNet.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
   public static class InitBLL
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        public static void Init()
        {
            //初始化其它配置，获取实现了IInits接口的类
            List<Type> typeList = ReflectionHelper.GetTypeListOfImplementedInterface(typeof(IInits));
            foreach (Type type in typeList)
            {
                //使用接口来生成对象
                var model = type.Assembly.CreateInstance(type.FullName) as IInits;
                model.Init();
            }
        }
    }
}
