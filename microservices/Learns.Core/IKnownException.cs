using System;
using System.Collections.Generic;
using System.Text;

namespace Learns
{
    public interface IKnownException
    {
        string Message { get; }

        int ErrorCode { get; }

        object[] ErrorData { get; }
    }
}
