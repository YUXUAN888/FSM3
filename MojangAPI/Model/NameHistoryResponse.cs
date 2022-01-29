using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.Model
{
    public class NameHistoryResponse : MojangAPIResponse
    {
        public NameHistoryResponse()
        {

        }

        internal NameHistoryResponse(NameHistory[] nameHistories)
        {
            this.Histories = nameHistories;
        }

        public NameHistory[] Histories { get; set; }
    }
}
