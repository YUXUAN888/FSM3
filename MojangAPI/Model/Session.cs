using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojangAPI.Model
{
    public class Session
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string UUID { get; set; }
        public string ClientToken { get; set; }

        [JsonIgnore]
        public bool IsEmpty
            => string.IsNullOrEmpty(Username)
            && string.IsNullOrEmpty(AccessToken)
            && string.IsNullOrEmpty(UUID);
    }
}
