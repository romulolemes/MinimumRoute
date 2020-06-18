using MinimumRoute.Model;

namespace MinimumRoute.Repository
{
    public interface ICityRepository
    {
        CityEntity FindByCode(string code);
    }
}