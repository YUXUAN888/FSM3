using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MojangAPI.Model
{
    public class PlayerUUID : MojangAPIResponse
    {
        public override bool IsSuccess
            => base.IsSuccess
            && CheckUserExistence();

        private bool CheckUserExistence()
        {
            if (StatusCode != 200)
            {
                Error = "NoPlayer";
                return false;
            }
            else
                return true;
        }

        [JsonProperty("id")]
        public string UUID { get; set; }

        [JsonProperty("name")]
        public string CurrentUsername { get; set; }

        [JsonProperty("legacy")]
        public bool IsLegacy { get; set; } = false;

        [JsonProperty("demo")]
        public bool IsDemo { get; set; } = false;
    }
}
