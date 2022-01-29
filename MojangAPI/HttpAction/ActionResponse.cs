using System;
using System.Collections.Generic;
using System.Text;

namespace HttpAction
{
    public class ActionResponse
    {
        public virtual bool IsSuccess
            => StatusCode / 100 == 2;

        public int StatusCode { get; set; }
    }
}
