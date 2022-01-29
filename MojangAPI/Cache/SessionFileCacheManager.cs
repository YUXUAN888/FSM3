using MojangAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.Cache
{
    public class SessionFileCacheManager : JsonFileCacheManager<Session>
    {
        public SessionFileCacheManager(string filepath) : base (filepath)
        {

        }

        public override Session GetDefaultObject()
        {
            return new Session()
            {
                ClientToken = Guid.NewGuid().ToString()
            };
        }
    }
}
