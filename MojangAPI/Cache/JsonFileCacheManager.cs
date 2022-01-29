using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MojangAPI.Cache
{
    public class JsonFileCacheManager<T> : ICacheManager<T>
    {
        public string CacheFilePath { get; private set; }

        public JsonFileCacheManager(string filepath)
        {
            this.CacheFilePath = filepath;
        }

        public virtual T GetDefaultObject() => default(T);

        public T ReadCache()
        {
            if (!File.Exists(CacheFilePath))
                return GetDefaultObject();

            try
            {
                string filecontent = File.ReadAllText(CacheFilePath);
                return JsonConvert.DeserializeObject<T>(filecontent);
            }
            catch
            {
                return GetDefaultObject();
            }
        }

        public void SaveCache(T obj)
        {
            try
            {
                File.WriteAllText(CacheFilePath, JsonConvert.SerializeObject(obj));
            }
            catch
            {

            }
        }
    }
}
