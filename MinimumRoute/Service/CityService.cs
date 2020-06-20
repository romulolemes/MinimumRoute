using MinimumRoute.Data;
using MinimumRoute.Model;
using System;
using System.Linq;

namespace MinimumRoute.Service
{
    public class CityService
    {
        protected Context _context;

        public CityService(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CityEntity FindByCode(string code)
        {
            return _context.Cities.FirstOrDefault(c => c.Code.Equals(code));
        }
    }
}
