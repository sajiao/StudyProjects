using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNet.Common
{
   public class ZipHelper
    {
        private const int BUFFERSIZE = 1024;

        public static void CompressFile(IEnumerable<string> files, IEnumerable<string> folders, string savePath)
        {
            string directoryName = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            List<string> expr_1B = new List<string>();
            if(files != null)
            {
                expr_1B.AddRange(files);
            }

            if (folders != null)
            {
                expr_1B.AddRange(folders);
            }
           
            Dictionary<string, string> dictionary = PrepareFileSystementities(expr_1B);
            using (ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(savePath)))
            {
                zipOutputStream.SetLevel(6);
                foreach (var item in dictionary)
                {
                   
                    if (!File.Exists(item.Key))
                    {
                        ZipEntry entry = new ZipEntry(item.Key + "/");
                        zipOutputStream.PutNextEntry(entry);
                    }
                    else
                    {
                        FileInfo fileInfo = new FileInfo(item.Key);
                        using (FileStream fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            zipOutputStream.PutNextEntry(new ZipEntry(item.Value)
                            {
                                DateTime = fileInfo.LastWriteTime,
                                Size = fileStream.Length
                            });
                            byte[] array = new byte[1024];
                            int num;
                            do
                            {
                                num = fileStream.Read(array, 0, 1024);
                                zipOutputStream.Write(array, 0, num);
                            }
                            while (num == 1024);
                        }
                    }
                }
                
            }
        }

        public static void DecomparessFile(string zipPath, string targetPath)
        {
            if (!File.Exists(zipPath))
            {
                throw new FileNotFoundException("要解压的文件不存在", zipPath);
            }
            if (string.IsNullOrWhiteSpace(targetPath))
            {
                targetPath = Path.GetDirectoryName(zipPath);
            }
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            using (ZipInputStream zipInputStream = new ZipInputStream(File.Open(zipPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                while (true)
                {
                    ZipEntry nextEntry = zipInputStream.GetNextEntry();
                    if (nextEntry == null)
                    {
                        break;
                    }
                    if (nextEntry.IsDirectory)
                    {
                        Directory.CreateDirectory(Path.Combine(targetPath, Path.GetDirectoryName(nextEntry.Name)));
                    }
                    else
                    {
                        string fileName = Path.GetFileName(nextEntry.Name);
                        if (!string.IsNullOrEmpty(fileName) && fileName.Trim().Length > 0)
                        {
                            FileInfo fileInfo = new FileInfo(Path.Combine(targetPath, nextEntry.Name));
                            using (FileStream fileStream = fileInfo.Create())
                            {
                                byte[] array = new byte[1024];
                                int num;
                                do
                                {
                                    num = zipInputStream.Read(array, 0, 1024);
                                    fileStream.Write(array, 0, num);
                                }
                                while (num == 1024);
                                fileStream.Flush();
                            }
                            fileInfo.LastWriteTime= nextEntry.DateTime;
                        }
                    }
                }
            }
        }

        private static Dictionary<string, string> PrepareFileSystementities(IEnumerable<string> sourceFileEntityPathList)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var item in sourceFileEntityPathList)
            {
                string text = item;
                if (text.EndsWith("\\"))
                {
                    text = text.Remove(text.LastIndexOf("\\"));
                }
                string text2 = Path.GetDirectoryName(text) + "\\";
                if (text2.EndsWith(":\\\\"))
                {
                    text2 = text2.Replace("\\\\", "\\");
                }
                Dictionary<string, string> allFileSystemEntities = GetAllFileSystemEntities(text, text2);
                foreach (var file in allFileSystemEntities)
                {
                    string current = file.Key;
                    if (!dictionary.ContainsKey(current))
                    {
                        dictionary.Add(current, file.Value);
                    }
                }
                
            }
             
            return dictionary;
        }

        private static Dictionary<string, string> GetAllFileSystemEntities(string source, string topDirectory)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(source, source.Replace(topDirectory, ""));
            if (Directory.Exists(source))
            {
                string[] array = Directory.GetDirectories(source, "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i];
                    dictionary.Add(text, text.Replace(topDirectory, ""));
                }
                array = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < array.Length; i++)
                {
                    string text2 = array[i];
                    dictionary.Add(text2, text2.Replace(topDirectory, ""));
                }
            }
            return dictionary;
        }

    }
}
