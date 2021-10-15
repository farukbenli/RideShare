using System;
using System.Collections.Generic;

namespace RideShareCase
{
    public class BaseException : Exception
    {
        public Dictionary<object, object> statusInfo = new Dictionary<object, object>();

        public BaseException(int status)
        {
            statusInfo["statusCode"] = status;
        }
        public BaseException(int status, Exception e)
        {
            statusInfo["statusCode"] = status;
            statusInfo["exception"] = e;
        }

        public override System.Collections.IDictionary Data
        {
            get
            {
                return statusInfo;
            }
        }
    }
}