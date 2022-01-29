using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MojangAPI.Model
{
    public class MojangAuthResponse : HttpAction.ActionResponse
    {
        public MojangAuthResponse(MojangAuthResult result)
        {
            this.Result = result;
        }

        public override bool IsSuccess
            => base.IsSuccess
            && this.Result == MojangAuthResult.Success
            && string.IsNullOrEmpty(Error)
            && string.IsNullOrEmpty(ErrorMessage);

        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("message")]
        public string ErrorMessage { get; set; }

        public MojangAuthResult Result { get; set; }
        public Session Session { get; set; }
    }
}
