using MinimumRoute.Data;
using MinimumRoute.Model;
using System;
using System.Collections.Generic;

namespace MinimumRoute.Repository
{
    public class RouteRepository 
    {
        protected Context _context;

        public RouteRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRange(IEnumerable<RouteEntity> routes)
        {
            _context.Routes.AddRange(routes);
        }
    }
}
