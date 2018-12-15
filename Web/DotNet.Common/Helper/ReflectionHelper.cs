using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common
{
    /// <summary>
    /// 反射辅助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 获取实现了接口的类型列表
        /// </summary>
        /// <param name="interfaceTypes">接口类型集合</param>
        /// <returns></returns>
        public static List<Type> GetTypeListOfImplementedInterface(string assemblyName ,params Type[] interfaceTypes)
        {

            List<Type> typeList = new List<Type>();

            //获取整个应用程序集的类型数组
            Type[] types;

            if (assemblyName.IsNullOrEmpty())
            {
                types = Assembly.GetExecutingAssembly().GetTypes();
            }
            else
            {
                types = Assembly.Load(assemblyName).GetTypes();
            }
            
            if (types == null || types.Length == 0) return typeList;

            //遍历每一个类型，看看是否实现了指定的接口
            foreach (Type type in types)
            {
                //获得此类型所实现的所有接口列表
                Type[] allInterfaces = type.GetInterfaces();

                //判断给出的接口类型列表是否存在于实现的所有接口列表中
                if (interfaceTypes.All(item => allInterfaces.Contains(item)))
                {
                    typeList.Add(type);
                }
            }

            return typeList;
        }
    }
}
