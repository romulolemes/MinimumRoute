using MinimumRoute.Data;
using MinimumRoute.Model;
using System;
using System.Linq;

namespace MinimumRoute.Repository
{
    public class CityRepository : ICityRepository
    {
        protected IContext _context;

        public CityRepository(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CityEntity FindByCode(string code)
        {
            return _context.Cities.FirstOrDefault(c => c.Code.Equals(code));
        }

    }
}
