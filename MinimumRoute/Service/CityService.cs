using MinimumRoute.Data;
using MinimumRoute.Exceptions;
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

        //TODO: Olhar a possibilidade de testar se o parametro é null
        public virtual CityEntity FindByCode(string code)
        {
            CityEntity cityEntity = _context.Cities.FirstOrDefault(c => c.Code.Equals(code));
            if (cityEntity != null)
            {
                return cityEntity;
            }
            throw new EntityNotFoundException($"City code '{code}' not found");
        }
    }
}
