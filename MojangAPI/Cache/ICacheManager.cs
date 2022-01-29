using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.Cache
{
    public interface ICacheManager<T>
    {
        T GetDefaultObject();
        T ReadCache();
        void SaveCache(T obj);
    }
}
