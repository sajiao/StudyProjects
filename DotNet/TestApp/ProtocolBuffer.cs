using DotNet.Common;
using Entities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
   public  class ProtocolBuffer 
    {

        public void Test1()
        {
            //创建对象
            Person person = new Person() { Id = 1, Name = "Test yi" };
            person.Address = new Address() { Phone="123456", Zip="654321", Country="Chin"};
            using (var file = File.Create("person.bin"))
            {
                Serializer.Serialize(file, person);
            }

            Person newPerson;
            using (var file = File.OpenRead("person.bin"))
            {
                newPerson = Serializer.Deserialize<Person>(file);
            }
            Debug.WriteLine(newPerson.ToString());
        }


        public void Test()
        {
            //创建对象
            Person item = new Person() { Id = 1, Name = "LanOu" };
            //序列化对象
            byte[] temp = Serialize(item);
            //ProtoBuf的优势一：小
            Debug.WriteLine(temp.Length);
            //反序列化为对象
            var result = DeSerialize<Person>(temp);
            Debug.WriteLine(result);
        }

        // 将消息序列化为二进制的方法
        // < param name="model">要序列化的对象< /param>
        public byte[] Serialize<T>(T model)
        {
            try
            {
                //涉及格式转换，需要用到流，将二进制序列化到流中
                using (MemoryStream ms = new MemoryStream())
                {
                    //使用ProtoBuf工具的序列化方法
                    ProtoBuf.Serializer.Serialize<T>(ms, model);
                    //定义二级制数组，保存序列化后的结果
                    byte[] result = new byte[ms.Length];
                    //将流的位置设为0，起始点
                    ms.Position = 0;
                    //将流中的内容读取到二进制数组中
                    ms.Read(result, 0, result.Length);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("序列化失败: " + ex.ToString());
                return null;
            }
        }

        // 将收到的消息反序列化成对象
        // < returns>The serialize.< /returns>
        // < param name="msg">收到的消息.</param>
        public T DeSerialize<T>(byte[] msg)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    //将消息写入流中
                    ms.Write(msg, 0, msg.Length);
                    //将流的位置归0
                    ms.Position = 0;
                    //使用工具反序列化对象
                    var result = ProtoBuf.Serializer.Deserialize<T>(ms);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("反序列化失败: " + ex.ToString());
                return default(T);
            }
        }


    }
}
