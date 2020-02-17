using SIS.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.MvcFramework
{
    public abstract class HttpMethodAttribute : Attribute
    {
        protected HttpMethodAttribute()
        {

        }

        protected HttpMethodAttribute(string url)
        {
            Url = url;
        }

        public string Url { get; }

        public abstract HttpMethodType Type { get; }
    }
}
