using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MojangAPI.Model
{
    public class NameHistory
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("changedToAt")]
        public long? ChangedToAt { get; set; }

        public DateTime ChangedTime
        {
            get
            {
                if (ChangedToAt == null)
                    return DateTime.MinValue;
                else
                    return DateTimeOffset.FromFileTime((long)ChangedToAt).LocalDateTime;
            }
        }
    }
}
