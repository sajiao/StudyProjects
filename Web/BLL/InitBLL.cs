using DotNet.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
   public static class InitBLL
    {

        public static void Start()
        {
           // Init();
            //Check();
          //  ConvertData();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public static void Init()
        {
            try
            {
                //初始化其它配置，获取实现了IInits接口的类
                List<Type> typeList = ReflectionHelper.GetTypeListOfImplementedInterface("BLL", typeof(IInits));
                foreach (Type type in typeList)
                {
                    //使用接口来生成对象
                    var model = type.Assembly.CreateInstance(type.FullName) as IInits;
                    model.Init();
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(string.Format("Message={0};StackTrace={1}", e.Message, e.StackTrace));
                throw e;
            }
           
        }

        public static void Check()
        {
            try
            {
                //初始化其它配置，获取实现了IBLL接口的类
                List<Type> typeList = ReflectionHelper.GetTypeListOfImplementedInterface("BLL", typeof(IBLL));
                foreach (Type type in typeList)
                {
                    //使用接口来生成对象
                    var model = type.Assembly.CreateInstance(type.FullName) as IBLL;
                    var errors = model.Check();
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(string.Format("Message={0};StackTrace={1}", e.Message, e.StackTrace));
                throw e;
            }
        }

        public static void ConvertData()
        {
            try
            {
                //初始化其它配置，获取实现了IBLL接口的类
                List<Type> typeList = ReflectionHelper.GetTypeListOfImplementedInterface("BLL", typeof(IBLL));
                foreach (Type type in typeList)
                {
                    //使用接口来生成对象
                    var model = type.Assembly.CreateInstance(type.FullName) as IBLL;
                    model.ConvertData();
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(string.Format("Message={0};StackTrace={1}", e.Message, e.StackTrace));
                throw e;
            }
        }
    }
}
