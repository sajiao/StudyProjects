using DAL;
using Entities;
using System.Collections.Generic;
using Entities.Model;

namespace BLL
{
   public class WordsBLL : DbContext, IInits, IBLL
    {
        private static Dictionary<string, Words> mDict = null;
        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<string, Words>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Word] = item;
                }
            }
            else
            {
                mDict = new Dictionary<string, Words>();
            }
        }

        public List<string> Check()
        {
            return null;
        }

        public void ConvertData()
        {
            foreach (var item in mDict)
            {
                var etyma = EtymaBLL.GetById(item.Value.EtymaId);
                if (etyma != null)
                {
                    item.Value.EtymaWord = etyma.Word;
                }
            }
        }

        private static Words GetItem(string word)
        {
            if (mDict.ContainsKey(word))
            {
                return mDict[word];
            }

            return null;
        }

        public static Words GetById(int id)
        {
            Words result = null;
            foreach (var item in mDict)
            {
                if (item.Value.Id == id)
                {
                    result = item.Value;
 
                    break;
                }
            }
            return result;
        }

        public static List<Words> GetByEtyma(int etymaId)
        {
            List<Words> result = new List<Words>();
            foreach (var item in mDict)
            {
                if (item.Value.EtymaId == etymaId)
                {
                    result.Add(item.Value);
                }
            }
            return result;
        }

        public static (bool, Words) GetByName(string word)
        {
            Words result = GetItem(word) ;
            bool isExist = result != null;
            return (isExist, result);
        }

        public static List<Words> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.WordsDb.GetList();
        }

        public static Words Insert(Words param)
        {
            var (isExist, result) = GetByName(param.Word);
            int id;
            if (isExist)
            {
                id = result.Id;
                param.Id = result.Id;
                Update(param);
            }
            else
            {
                var dbContext = new DbContext();
                id = dbContext.WordsDb.InsertReturnIdentity(param);
                param.Id = id;
            }
            mDict[param.Word] = param;
            return GetById(id);
        }

        public static Words Update(Words param)
        {
            var dbContext = new DbContext();
            dbContext.WordsDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.WordsDb.DeleteById(id);
        }
    }
}
