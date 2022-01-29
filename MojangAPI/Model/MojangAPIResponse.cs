using System;
using System.Collections.Generic;
using System.Text;

namespace MojangAPI.Model
{
    public class MojangAPIResponse : HttpAction.ActionResponse
    {
        public override bool IsSuccess
            => base.IsSuccess
            && string.IsNullOrEmpty(Error)
            && string.IsNullOrEmpty(ErrorMessage); 

        public virtual string Error { get; set; }
        public virtual string ErrorMessage { get; set; }
    }
}
