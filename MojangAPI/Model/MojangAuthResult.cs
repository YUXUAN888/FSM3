using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.Model
{
    public enum MojangAuthResult
    {
        Success,
        BadRequest,
        InvalidCredentials,
        InvalidSession,
        NoProfile,
        UnknownError
    }
}
