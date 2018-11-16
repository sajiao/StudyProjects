using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common.Helper
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
        public static List<Type> GetTypeListOfImplementedInterface(params Type[] interfaceTypes)
        {
         
            if (interfaceTypes == null || interfaceTypes.Length == 0) return null;
            //获取整个应用程序集的类型数组
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            if (types == null || types.Length == 0) return null;
            List<Type> typeList = new List<Type>();
            var iTypes = types.Where(t => {
                var allInterfaces = t.GetInterfaces();
                return interfaceTypes.All(item => allInterfaces.Contains(item));
                });
            if (iTypes == null || iTypes.Count() == 0) return null;
            typeList = new List<Type>(iTypes.Count());
            foreach (var item in iTypes)
            {
                typeList.Add(item);
            }
 
            return typeList;
        }
    }
}
