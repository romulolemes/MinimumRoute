using MinimumRoute.Data;
using MinimumRoute.Model;
using System;
using System.Collections.Generic;

namespace MinimumRoute.Repository
{
    public class RouteRepository
    {
        protected IContext _context;

        public RouteRepository(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRange(List<RouteEntity> routes)
        {
            _context.Routes.AddRange(routes);
        }
    }
}
