using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.Model
{
    public class Statistics : MojangAPIResponse
    {
        public int Total { get; set; }
        public int Last24h { get; set; }
        public double SaleVelocityPerSeconds { get; set; }
    }
}
