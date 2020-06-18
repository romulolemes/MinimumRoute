using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumRoute.Model
{
    public class CityEntity
    {
        public CityEntity(string name, string code)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }

        public string Name { get; set; }
        public string Code { get; set; }


    }
}
