using System;
using System.Collections.Generic;
using System.Text;

namespace MojangAPI.Model
{
    public class PlayerProfile : MojangAPIResponse
    {
        public string UUID { get; set; } = "";
        public string Name { get; set; } = "";
        public Skin Skin { get; set; }
        public bool IsLegacy { get; set; } = false;
    }
}
