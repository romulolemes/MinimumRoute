using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumRoute.Serialization
{
    public interface ISerializer
    {
        public string Serializer(Func<object, string> deserializeField);
    }
}
